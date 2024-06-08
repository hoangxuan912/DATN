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
        CrudMajorFlow workflow;
        private readonly IUnitOfWork uow;
        private readonly IMapper _map;
        public MajorController(IUnitOfWork _uow, IMapper map)
        {
            uow = _uow;
            _map = map;
            workflow = new CrudMajorFlow(uow);
        }
        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAllMajor()
        {
            var result = workflow.List();
            if (result.Status == Message.SUCCESS)
            {
                return Ok(result.Result);
            }

            return BadRequest(result);
        }
        [HttpPost]
        public IActionResult CreateMajor(CreateMajorPresenter model)
        {
            var majorResponse = workflow.FindCodeByName(model.Code);
            if (majorResponse.Status == Message.ERROR)
            {
                return BadRequest("existing code name");
            }
            var departmentResponse = workflow.FindByName(model.DepartmentName);
            if (departmentResponse.Status == Message.ERROR)
            {
                return BadRequest(departmentResponse);
            }

            var department = departmentResponse.Result as Department;
            if (department == null)
            {
                return BadRequest("Invalid department data.");
            }
            var map = _map.Map<Major>(model);
            map.DepartmentId = department.Id;
            map.CreatedAt = DateTime.Now;
            var result = workflow.Create(map);
            if (result.Status == Message.SUCCESS)
            {
                return Ok(result.Result);
            }

            return BadRequest(result);
        }

        [HttpPut]
        public IActionResult UpdateMajor(UpdateMajorPresenter model, string code)
        {
            var map = _map.Map<Major>(model);
            map.UpdatedAt = DateTime.Now;
            var result = workflow.Update(map, code);
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
