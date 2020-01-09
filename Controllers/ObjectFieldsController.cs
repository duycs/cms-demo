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
    public class ObjectFieldsController : ControllerBase
    {
        private readonly CmsContext context;
        public ObjectFieldsController()
        {
            context = new CmsContext();
        }

        [HttpGet]
        public ActionResult Get()
        {
            var objectFieldsDto = new List<ObjectFieldDto>();

            var objects = context.Objects.Where(w => w.IsDeleted == 0).ToList();

            if (objects == null)
                return NotFound();

            if (!objects.Any())
                return NoContent();

            foreach (var obj in objects)
            {
                var objectFields = context.ObjectFields.Where(w => w.ObjectId == obj.Id).ToList();

                var fields = new List<Fields>();
                foreach (var objectField in objectFields)
                {
                    var field = context.Fields.Where(w => w.Id == objectField.FieldId).FirstOrDefault();
                    fields.Add(field);
                }

                var objectFieldDto = new ObjectFieldDto
                {
                    Object = obj,
                    Fields = fields
                };

                objectFieldsDto.Add(objectFieldDto);
            }

            if (!objectFieldsDto.Any())
                return NoContent();

            return Ok(objectFieldsDto);
        }

        [HttpGet("{objectId}")]
        public ActionResult GetObjectFieldsByObjectId(int objectId)
        {
            // Find Dto use many query
            var objectFields = context.ObjectFields.ToList();
            var obj = context.Objects.Where(w => w.Id == objectId.ToString()).FirstOrDefault();

            if (!objectFields.Any())
                return NoContent();

            var fields = new List<Fields>();
            foreach (var objectField in objectFields)
            {
                var field = context.Fields.Where(w => w.Id == objectField.FieldId).FirstOrDefault();
                fields.Add(field);
            }
            var objectFieldDto = new ObjectFieldDto
            {
                Object = obj,
                Fields = fields
            };

            return Ok(objectFieldDto);
        }

        [HttpPost()]
        public ActionResult Post([FromBody]ObjectFields viewModel)
        {
            if (viewModel == null)
                return BadRequest();

            var objectFiled = viewModel;
            var objectFiledCreated = context.ObjectFields.Add(objectFiled);
            context.SaveChanges();

            return Ok(objectFiledCreated);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]ObjectFields viewModel)
        {
            if (id < 1 || viewModel == null)
                return BadRequest();

            var objectField = context.ObjectFields.Where(w => w.Id == id).FirstOrDefault();

            if (objectField == null)
                return NotFound();

            // Always update UpdatedDate to now
            objectField.UpdatedDate = DateTime.UtcNow;

            // Any field is not null then update this field
            if (viewModel.ObjectId != null)
            {
                objectField.ObjectId = viewModel.ObjectId;
            }

            if (viewModel.FieldId != null)
            {
                objectField.FieldId = viewModel.FieldId;
            }

            var objectFieldUpdated = context.ObjectFields.Update(objectField);
            context.SaveChanges();

            return Ok(objectFieldUpdated);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (id < 1)
                return BadRequest();

            var objectFiled = context.ObjectFields.Where(w => w.Id == id).FirstOrDefault();

            if (objectFiled == null)
                return NotFound();

            //Soft remove this object
            objectFiled.IsDeleted = 1;
            context.ObjectFields.Update(objectFiled);

            // Hard remove this object
            //context.ObjectFields.Remove(objectFiled);

            context.SaveChanges();

            return NoContent();
        }



        // Dto classes
        // Dto is Data transfer object to return response

        public class ObjectFieldDto
        {
            public Objects Object { get; set; }
            public List<Fields> Fields { get; set; }
        }
    }
}
