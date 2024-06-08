using asd123.Helpers;
using asd123.Services;
using asd123.Ultil;
using Microsoft.EntityFrameworkCore;

namespace asd123.UseCase.Student.Crud;

public class CrudStudentFlow
{
    private readonly IUnitOfWork _uow;

    public CrudStudentFlow(IUnitOfWork uow)
    {
        _uow = uow;
    }
    public ResponseData List()
    {
        try
        {
            var subjects = _uow.Students.FindAll();
            return new ResponseData(Message.SUCCESS, subjects);
        }
        catch (Exception ex)
        {
            return new ResponseData(Message.ERROR, $"An error occurred: {ex.Message}");
        }
    }
    public ResponseData FindByName(string name)
    {
        try
        {
            var existingClass = _uow.Class.GetCodeClass(name);
            return new ResponseData(Message.SUCCESS, existingClass);
        }
        catch (Exception ex)
        {
            return new ResponseData(Message.ERROR, $"An error occurred: {ex.Message}");
        }
    }
    public ResponseData Create(Model.Students sts)
    {
        try
        {
            var result = _uow.Students.Create(sts);
            return new ResponseData(Message.SUCCESS, result);
        }
        catch (Exception ex)
        {
            return new ResponseData(Message.ERROR, $"An error occurred: {ex.Message}");
        }
    }
    public ResponseData Update(Model.Students stu, string code)
    {
        var existingStudent = _uow.Students.getCodeStudents(code);
        try
        {
            existingStudent.Code = stu.Code;
            existingStudent.Name = stu.Name;
            existingStudent.Sex = stu.Sex;
            existingStudent.Dob = stu.Dob;
            existingStudent.HomeTown = stu.HomeTown;
            existingStudent.ContactNumber = stu.ContactNumber;
            existingStudent.UpdatedAt = stu.UpdatedAt;
            _uow.SaveChanges();

            return new ResponseData(Message.SUCCESS, existingStudent);
        }
        catch (DbUpdateConcurrencyException)
        {
            return new ResponseData(Message.ERROR, "The entity being updated has been modified by another user. Please reload the entity and try again.");
        }
        catch (Exception ex)
        {
            return new ResponseData(Message.ERROR, $"An error occurred: {ex.Message}");
        }
    }
    
    public ResponseData Delete(string code)
    {
        try
        {
            var existingStudent = _uow.Students.getCodeStudents(code);

            var result = _uow.Students.Delete(existingStudent.Id);
            return new ResponseData(Message.SUCCESS, result);
        }
        catch (Exception ex)
        {
            return new ResponseData(Message.ERROR, $"An error occurred: {ex.Message}");
        }
    }
}