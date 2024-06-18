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

    public ResponseData List(int pageNumber, int pageSize)
    {
        try
        {
            var subjects = _uow.Marks.GetInstance();
            var skip = (pageNumber - 1) * pageSize;
            var take = pageSize;
            var sub = subjects.Skip(skip).Take(take).ToList();
            return new ResponseData(Message.SUCCESS, sub);
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

    public ResponseData FindMarkByStudentAndSubject(int studentId, int subjectId)
    {
        try
        {
            var existingMark = _uow.Marks.FindMarkByStudentAndSubject(studentId,subjectId);
            if (existingMark != null)
            {
                return new ResponseData(Message.ERROR, "A mark for this student and subject already exists. Please update the mark if you want to change it.");
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

    public ResponseData FindStudentByID(int id)
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
    public ResponseData FindSubjectByID(int id)
    {
        try
        {
            var existingSubject = _uow.Subjects.FindOne(id);
            if (existingSubject == null)
            {
                return new ResponseData(Message.ERROR, "Subject not found");

            }

            return new ResponseData(Message.SUCCESS, existingSubject);
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