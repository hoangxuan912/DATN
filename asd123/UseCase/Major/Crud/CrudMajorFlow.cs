using asd123.Helpers;
using asd123.Services;
using asd123.Ultil;
using Microsoft.EntityFrameworkCore;

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
            var users = unitOfWork.Majors.FindAll();

            return new ResponseData(Message.SUCCESS, users);
        }

        public ResponseData FindByName(string name)
        {
            var existingDepartmen = unitOfWork.Departments.GetCodeDepartment(name);
            if (existingDepartmen == null)
            {
                return new ResponseData(Message.ERROR, "Department not found");
            }
            return new ResponseData(Message.SUCCESS, existingDepartmen);
        }

        public ResponseData Create(asd123.Model.Major major)
        {
            var result = unitOfWork.Majors.Create(major);

            return new ResponseData(Message.SUCCESS, result);
        }
        public ResponseData Update(asd123.Model.Major major, string code)
        {
            var existingMajor = unitOfWork.Majors.GetCodeMajor(code);
            if (existingMajor == null)
            {
                return new ResponseData(Message.ERROR, "Major not found");
            }
            try
            {
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

        }
        public ResponseData Delete(string code)
        {
            var existingDepartmen = unitOfWork.Majors.GetCodeMajor(code);
            if (existingDepartmen == null)
            {
                return new ResponseData(Message.ERROR, "Department not found");
            }
            var result = unitOfWork.Majors.Delete(existingDepartmen.Id);
            return new ResponseData(Message.SUCCESS, result);
        }
    }
}
