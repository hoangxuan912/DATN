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
        
        public ResponseData FindByName(string name)
        {
            try
            {
                var existingDepartment = unitOfWork.Departments.GetCodeDepartment(name);
                if (existingDepartment == null)
                {
                    return new ResponseData(Message.ERROR, "Department not found");
                }
                return new ResponseData(Message.SUCCESS, existingDepartment);
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
                var existingDepartment = unitOfWork.Majors.GetCodeMajor(major.Code);
                if (existingDepartment != null)
                {
                    return new ResponseData(Message.SUCCESS, "Major existed");
                }
                var result = unitOfWork.Majors.Create(major);
                return new ResponseData(Message.SUCCESS, result);
            }
            catch (Exception ex)
            {
                return new ResponseData(Message.ERROR, $"An error occurred: {ex.Message}");
            }
        }

        public ResponseData Update(asd123.Model.Major major, string code)
        {
            try
            {
                var existingMajor = unitOfWork.Majors.GetCodeMajor(code);
                if (existingMajor == null)
                {
                    return new ResponseData(Message.ERROR, "Major not found");
                }

                existingMajor.Code = major.Code;
                existingMajor.Name = major.Name;
                existingMajor.UpdatedAt = major.UpdatedAt;
                unitOfWork.SaveChanges();

                return new ResponseData(Message.SUCCESS, existingMajor);
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
                var existingMajor = unitOfWork.Majors.GetCodeMajor(code);
                if (existingMajor == null)
                {
                    return new ResponseData(Message.ERROR, "Major not found");
                }

                var result = unitOfWork.Majors.Delete(existingMajor.Id);
                return new ResponseData(Message.SUCCESS, result);
            }
            catch (Exception ex)
            {
                return new ResponseData(Message.ERROR, $"An error occurred: {ex.Message}");
            }
        }
    }
}
