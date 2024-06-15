using asd123.Helpers;
using asd123.Services;
using asd123.Ultil;

namespace asd123.UseCase.Mark.Crud;

public class CrudMarkFlow
{
    private readonly IUnitOfWork _uow;

    public CrudMarkFlow(IUnitOfWork uow)
    {
        _uow = uow;
    }
    public ResponseData List()
    {
        try
        {
            var subjects = _uow.Subjects.FindAll();
            return new ResponseData(Message.SUCCESS, subjects);
        }
        catch (Exception ex)
        {
            return new ResponseData(Message.ERROR, $"An error occurred: {ex.Message}");
        }
    }
    public ResponseData FindMarkBySubjectId(int id)
    {
        try
        {
            var existingMark = _uow.Marks.getAllMarkBySubjectId(id);
            if (existingMark == null)
            {
                return new ResponseData(Message.ERROR, "Mark not found");
            }
            return new ResponseData(Message.SUCCESS, existingMark);
        }
        catch (Exception ex)
        {
            return new ResponseData(Message.ERROR, $"An error occurred: {ex.Message}");
        }
    }
    public ResponseData FindMarkByStudentId(int id)
    {
        try
        {
            var existingMark = _uow.Marks.getAllMarkByStudentId(id);
            if (existingMark == null)
            {
                return new ResponseData(Message.ERROR, "Mark not found");
            }
            return new ResponseData(Message.SUCCESS, existingMark);
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
            var existingMark = _uow.Marks.FindOne(id);
            if (existingMark == null)
            {
                return new ResponseData(Message.ERROR, "Mark not found");
            }
            return new ResponseData(Message.SUCCESS, existingMark);
        }
        catch (Exception ex)
        {
            return new ResponseData(Message.ERROR, $"An error occurred: {ex.Message}");
        }
    }
    public ResponseData Create(asd123.Model.Marks mrk)
    {
        try
        {
            var result = _uow.Marks.Create(mrk);
            return new ResponseData(Message.SUCCESS, result);
        }
        catch (Exception ex)
        {
            return new ResponseData(Message.ERROR, $"An error occurred: {ex.Message}");
        }
    }
    public ResponseData Update(Model.Marks mrk)
    {
        try
        {
            _uow.Marks.Update(mrk);
            _uow.SaveChanges();
            return new ResponseData(Message.SUCCESS, mrk);
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
            var result = _uow.Marks.Delete(id);
            return new ResponseData(Message.SUCCESS, result);
        }
        catch (Exception ex)
        {
            return new ResponseData(Message.ERROR, $"An error occurred: {ex.Message}");
        }
    }
}