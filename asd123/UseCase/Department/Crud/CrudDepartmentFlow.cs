using asd123.Biz.Roles;
using asd123.Biz.Rules;
using asd123.Helpers;
using asd123.Model;
using asd123.Services;
using asd123.Ultil;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;

namespace asd123.UseCase.Department.Crud
{
    public class CrudDepartmentFlow
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public CrudDepartmentFlow(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public ResponseData List()
        {
            try
            {
                var departments = unitOfWork.Departments.FindAll();
                return new ResponseData(Message.SUCCESS, departments);
            }
            catch (Exception ex)
            {
                return new ResponseData(Message.ERROR, $"An error occurred: {ex.Message}");
            }
        }

        public ResponseData Create(asd123.Model.Department department)
        {
            try
            {
                var result = unitOfWork.Departments.Create(department);
                return new ResponseData(Message.SUCCESS, result);
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

        public ResponseData Update(asd123.Model.Department department, string code)
        {
            try
            {
                var existingDepartment = unitOfWork.Departments.GetCodeDepartment(code);
                if (existingDepartment == null)
                {
                    return new ResponseData(Message.ERROR, "Department not found");
                }

                mapper.Map(department, existingDepartment);  // Use AutoMapper to map properties
                unitOfWork.SaveChanges();

                return new ResponseData(Message.SUCCESS, existingDepartment);
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
                var existingDepartment = unitOfWork.Departments.GetCodeDepartment(code);
                if (existingDepartment == null)
                {
                    return new ResponseData(Message.ERROR, "Department not found");
                }

                var result = unitOfWork.Departments.Delete(existingDepartment.Id);
                return new ResponseData(Message.SUCCESS, result);
            }
            catch (Exception ex)
            {
                return new ResponseData(Message.ERROR, $"An error occurred: {ex.Message}");
            }
        }
    }
}
