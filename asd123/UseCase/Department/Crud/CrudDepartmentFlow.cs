using asd123.Biz.Roles;
using asd123.Biz.Rules;
using asd123.Helpers;
using asd123.Model;
using asd123.Services;
using asd123.Ultil;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace asd123.UseCase.Department.Crud
{
    public class CrudDepartmentFlow
    {
        private readonly IUnitOfWork unitOfWork;
        public CrudDepartmentFlow(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }


        public ResponseData List()
        {
            var users = unitOfWork.Departments.FindAll();
            return new ResponseData(Message.SUCCESS, users);
        }

        public ResponseData Create(asd123.Model.Department department)
        {
            var result =  unitOfWork.Departments.Create(department);
            return new ResponseData(Message.SUCCESS, result);
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

        public ResponseData Update(asd123.Model.Department department,string code)
        {
            var existingDepartment = unitOfWork.Departments.GetCodeDepartment(code);
            if (existingDepartment == null)
            {
                return new ResponseData(Message.ERROR,"Department not found");
            }
            try
            {
                existingDepartment.Code = department.Code;
                existingDepartment.Name = department.Name;
                existingDepartment.Address = department.Address;
                existingDepartment.PhoneNumber = department.PhoneNumber;
                existingDepartment.UpdatedAt = department.UpdatedAt;
                unitOfWork.SaveChanges();
                return new ResponseData(Message.SUCCESS, existingDepartment);
            }
            catch (DbUpdateConcurrencyException)
            {

                return new ResponseData(Message.ERROR, "The entity being updated has been modified by another user. Please reload the entity and try again.");
            }
            
        }

        public ResponseData Delete(string code)
        {
            var existingDepartmen = unitOfWork.Departments.GetCodeDepartment(code);
            if (existingDepartmen == null)
            {
                return new ResponseData(Message.ERROR, "Department not found");
            }
            var result =  unitOfWork.Departments.Delete(existingDepartmen.Id);
            return new ResponseData(Message.SUCCESS, result);
        }
    }
}
