using asd123.Model;
using asd123.Presenters.Class;
using asd123.Presenters.Subject;
using asd123.Services;
using asd123.Ultil;
using asd123.UseCase.Class.Crud;
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
        CrudClassFlow workflow;
        private readonly IUnitOfWork uow;
        private readonly IMapper _map;
        public ClassController(IUnitOfWork _uow, IMapper map)
        {
            uow = _uow;
            _map = map;
            workflow = new CrudClassFlow(uow);
        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAllClass()
        {
            var result = workflow.List();
            if (result.Status == Message.SUCCESS)
            {
                return Ok(result.Result);
            }

            return BadRequest(result);
        }

        [HttpPost]
        public IActionResult CreateClass(CreateClassPresenter model)
        {
            var major_response = workflow.FindByName(model.MajorName);
            if (major_response.Status == Message.ERROR)
            {
                return BadRequest(major_response);
            }

            var major = major_response.Result as Major;
            if (major == null)
            {
                return BadRequest("Invalid Major data.");
            }
            var map = _map.Map<Class>(model);
            map.MajorId = major.Id;
            map.CreatedAt = DateTime.Now;
            var result = workflow.Create(map);
            if (result.Status == Message.SUCCESS)
            {
                return Ok(result.Result);
            }

            return BadRequest(result);
        }

        [HttpPut]
        public IActionResult UpdateClass(UpdateSubjectPresenter model, string code)
        {
            var map = _map.Map<Class>(model);
            map.UpdatedAt = DateTime.Now;
            var result = workflow.Update(map, code);
            if (result.Status == Message.SUCCESS)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }

}
