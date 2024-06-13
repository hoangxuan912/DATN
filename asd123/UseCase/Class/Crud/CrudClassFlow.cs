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
                var existingClass = _uow.Class.FindAll();
                return new ResponseData(Message.SUCCESS, existingClass);
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
                var existingClass = _uow.Class.GetCodeClass(code);
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
        public ResponseData Update(Model.Class cls)
        {
            try
            {
                _uow.Class.Update(cls);
                _uow.SaveChanges();
                return new ResponseData(Message.SUCCESS, cls);
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
