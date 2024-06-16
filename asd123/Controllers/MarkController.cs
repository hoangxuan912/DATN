using asd123.Model;
using asd123.Presenters.Mark;
using asd123.Services;
using asd123.Ultil;
using asd123.UseCase.Mark.Crud;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace asd123.Controllers;


[Route("api/[controller]")]
[ApiController]
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
    public IActionResult GetAllMark()
    {
        var result = workflow.List();
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

        
        
        var sbt = _map.Map<Marks>(createMark);
        sbt.CreatedAt = DateTime.Now;
        var createResult = workflow.Create(sbt);
        if (createResult.Status == Message.SUCCESS)
        {
            return Ok(createResult.Result);
        }

        return BadRequest("An error occurred while creating the major.");
    }
    [HttpPut("{id}")]
    public IActionResult UpdateMark(update_mark_presenter model, int id)
    {
        var existingMarkResult = workflow.FindById(id);
        if (existingMarkResult.Status != Message.SUCCESS)
        {
            return NotFound("Mark not found.");
        }

        // Update the existing major with new values
        var existingMark = _map.Map<Marks>(model);
        existingMark.UpdatedAt = DateTime.Now;

        var result = workflow.Update(existingMark);
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