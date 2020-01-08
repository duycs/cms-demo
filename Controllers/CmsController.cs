using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cms_demo.Models;
using Microsoft.AspNetCore.Mvc;

namespace cms_demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CmsController : ControllerBase
    {

        private CmsContext context = new CmsContext();
        // GET api/values
        [HttpGet("objects")]
        public ActionResult<IEnumerable<string>> Get()
        {
            var objects = context.Objects.ToList();
            //var objects = string.Empty;

            if(!objects.Any())
                return NoContent();

            return Ok(objects);
        }

        // GET api/values/5
        [HttpGet("objects/{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost("objects")]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("objects/{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("objects/{id}")]
        public void Delete(int id)
        {
        }
    }
}
