using asd123.Biz.Roles;
using asd123.Model;
using asd123.Presenters.Mark;
using asd123.Services;
using asd123.Ultil;
using asd123.UseCase.Mark.Crud;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace asd123.Controllers;

[Route("api/[controller]")]
[ApiController]
// [Authorize(Roles = UserRoles.User)]
public class MarkController : ControllerBase
{
    CrudMarkFlow workflow;
    private readonly IUnitOfWork uow;
    private readonly IMapper _map;

    public MarkController(IUnitOfWork _uow, IMapper map)
    {
        uow = _uow;
        _map = map;
        workflow = new CrudMarkFlow(uow);
    }

    [HttpGet]
    [Route("GetAll")]
    public IActionResult GetAllMark(int pageNumber = 1, int pageSize = 10)
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
    [Route("get_mark_by_subjectid")]
    public IActionResult GetBySubjectId(int id)
    {
        var result = workflow.FindMarkBySubjectId(id);
        if (result.Status == Message.SUCCESS)
        {
            return Ok(result.Result);
        }

        return BadRequest(result);
    }


    [HttpGet]
    [Route("get_mark_by_studentid")]
    public IActionResult GetByStudentId(int id)
    {
        var result = workflow.FindMarkByStudentId(id);
        if (result.Status == Message.SUCCESS)
        {
            return Ok(result.Result);
        }

        return BadRequest(result);
    }

    [HttpPost]
    public IActionResult CreateMark(create_mark_presenter createMark)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var student = workflow.FindStudentByID(createMark.StudentId);
        if (student.Status == Message.ERROR)
        {
            return NotFound($"Student with ID {createMark.StudentId} not found.");
        }

        var subject = workflow.FindSubjectByID(createMark.SubjectId);
        if (subject.Status == Message.ERROR)
        {
            return NotFound($"Subject with ID {createMark.SubjectId} not found.");
        }

        var existingMarkResult = workflow.FindMarkByStudentAndSubject(createMark.StudentId, createMark.SubjectId);
        if (existingMarkResult.Result != null)
        {
            return Conflict("A mark for this student and subject already exists. Please update the mark if you want to change it.");
        }

        // Create a new mark
        var newMark = new Marks
        {
            StudentId = createMark.StudentId,
            SubjectId = createMark.SubjectId,
            Midterm = createMark.Midterm,
            Final_Exam = createMark.Final_Exam,
            Attendance = createMark.Attendance,
            CreatedAt = DateTime.Now
        };

        var createResult = workflow.Create(newMark);
        if (createResult.Status == Message.SUCCESS)
        {
            return Ok(createResult.Result);
        }

        return BadRequest("An error occurred while creating the mark.");
    }


    [HttpPut("{id}")]
    public IActionResult UpdateMark(update_mark_presenter model, int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var existingMarkResult = workflow.FindById(id);
        if (existingMarkResult.Status != Message.SUCCESS)
        {
            return NotFound("Mark not found.");
        }

        var existingMark = existingMarkResult.Result as Marks;

        if (existingMark == null)
        {
            return NotFound("Mark not found.");
        }

        existingMark.Midterm = model.Midterm;
        existingMark.Final_Exam = model.Final_Exam;
        existingMark.Attendance = model.Attendance;
        existingMark.UpdatedAt = DateTime.Now;

        var result = workflow.Update(existingMark);
        if (result.Status == Message.SUCCESS)
        {
            return Ok(result.Result);
        }

        return BadRequest("An error occurred while updating the mark.");
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