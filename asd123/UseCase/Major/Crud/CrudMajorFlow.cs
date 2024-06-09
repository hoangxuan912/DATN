using asd123.Helpers;
using asd123.Services;
using asd123.Ultil;
using Microsoft.EntityFrameworkCore;
using System;

namespace asd123.UseCase.Major.Crud
{
    public class CrudMajorFlow
    {
        private readonly IUnitOfWork unitOfWork;

        public CrudMajorFlow(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public ResponseData List()
        {
            try
            {
                var majors = unitOfWork.Majors.FindAll();
                return new ResponseData(Message.SUCCESS, majors);
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
                var existingMajor = unitOfWork.Majors.GetCodeMajor(code);
                if (existingMajor == null)
                {
                    return new ResponseData(Message.ERROR, "Major not found");
                }
                return new ResponseData(Message.SUCCESS, existingMajor);
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
                var existingMajor = unitOfWork.Majors.FindOne(id);
                if (existingMajor == null)
                {
                    return new ResponseData(Message.ERROR, "Major not found");
                }
                return new ResponseData(Message.SUCCESS, existingMajor);
            }
            catch (Exception ex)
            {
                return new ResponseData(Message.ERROR, $"An error occurred: {ex.Message}");
            }
        }

        public ResponseData Create(asd123.Model.Major major)
        {
            try
            {
                var result = unitOfWork.Majors.Create(major);
                return new ResponseData(Message.SUCCESS, result);
            }
            catch (Exception ex)
            {
                return new ResponseData(Message.ERROR, $"An error occurred: {ex.Message}");
            }
        }

        
        public ResponseData Update(Model.Major major)
        {
            try
            {
                unitOfWork.Majors.Update(major);
                unitOfWork.SaveChanges();
                return new ResponseData(Message.SUCCESS, major);
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
                var result = unitOfWork.Majors.Delete(id);
                return new ResponseData(Message.SUCCESS, result);
            }
            catch (Exception ex)
            {
                return new ResponseData(Message.ERROR, $"An error occurred: {ex.Message}");
            }
        }
    }
}
