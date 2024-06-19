using asd123.Biz.Roles;
using asd123.Model;
using asd123.Presenters.Major;
using asd123.Presenters.Subject;
using asd123.Services;
using asd123.Ultil;
using asd123.UseCase.Major.Crud;
using asd123.UseCase.Subject.Crud;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace asd123.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = UserRoles.User)]
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
        public IActionResult GetAllSubject(int pageNumber = 1, int pageSize = 10)
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
            if (result.Status == Message.SUCCESS)
            {
                return Ok(result.Result);
            }

            return BadRequest(result);
        }
        
        [HttpGet]
        [Route("get_subject_by_id")]
        public IActionResult GetSubjectById(int id)
        {
            var result = workflow.FindById(id);
            if (result.Status == Message.SUCCESS)
            {
                return Ok(result.Result);
            }

            return BadRequest(result);
        }
        [HttpGet]
        [Route("get_subject_by_code")]
        public IActionResult GetSubjectById(string code)
        {
            var result = workflow.FindByCode(code);
            if (result.Status == Message.SUCCESS)
            {
                return Ok(result.Result);
            }

            return BadRequest(result);
        }
        
        [HttpPost]
        public IActionResult CreateSubject(CreateSubjectPresenter model)
        {
            var subjectResponse = workflow.FindByCode(model.Code);
            if (subjectResponse.Status == Message.SUCCESS)
            {
                return BadRequest("Subject with the same code already exists.");
            }
            
            var majorResponse = workflow.FindMajorById(model.MajorId);
            if (majorResponse.Status == Message.ERROR)
            {
                return BadRequest("Major not found.");
            }
            var sbt = _map.Map<Subject>(model);
            sbt.Code = model.Code;
            sbt.Name = model.Name;
            sbt.MajorId = model.MajorId;
            sbt.CreatedAt = DateTime.Now;

            // Tạo Major mới
            var createResult = workflow.Create(sbt);
            if (createResult.Status == Message.SUCCESS)
            {
                return Ok(createResult.Result);
            }

            return BadRequest("An error occurred while creating the major.");
            
        }

        [HttpPut("{id}")]
        public IActionResult UpdateClass(UpdateSubjectPresenter model, int id)
        {
            var existingSubjectResult = workflow.FindById(id);
            if (existingSubjectResult.Status != Message.SUCCESS)
            {
                return NotFound("Subject not found.");
            }

            var majorResponse = uow.Majors.FindOne(model.MajorId);
            if (majorResponse == null)
                return BadRequest("Major not found.");

            var existingSubject = existingSubjectResult.Result as Subject;
            if (existingSubject == null)
            {
                return NotFound("Subject not found.");
            }

            // Update the existing major with new values
            existingSubject.Code = model.Code;
            existingSubject.Name = model.Name;
            existingSubject.TotalCredits = model.TotalCreadits;
            existingSubject.UpdatedAt = DateTime.Now;
            existingSubject.MajorId = model.MajorId;

            var result = workflow.Update(existingSubject);
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
