using System;
using System.Collections.Generic;
using System.Dynamic;
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
            var objectFieldsDto = new List<object>();

            var objects = context.Objects.Where(w => w.IsDeleted == 0).ToList();

            if (objects == null)
                return NotFound();

            if (!objects.Any())
                return NoContent();

            foreach (var obj in objects)
            {
                var objectFields = context.ObjectFields.Where(w => w.ObjectId == obj.Id).ToList();

                var fieldsDto= new List<dynamic>();
                foreach (var objectField in objectFields)
                {
                    var field = context.Fields.Where(w => w.Id == objectField.FieldId).FirstOrDefault();
                    var fieldDto = GetFieldDto(field);
                    fieldsDto.Add(fieldDto);
                }

                var objDto = GetObjectDto(obj);

                var objectFieldDto = new
                {
                    Object = objDto,
                    Fields = fieldsDto
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
            var objectFieldDto = new
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

        // Private method mapping object, we want to get child object data by id of foreign key with parent object

        private dynamic GetObjectDto(Objects obj){
            dynamic objDto = new ExpandoObject();
            objDto.Id = obj.Id;
            objDto.name = obj.Name;
            objDto.objectType = GetObjectTypeById((int)obj.ObjectTypeId);
            objDto.permission = GetPermissionById((int)obj.PermissionId);
            objDto.description = obj.Description;
            objDto.updatedDate = obj.UpdatedDate;
            objDto.createdDate = obj.CreatedDate;
            return objDto;
        }

        private dynamic GetFieldDto(Fields field){
            dynamic fieldDto = new ExpandoObject();
            fieldDto.Id = field.Id;
            fieldDto.fieldName = field.FieldName;
            fieldDto.permission = GetPermissionById((int)field.PermissionId);
            fieldDto.fieldType = GetFieldTypeById((int)field.FieldTypeId);
            fieldDto.fieldValue = field.FieldValue;
            fieldDto.description = field.Description;
            fieldDto.createdDate = field.CreatedDate;

            return fieldDto;
        }

        private dynamic GetPermissionById(int id){
            var permisions = context.Permissions.Where(w => w.IsDeleted == 0).ToList();
            var result = permisions.Where(w=>w.Id == id).Select(
                    s => new {
                        s.Id,
                        s.Name,
                        s.Description
                    }
                ).FirstOrDefault();
            return result;
        }

        private dynamic GetObjectTypeById(int id){
            var objectTypes = context.ObjectTypes.Where(w => w.IsDeleted == 0).ToList();
            var result = objectTypes.Where(w=>w.Id == id).Select(
                    s => new {
                        s.Id,
                        s.Name,
                        s.Description
                    }
                ).FirstOrDefault();
            return result;
        }

        private dynamic GetFieldTypeById(int id){
            var fieldTypes = context.FieldTypes.Where(w => w.IsDeleted == 0).ToList();
            var result = fieldTypes.Where(w=>w.Id == id).Select(
                    s => new {
                        s.Id,
                        s.Name,
                        s.Description
                    }
                ).FirstOrDefault();
            return result;
        }

    }
}
