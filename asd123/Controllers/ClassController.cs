using asd123.Model;
using asd123.Presenters.Class;
using asd123.Presenters.Subject;
using asd123.Services;
using asd123.Ultil;
using asd123.UseCase.Class.Crud;
using asd123.UseCase.Major.Crud;
using asd123.UseCase.Subject.Crud;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace asd123.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassController : ControllerBase
    {
        CrudClassFlow _workflow;
        CrudMajorFlow _MajorWorkflow;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _map;
        public ClassController(IUnitOfWork uow, IMapper map)
        {
            _uow = uow;
            _map = map;
            _workflow = new CrudClassFlow(uow);
        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAllClass()
        {
            var result = _workflow.List();
            if (result.Status == Message.SUCCESS)
            {
                return Ok(result.Result);
            }

            return BadRequest(result);
        }

        [HttpGet]
        [Route("get_class_by_id")]
        public IActionResult GetClassById(int id)
        {
            var result = _workflow.FindById(id);
            if (result.Status == Message.SUCCESS)
            {
                return Ok(result.Result);
            }

            return BadRequest(result);
        }
        
        [HttpPost]
        public IActionResult CreateClass(CreateClassPresenter model)
        {
            var majorResponse = _workflow.FindByCode(model.Code);
            if (majorResponse.Status == Message.SUCCESS)
            {
                return BadRequest("Class with the same code already exists.");
            }
            
            var departmentResponse = _workflow.FindMajorById(model.MajorId);
            if (departmentResponse.Status == Message.ERROR)
            {
                return BadRequest("Major not found.");
            }
            var cls = _map.Map<Class>(model);
            cls.Code = model.Code;
            cls.Name = model.Name;
            cls.MajorId = model.MajorId;
            cls.CreatedAt = DateTime.Now;

            // Tạo Major mới
            var createResult = _workflow.Create(cls);
            if (createResult.Status == Message.SUCCESS)
            {
                return Ok(createResult.Result);
            }

            return BadRequest("An error occurred while creating the major.");
            
        }

        [HttpPut("{id}")]
        public IActionResult UpdateClass(UpdateClassPresenter model, int id)
        {
            var existingClassResult = _workflow.FindById(id);
            if (existingClassResult.Status != Message.SUCCESS)
            {
                return NotFound("Class not found.");
            }

            var majorResponse = _uow.Majors.FindOne(model.MajorId);
            if (majorResponse == null)
                return BadRequest("Major not found.");

            var existingClass = existingClassResult.Result as Class;
            if (existingClass == null)
            {
                return NotFound("Class not found.");
            }

            // Update the existing major with new values
            existingClass.Code = model.Code;
            existingClass.Name = model.Name;
            existingClass.UpdatedAt = DateTime.Now;
            existingClass.MajorId = model.MajorId;

            var result = _workflow.Update(existingClass);
            if (result.Status == Message.SUCCESS)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var result = _workflow.Delete(id);
            if (result.Status == Message.SUCCESS)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }

}
