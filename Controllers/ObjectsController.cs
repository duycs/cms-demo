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
    public class ObjectController : ControllerBase
    {

        private readonly CmsContext context;
        public ObjectController()
        {
            context = new CmsContext();
        }

        [HttpGet]
        public ActionResult Get()
        {
            var objects = context.Objects.ToList();

            if (objects == null)
                return NotFound();

            if (!objects.Any())
                return NoContent();

            var objectsDto = objects;
            return Ok(objectsDto);
        }

        [HttpGet("{id}")]
        public ActionResult Get(string id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest();

            var obj = context.Objects.Where(w => w.Id == id).FirstOrDefault();

            if (obj == null)
                return NotFound();

            var objectDto = obj;
            return Ok(objectDto);
        }

        [HttpPost()]
        public ActionResult Post([FromBody]Objects viewModel)
        {
            if (viewModel == null)
                return BadRequest();

            var obj = viewModel;
            var objectCreated = context.Objects.Add(obj);
            context.SaveChanges();

            return Ok(objectCreated);
        }

        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody]Objects viewModel)
        {
            if (string.IsNullOrEmpty(id) || viewModel == null)
                return BadRequest();

            var obj = context.Objects.Where(w => w.Id == id).FirstOrDefault();

            if (obj == null)
                return NotFound();

            // Always update UpdatedDate to now
            obj.UpdatedDate = DateTime.UtcNow;

            // Any field is not null then update this field
            if (viewModel.ObjectTypeId != null)
            {
                obj.ObjectTypeId = viewModel.ObjectTypeId;
            }

            if (viewModel.PermissionId != null)
            {
                obj.PermissionId = viewModel.PermissionId;
            }

            if (viewModel.Name != null)
            {
                obj.Name = viewModel.Name;
            }

            if (viewModel.Description != null)
            {
                obj.Description = viewModel.Description;
            }

            var objectUpdated = context.Objects.Update(obj);
            context.SaveChanges();

            return Ok(objectUpdated);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest();

            var obj = context.Objects.Where(w => w.Id == id).FirstOrDefault();

            if (obj == null)
                return NotFound();

            //Soft remove this object
            obj.IsDeleted = 1;
            context.Objects.Update(obj);

            // Hard remove this object
            //context.Objects.Remove(obj);

            context.SaveChanges();

            return NoContent();
        }
    }
}
