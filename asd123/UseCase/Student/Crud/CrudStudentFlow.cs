using asd123.Helpers;
using asd123.Model;
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
    public ResponseData FindByCode(string code)
    {
        try
        {
            var existingStudent = _uow.Students.getCodeStudents(code);
            if (existingStudent == null)
            {
                return new ResponseData(Message.ERROR, "Student not found");
            }
            
            return new ResponseData(Message.SUCCESS, existingStudent);
        }
        catch (Exception ex)
        {
            return new ResponseData(Message.ERROR, $"An error occurred: {ex.Message}");
        }
    }
    
    public ResponseData FindClassById(int id)
    {
        try
        {
            var existingClass = _uow.Class.FindOne(id);
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
    
    public ResponseData FindById(int id)
    {
        try
        {
            var existingStudent = _uow.Students.FindOne(id);
            if (existingStudent == null)
            {
                return new ResponseData(Message.ERROR, "Student not found");
            }
            return new ResponseData(Message.SUCCESS, existingStudent);
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
    public ResponseData Update(Model.Students stu)
    {
        try
        {
            _uow.Students.Update(stu);
            _uow.SaveChanges();
            return new ResponseData(Message.SUCCESS, stu);
        }
        catch (Exception ex)
        {
            return new ResponseData(Message.ERROR, $"An error occurred: {ex.Message}");
        }
    }

    
    public ResponseData Delete(int id)
    {
        try
        {
            var result = _uow.Students.Delete(id);
            return new ResponseData(Message.SUCCESS, result);
        }
        catch (Exception ex)
        {
            return new ResponseData(Message.ERROR, $"An error occurred: {ex.Message}");
        }
    }
}