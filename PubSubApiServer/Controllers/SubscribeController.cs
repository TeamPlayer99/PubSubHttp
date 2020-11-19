using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PubSubApiServer.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PubSubApiServer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SubscribeController: ControllerBase
    {
        private readonly SubscribeContext _subscribeContext;
        private readonly TopicContext _topicContext;

        public SubscribeController(SubscribeContext subscribeContext, TopicContext topicContext)
        {
            _subscribeContext = subscribeContext;
            _topicContext = topicContext;
        }

        //HTTP POST /subscribe/{TOPIC}
        [HttpPost("{topic}")]
        public async Task<ActionResult<Subscribe>> Subscribe(object subscription, string topic)
        {
            //deserialize subscription data
            Subscribe subscribe = JsonConvert.DeserializeObject<Subscribe>(subscription.ToString());


            //get all topics in system
            Topic topicEntity = _topicContext.Topics
                .Where(t => t.Title == topic.ToLower())
                .SingleOrDefault();

            //If topic exists add subscriber else create new topic and add subscriber
            if(topicEntity != null)
            {
                topicEntity.Subscribers.Add(subscribe);
            }
            else
            {
                topicEntity = new Topic
                {
                    Title = topic.ToLower(),
                    Subscribers = new List<Subscribe>()
                };

                _topicContext.Add(topicEntity);

                subscribe.Topic = topicEntity;
                subscribe.TopicId = topicEntity.TopicId;

                topicEntity.Subscribers.Add(subscribe);
            }

            await _topicContext.SaveChangesAsync();

            _subscribeContext.Add(subscribe);

            await _subscribeContext.SaveChangesAsync();

            return Ok();
        }
    }
}
