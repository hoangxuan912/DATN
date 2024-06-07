using asd123.Model;
using asd123.Presenters.Student;
using asd123.Services;
using asd123.Ultil;
using asd123.UseCase.Student.Crud;
using asd123.UseCase.Subject.Crud;
using AutoMapper;
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
    public IActionResult GetAllStudent()
    {
        var result = workflow.List();
        if (result.Status == Message.SUCCESS)
        {
            return Ok(result.Result);
        }

        return BadRequest(result);
    }
    [HttpPost]
    public IActionResult CreateStudent(create_student_presenter model)
    {
        var class_response = workflow.FindByName(model.ClassName);
        if (class_response.Status == Message.ERROR)
        {
            return BadRequest(class_response);
        }

        var _class = class_response.Result as Class;
        if (_class == null)
        {
            return BadRequest("Invalid Class data.");
        }
        var map = _map.Map<Students>(model);
        map.ClassID = _class.Id;
        map.CreatedAt = DateTime.Now;
        var result = workflow.Create(map);
        if (result.Status == Message.SUCCESS)
        {
            return Ok(result.Result);
        }

        return BadRequest(result);
    }
    [HttpPut]
    public IActionResult UpdateStudent(update_student_presenter model, string code)
    {
        var map = _map.Map<Students>(model);
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