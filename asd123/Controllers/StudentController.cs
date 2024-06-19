using asd123.Biz.Roles;
using asd123.Model;
using asd123.Presenters.Student;
using asd123.Services;
using asd123.Ultil;
using asd123.UseCase.Student.Crud;
using asd123.UseCase.Subject.Crud;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace asd123.Controllers;
[Route("api/[controller]")]
[ApiController]
public class StudentController : Controller
{
    CrudStudentFlow workflow;
    private readonly IUnitOfWork _uow;
    private readonly IMapper _map;
    public StudentController(IUnitOfWork _uow, IMapper map)
    {
        _uow = _uow;
        _map = map;
        workflow = new CrudStudentFlow(_uow);
    }
    [HttpGet]
    [Route("GetAll")]
    [Authorize(Roles = UserRoles.User)]
    public IActionResult GetAllStudent(int pageNumber = 1, int pageSize = 10)
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
    [Route("get_student_by_id")]
    public IActionResult GetStudentById(int id)
    {
        var result = workflow.FindById(id);
        if (result.Status == Message.SUCCESS)
        {
            return Ok(result.Result);
        }

        return BadRequest(result);
    }
    
    [HttpGet]
    [Route("get_student_by_code")]
    public IActionResult GetStudentById(string code)
    {
        var result = workflow.FindByCode(code);
        if (result.Status == Message.SUCCESS)
        {
            return Ok(result.Result);
        }

        return BadRequest(result);
    }
    
    [HttpPost]
    public IActionResult CreateStudent(create_student_presenter model)
    {
        var class_response = workflow.FindByCode(model.Code);
        if (class_response.Status == Message.SUCCESS)
        {
            return BadRequest("Student with the same code already exists.");
        }

        var _class = workflow.FindClassById(model.ClassId);
        if (_class.Status == Message.ERROR)
        {
            return BadRequest("Class not found.");
        }
        var stt = _map.Map<Students>(model);
        stt.Code = model.Code;
        stt.Name = model.Name;
        stt.Sex = model.Sex;
        stt.Dob = model.Dob;
        stt.HomeTown = model.HomeTown;
        stt.ContactNumber = model.ContactNumber;
        stt.ClassID = model.ClassId;
        stt.CreatedAt = DateTime.Now;
        var result = workflow.Create(stt);
        if (result.Status == Message.SUCCESS)
        {
            return Ok(result.Result);
        }

        return BadRequest("An error occurred while creating the student.");
    }
    [HttpPut("{id}")]
    public IActionResult UpdateStudent(update_student_presenter model, int id)
    {
        var existingStudentResult = workflow.FindById(id);
        if (existingStudentResult.Status != Message.SUCCESS)
        {
            return NotFound("Student not found.");
        }
        
        var classResponse = workflow.FindClassById(model.ClassId);
        if (classResponse.Status == Message.ERROR)
            return BadRequest("class not found.");
        
        var existingStudent = existingStudentResult.Result as Students;
        if (existingStudent == null)
        {
            return NotFound("Student not found.");
        }
        //var map = _map.Map<Students>(model);
        existingStudent.Code = model.Code;
        existingStudent.Name = model.Name;
        existingStudent.Sex = model.Sex;
        existingStudent.Dob = model.Dob;
        existingStudent.HomeTown = model.HomeTown;
        existingStudent.ContactNumber = model.ContactNumber;
        existingStudent.ClassID = model.ClassId;
        existingStudent.UpdatedAt = DateTime.Now;
        var result = workflow.Update(existingStudent);
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