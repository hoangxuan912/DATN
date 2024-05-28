using asd123.Helpers;
using asd123.Services;
using asd123.Ultil;
using Microsoft.EntityFrameworkCore;

namespace asd123.UseCase.Subject.Crud
{
    public class CrudSubjectFlow
    {
        private readonly IUnitOfWork unitOfWork;
        public CrudSubjectFlow(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public ResponseData List()
        {
            var users = unitOfWork.Subjects.FindAll();

            return new ResponseData(Message.SUCCESS, users);
        }
        public ResponseData FindByName(string name)
        {
            var existingDepartmen = unitOfWork.Majors.GetCodeMajor(name);
            if (existingDepartmen == null)
            {
                return new ResponseData(Message.ERROR, "Major not found");
            }
            return new ResponseData(Message.SUCCESS, existingDepartmen);
        }
        public ResponseData Create(asd123.Model.Subject subject)
        {
            var result = unitOfWork.Subjects.Create(subject);

            return new ResponseData(Message.SUCCESS, result);
        }
        public ResponseData Update(asd123.Model.Subject subject, string code)
        {
            var existing_subject = unitOfWork.Subjects.GetCodeSubject(code);
            if (existing_subject == null)
            {
                return new ResponseData(Message.ERROR, "Subject not found");
            }
            try
            {
                existing_subject.Code = subject.Code;
                existing_subject.Name = subject.Name;
                existing_subject.TotalCreadits = subject.TotalCreadits;
                existing_subject.UpdatedAt = subject.UpdatedAt;
                unitOfWork.SaveChanges();
                return new ResponseData(Message.SUCCESS, existing_subject);
            }
            catch (DbUpdateConcurrencyException)
            {

                return new ResponseData(Message.ERROR, "The entity being updated has been modified by another user. Please reload the entity and try again.");
            }

        }
        public ResponseData Delete(string code)
        {
            var existing_subject = unitOfWork.Subjects.GetCodeSubject(code);
            if (existing_subject == null)
            {
                return new ResponseData(Message.ERROR, "Subject  not found");
            }
            var result = unitOfWork.Subjects.Delete(existing_subject.Id);
            return new ResponseData(Message.SUCCESS, result);
        }
    }
}
