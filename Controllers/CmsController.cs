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

        #region Objects
        private CmsContext context = new CmsContext();
        // GET api/values
        [HttpGet("objects")]
        public ActionResult Get()
        {
            var objects = context.Objects.ToList();
            //var objects = string.Empty;

            if(!objects.Any())
                return NoContent();

            return Ok(objects);
        }

        // GET api/values/5
        [HttpGet("objects/{id}")]
        public ActionResult Get(int id)
        {
            return NoContent();
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

        #endregion Objects

        [HttpGet("object-fields/{objectId}")]
        public ActionResult GetObjectFields(int objectId)
        {
            var objectFields = context.ObjectFileds.ToList();
            var obj = context.Objects.Where(w => w.Id == objectId.ToString()).FirstOrDefault();

            if(!objectFields.Any())
                return NoContent();

            var fields = new List<Fields>();
            foreach(var objectField in objectFields){
                var field = context.Fields.Where(w=>w.Id == objectField.FieldId).FirstOrDefault();
                fields.Add(field);
            }
            var ObjectFieldDto = new ObjectFieldDto{
                Object = obj,
                Fields = fields
            };

            return Ok(ObjectFieldDto);
        }

        public class ObjectFieldDto {
            public Objects Object {get;set;}
            public List<Fields> Fields {get;set;}
        }
    }
}
