using asd123.Model;
using asd123.Presenters.Department;
using asd123.Presenters.Major;
using asd123.Services;
using asd123.Ultil;
using asd123.UseCase.Department.Crud;
using asd123.UseCase.Major.Crud;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace asd123.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MajorController : ControllerBase
    {
        CrudMajorFlow _workflow;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _map;
        public MajorController(IUnitOfWork _uow, IMapper map)
        {
            _uow = _uow;
            _map = map;
            _workflow = new CrudMajorFlow(_uow);
        }
        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAllMajor()
        {
            var result = _workflow.List();
            if (result.Status == Message.SUCCESS)
            {
                return Ok(result.Result);
            }
            return BadRequest(result);
        }
        [HttpGet]
        [Route("get_major_by_id")]
        public IActionResult GetMajorById(int id)
        {
            var result = _workflow.FindById(id);
            if (result.Status == Message.SUCCESS)
            {
                return Ok(result.Result);
            }

            return BadRequest(result);
        }
        [HttpPost]
        public IActionResult CreateMajor(CreateMajorRequest model)
        {
            var majorResponse = _uow.Majors.GetCodeMajor(model.Code);
            if (majorResponse.Code != "DefaultCode")
            {
                return BadRequest("Major with the same code already exists.");
            }
            
            var departmentResponse = _uow.Departments.FindOne(model.DepartmentId);
            if (departmentResponse == null)
            {
                return BadRequest("Department not found.");
            }
            var major = _map.Map<Major>(model);
            major.Name = model.Name;
            major.DepartmentId = departmentResponse.Id;
            major.CreatedAt = DateTime.Now;

            // Tạo Major mới
            var createResult = _workflow.Create(major);
            if (createResult.Status == Message.SUCCESS)
            {
                return Ok(createResult.Result);
            }

            return BadRequest("An error occurred while creating the major.");
            
        }

        
        [HttpPut("{id}")]
        public IActionResult UpdateMajor(UpdateMajorRequest model, int id)
        {
            var existingMajorResult = _workflow.FindById(id);
            if (existingMajorResult.Status != Message.SUCCESS)
            {
                return NotFound("Major not found.");
            }

            var departmentResponse = _uow.Departments.FindOne(model.DepartmentId);
            if (departmentResponse == null)
                return BadRequest("Department not found.");

            var existingMajor = existingMajorResult.Result as Major;
            if (existingMajor == null)
            {
                return NotFound("Major not found.");
            }

            // Update the existing major with new values
            existingMajor.Code = model.Code;
            existingMajor.Name = model.Name;
            existingMajor.UpdatedAt = DateTime.Now;
            existingMajor.DepartmentId = model.DepartmentId;

            var result = _workflow.Update(existingMajor);
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
