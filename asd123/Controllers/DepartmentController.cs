using asd123.Model;
using asd123.Presenters.Department;
using asd123.Services;
using asd123.Ultil;
using asd123.UseCase.Department.Crud;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace asd123.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        [Route("GetAll")]
        public IActionResult GetAllDepartment()
        {
            var result = workflow.List();
            if(result.Status == Message.SUCCESS)
            {   
                return Ok(result.Result);
            }

            return BadRequest(result);
        }
        [HttpGet]
        [Route("GetDepartmentByName")]
        public IActionResult GetDepartmentByName(string name)
        {
            var result = workflow.FindByName(name);
            if (result.Status == Message.SUCCESS)
            {
                return Ok(result.Result);
            }

            return BadRequest(result);
        }

        [HttpPost]
        public IActionResult CreateDepartment(CreateDepartmentPresenter model)
        {
            var map = _map.Map<Department>(model);
            map.CreatedAt = DateTime.Now;
            var result = workflow.Create(map);
            if(result.Status == Message.SUCCESS )
            {
                return Ok(result.Result);
            }

            return BadRequest(result);
        }

        [HttpPut]
        public IActionResult UpdateDepartment(CreateDepartmentPresenter model, string code)
        {
            var map = _map.Map<Department>(model);
            map.UpdatedAt = DateTime.Now;
            var result = workflow.Update(map, code);
            if (result.Status == Message.SUCCESS)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpDelete]
        public IActionResult Delete(string code)
        {
            var result = workflow.Delete(code);
            if (result.Status == Message.SUCCESS)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}
