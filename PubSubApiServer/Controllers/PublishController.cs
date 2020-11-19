using System;
using System.Linq;
using System.Text.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using PubSubApiServer.Models;
using Newtonsoft.Json;
using System.Text;

namespace PubSubApiServer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PublishController: ControllerBase
    {
        private readonly PublishContext _context;
        private readonly SubscribeContext _subscribeContext;

        public PublishController(PublishContext context, SubscribeContext subscribeContext)
        {
            _context = context;
            _subscribeContext = subscribeContext;
        }

        //HTTP POST /publish/{TOPIC}
        [HttpPost("{topic}")]
        public async Task<ActionResult<Publish>> PostPublish(object publication, string topic)
        {
            var eventStr = publication.ToString();
            var topicToLower = topic.ToLower();

            Event @event = JsonConvert.DeserializeObject<Event>(eventStr);
            Publish publish = JsonConvert.DeserializeObject<Publish>(eventStr);

            //retrive all subscribers for topic
            List<Subscribe> subscribers = _subscribeContext.Subscriptions
                .Where(t => t.Topic.Title == topicToLower)
                .ToList<Subscribe>();

            //for each subscriber post message to specified url/event
            foreach(var s in subscribers)
            {
                await testHttpClientAsync(@event, s.Url);
            }

            _context.Publications.Add(publish);

            await _context.SaveChangesAsync();

            return Ok();
        }

        private async Task testHttpClientAsync(Event @event, string url)
        {
            var json = JsonConvert.SerializeObject(@event);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            using var client = new HttpClient();

            var res = await client.PostAsync(url, data);

            string result = res.Content.ReadAsStringAsync().Result;
        }
    }
}
