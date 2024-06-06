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
            if (existingClass == null)
            {
                return new ResponseData(Message.ERROR, "Class not found");
            }
            return new ResponseData(Message.SUCCESS, existingClass);
        }
        catch (Exception ex)
        {
            return new ResponseData(Message.ERROR, $"An error occurred: {ex.Message}");
        }
    }
    public ResponseData Create(asd123.Model.Students sts)
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
    public ResponseData Update(asd123.Model.Students stu, string code)
    {
        try
        {
            var existing_student = _uow.Students.getCodeStudents(code);
            if (existing_student == null)
            {
                return new ResponseData(Message.ERROR, "Student not found");
            }

            existing_student.Code = stu.Code;
            existing_student.Name = stu.Name;
            existing_student.Sex = stu.Sex;
            existing_student.Dob = stu.Dob;
            existing_student.HomeTown = stu.HomeTown;
            existing_student.ContactNumber = stu.ContactNumber;
            existing_student.UpdatedAt = stu.UpdatedAt;
            _uow.SaveChanges();

            return new ResponseData(Message.SUCCESS, existing_student);
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
            var existing_student = _uow.Students.getCodeStudents(code);
            if (existing_student == null)
            {
                return new ResponseData(Message.ERROR, "Student not found");
            }

            var result = _uow.Students.Delete(existing_student.Id);
            return new ResponseData(Message.SUCCESS, result);
        }
        catch (Exception ex)
        {
            return new ResponseData(Message.ERROR, $"An error occurred: {ex.Message}");
        }
    }
}