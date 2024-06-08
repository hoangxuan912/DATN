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

        public ResponseData FindById(int id)
        {
            try
            {
                var existingDepartment = unitOfWork.Departments.FindOne(id);
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

        public ResponseData Update(asd123.Model.Department department)
        {
            try
            {
                unitOfWork.Departments.Update(department);
                unitOfWork.SaveChanges();

                return new ResponseData(Message.SUCCESS, department);
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
                var result = unitOfWork.Departments.Delete(id);
                return new ResponseData(Message.SUCCESS, result);
            }
            catch (Exception ex)
            {
                return new ResponseData(Message.ERROR, $"An error occurred: {ex.Message}");
            }
        }
    }
}
