using asd123.Biz.Roles;
using asd123.Model;
using asd123.Presenters.Department;
using asd123.Presenters.Major;
using asd123.Services;
using asd123.Ultil;
using asd123.UseCase.Department.Crud;
using asd123.UseCase.Major.Crud;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace asd123.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = UserRoles.User)]
    public class MajorController : ControllerBase
    {
        CrudMajorFlow _workflow;
        private CrudDepartmentFlow _departmentFlow;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _map;
        public MajorController(IUnitOfWork _uow, IMapper map)
        {
            _uow = _uow;
            _map = map;
            _workflow = new CrudMajorFlow(_uow);
            _departmentFlow = new CrudDepartmentFlow(_uow);
        }
        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAllMajor(int pageNumber = 1, int pageSize = 10)
        {
            if (pageNumber <= 0)
            {
                pageNumber = 1;
            }

            if (pageSize <= 0)
            {
                pageSize = 10;
            }
            var result = _workflow.List(pageNumber, pageSize);
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
        [HttpGet]
        [Route("get_major_by_code")]
        public IActionResult GetMajorById(string code)
        {
            var result = _workflow.FindByCode(code);
            if (result.Status == Message.SUCCESS)
            {
                return Ok(result.Result);
            }

            return BadRequest(result);
        }
        [HttpPost]
        public IActionResult CreateMajor(CreateMajorRequest model)
        {
            var majorResponse = _workflow.FindByCode(model.Code);
            if (majorResponse.Status == Message.SUCCESS)
            {
                return BadRequest("Major with the same code already exists.");
            }
            
            var departmentResponse = _departmentFlow.FindById(model.DepartmentId);
            if (departmentResponse.Status == Message.ERROR)
            {
                return BadRequest("Department not found.");
            }
            var major = _map.Map<Major>(model);
            major.Code = model.Code;
            major.Name = model.Name;
            major.DepartmentId = model.DepartmentId;
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

            var departmentResponse = _workflow.FindDepartmentById(model.DepartmentId);
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
