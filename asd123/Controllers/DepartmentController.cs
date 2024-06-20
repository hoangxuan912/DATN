using asd123.Biz.Roles;
using asd123.Model;
using asd123.Presenters.Department;
using asd123.Services;
using asd123.Ultil;
using asd123.UseCase.Department.Crud;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace asd123.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // [Authorize(Roles = UserRoles.User)]
    public class DepartmentController : ControllerBase
    {
        CrudDepartmentFlow workflow;
        private readonly IUnitOfWork uow;
        private readonly IMapper _map;
        public DepartmentController(IUnitOfWork _uow,IMapper map)
        {
            uow = _uow;
            _map = map;
            workflow = new CrudDepartmentFlow(uow);
        }
        [HttpGet]
        [Route("get_all_department")]
        public IActionResult GetAllDepartment(int pageNumber = 1, int pageSize = 10)
        {
            if (pageNumber <= 0)
            {
                pageNumber = 1;
            }

            if (pageSize <= 0)
            {
                pageSize = 10;
            }
            var result = workflow.List(pageNumber, pageSize);
            if(result.Status == Message.SUCCESS)
            {   
                return Ok(result.Result);
            }

            return BadRequest(result);
        }
        [HttpGet]
        [Route("get_department_by_id")]
        public IActionResult GetDepartmentById(int id)
        {
            var result = workflow.FindById(id);
            if (result.Status == Message.SUCCESS)
            {
                return Ok(result.Result);
            }

            return BadRequest(result);
        }
        [HttpGet]
        [Route("get_department_by_code")]
        public IActionResult GetDepartmentByCode(string code)
        {
            var result = workflow.FindByCode(code);
            if (result.Status == Message.SUCCESS)
            {
                return Ok(result.Result);
            }

            return BadRequest(result);
        }

        [HttpPost]
        public IActionResult CreateDepartment(CreateDepartmentRequest model)
        {
            var existingDepartment = uow.Departments.GetCodeDepartment(model.Code);
            if (existingDepartment != null)
            {
                return BadRequest("Department with the same code already exists.");
            }
            var map = _map.Map<Department>(model);
            map.CreatedAt = DateTime.Now;
            var result = workflow.Create(map);
            if(result.Status == Message.SUCCESS )
            {
                return Ok(result.Result);
            }

            return BadRequest(result);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateDepartment(CreateDepartmentRequest model, int id)
        {
            var existingDepartmentResult = workflow.FindById(id);
            if (existingDepartmentResult.Status != Message.SUCCESS)
            {
                return NotFound("Department not found.");
            }
    

            var map = _map.Map<Department>(model);
            map.Id = id; 
            map.UpdatedAt = DateTime.Now;

            var result = workflow.Update(map);
            if (result.Status == Message.SUCCESS)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }



        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var result = workflow.Delete(id);
            if (result.Status == Message.SUCCESS)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}
