using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PubSubApiServer.Models;

namespace PubSubApiServer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PublishController: ControllerBase
    {
        private readonly PublishContext _context;

        public PublishController(PublishContext context)
        {
            _context = context;
        }

        [HttpPost("{topic}")]
        public async Task<ActionResult<Publish>> PostPublish(object publication, string topic)
        {
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
