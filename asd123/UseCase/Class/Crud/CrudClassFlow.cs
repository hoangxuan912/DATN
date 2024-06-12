using asd123.Helpers;
using asd123.Services;
using asd123.Ultil;
using Microsoft.EntityFrameworkCore;

namespace asd123.UseCase.Class.Crud
{
    public class CrudClassFlow
    {
        private readonly IUnitOfWork _uow;
        public CrudClassFlow(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public ResponseData List()
        {
            try
            {
                var subjects = _uow.Class.FindAll();
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
                var existing_major = _uow.Majors.GetCodeMajor(name);
                if (existing_major == null)
                {
                    return new ResponseData(Message.ERROR, "Major not found");
                }
                return new ResponseData(Message.SUCCESS, existing_major);
            }
            catch (Exception ex)
            {
                return new ResponseData(Message.ERROR, $"An error occurred: {ex.Message}");
            }
        }
        public ResponseData Create(asd123.Model.Class cls)
        {
            try
            {
                var result = _uow.Class.Create(cls);
                return new ResponseData(Message.SUCCESS, result);
            }
            catch (Exception ex)
            {
                return new ResponseData(Message.ERROR, $"An error occurred: {ex.Message}");
            }
        }
        public ResponseData Update(asd123.Model.Class cls, string code)
        {
            try
            {
                var existing_class = _uow.Class.GetCodeClass(code);
                if (existing_class == null)
                {
                    return new ResponseData(Message.ERROR, "Class not found");
                }

                existing_class.Code = cls.Code;
                existing_class.Name = cls.Name;
                existing_class.UpdatedAt = cls.UpdatedAt;
                _uow.SaveChanges();

                return new ResponseData(Message.SUCCESS, existing_class);
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
        public ResponseData Delete(int id)
        {
            try
            {
                var result = _uow.Class.Delete(id);
                return new ResponseData(Message.SUCCESS, result);
            }
            catch (Exception ex)
            {
                return new ResponseData(Message.ERROR, $"An error occurred: {ex.Message}");
            }
        }
    }
}
