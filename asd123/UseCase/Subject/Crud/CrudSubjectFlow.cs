using asd123.Helpers;
using asd123.Services;
using asd123.Ultil;
using Microsoft.EntityFrameworkCore;
using System;

namespace asd123.UseCase.Subject.Crud
{
    public class CrudSubjectFlow
    {
        private readonly IUnitOfWork unitOfWork;

        public CrudSubjectFlow(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public ResponseData List(int pageNumber, int pageSize)
        {
            try
            {
                var subjects = unitOfWork.Subjects.GetInstance();
                var skip = (pageNumber - 1) * pageSize;
                var take = pageSize;
                var subs = subjects.Skip(skip).Take(take).ToList();
                return new ResponseData(Message.SUCCESS, subs);
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
                var existingSubject = unitOfWork.Subjects.GetCodeSubject(code);
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
        public ResponseData FindMajorById(int id)
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
        
        public ResponseData FindById(int id)
        {
            try
            {
                var existingClass = unitOfWork.Subjects.FindOne(id);
                if (existingClass == null)
                {
                    return new ResponseData(Message.ERROR, "Subject not found");
                }
                return new ResponseData(Message.SUCCESS, existingClass);
            }
            catch (Exception ex)
            {
                return new ResponseData(Message.ERROR, $"An error occurred: {ex.Message}");
            }
        }
        
        public ResponseData Create(asd123.Model.Subject subject)
        {
            try
            {
                var result = unitOfWork.Subjects.Create(subject);
                return new ResponseData(Message.SUCCESS, result);
            }
            catch (Exception ex)
            {
                return new ResponseData(Message.ERROR, $"An error occurred: {ex.Message}");
            }
        }

        public ResponseData Update(Model.Subject subject)
        {
            try
            {
                unitOfWork.Subjects.Update(subject);
                unitOfWork.SaveChanges();
                return new ResponseData(Message.SUCCESS, subject);
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
                var result = unitOfWork.Subjects.Delete(id);
                return new ResponseData(Message.SUCCESS, result);
            }
            catch (Exception ex)
            {
                return new ResponseData(Message.ERROR, $"An error occurred: {ex.Message}");
            }
        }
    }
}
