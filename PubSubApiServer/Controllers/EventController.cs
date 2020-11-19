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
    [ApiController]
    [Route("[controller]")]
    public class EventController: ControllerBase
    {
        private readonly EventContext _eventContext;

        public EventController(EventContext eventContext)
        {
            _eventContext = eventContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Event>>> GetEvents()
        {
            var events = await _eventContext.Events.ToListAsync<Event>();

            return Ok(JsonConvert.SerializeObject(events, Formatting.Indented));
        }

        [HttpPost]
        public async Task<ActionResult<Event>> PostEvent(object _event)
        {
            if(_event != null)
            {
                _eventContext.Events.Add(JsonConvert.DeserializeObject<Event>(_event.ToString()));
            }

            await _eventContext.SaveChangesAsync();

            return Ok();
        }
    }
}
