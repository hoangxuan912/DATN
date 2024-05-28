using asd123.Model;
using asd123.Presenters.Major;
using asd123.Presenters.Subject;
using asd123.Services;
using asd123.Ultil;
using asd123.UseCase.Major.Crud;
using asd123.UseCase.Subject.Crud;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace asd123.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        CrudSubjectFlow workflow;
        private readonly IUnitOfWork uow;
        private readonly IMapper _map;
        public SubjectController(IUnitOfWork _uow, IMapper map)
        {
            uow = _uow;
            _map = map;
            workflow = new CrudSubjectFlow(uow);
        }
        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAllSubject()
        {
            var result = workflow.List();
            Console.WriteLine(result);
            if (result.Status == Message.SUCCESS)
            {
                return Ok(result.Result);
            }

            return BadRequest(result);
        }
        [HttpPost]
        public IActionResult CreateSubject(CreateSubjectPresenter model)
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
            var map = _map.Map<Subject>(model);
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
        public IActionResult UpdateSubject(UpdateSubjectPresenter model, string code)
        {
            var map = _map.Map<Subject>(model);
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
