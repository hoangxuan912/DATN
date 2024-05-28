using asd123.Biz.Rules;
using asd123.Helpers;
using asd123.Model;
using asd123.Services;
using asd123.Ultil;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace asd123.UseCase.Seed
{
    //public class DatabaseSeeder
    //{
    //    private readonly IUnitOfWork uow;
    //    public DatabaseSeeder(IUnitOfWork _uow)
    //    {
    //        uow = _uow;
    //    }
    //    public async Task<Response> Seed()
    //    {
    //        bool isSeeded = false;
    //        //var executionStrategy = uow.CreateExecutionStrategy();

    //        //await executionStrategy.Execute(async () =>
    //        //{
    //        //    using IDbContextTransaction transaction = uow.BeginTransaction();
    //        //    try
    //        //    {
    //        //        SeedRoles();
    //        //        SeedUsers();
    //        //        SeedUserRoles();
    //        //        uow.SaveChanges();
    //        //        isSeeded = true;
    //        //        transaction.Commit();
    //        //    }
    //        //    catch (Exception ex)
    //        //    {
    //        //        isSeeded = false;
    //        //        transaction.Rollback();
    //        //        Console.WriteLine($"Error seeding database: {ex.Message}");
    //        //        throw;
    //        //    }
    //        //});
    //        SeedRoles();
    //        SeedUsers();
    //        SeedUserRoles();
    //        isSeeded = false;
    //        Response response = new Response("success", isSeeded);
    //        Console.WriteLine(response.ToString());
    //        return response;
    //    }


    //    private void SeedRoles()
    //    {
    //        var roles = new List<RoleSchema>
    //        {
    //            new RoleSchema { RoleName = "Admin", Description = "Administrator role with full permissions" },
    //            new RoleSchema { RoleName = "User", Description = "Regular user with standard permissions" },
    //            new RoleSchema { RoleName = "Manager", Description = "Manager role with elevated permissions" }
    //        };

    //        foreach (var role in roles)
    //        {
    //            var existingRole = uow.Roles.FindByName(role.RoleName);
    //            if (existingRole == null)
    //            {
    //                uow.Roles.Create(role);
    //            }
    //        }
    //        uow.Commit();
    //    }


    //    private void SeedUsers()
    //    {
    //        string defaultpw = JwtUtil.MD5Hash(UserRule.DEFAULT_PASSWORD);
    //        var users = new List<applicationUser>
    //        {
    //            new applicationUser { UserName = "admin", Password = defaultpw, Email = "admin@example.com" },
    //            new applicationUser { UserName = "user", Password = defaultpw, Email = "user@example.com" },
    //            new applicationUser { UserName = "manager", Password = defaultpw, Email = "manager@example.com" }
    //        };

    //        foreach (var user in users)
    //        {
    //            var existingUser = uow.Users.FindByName(user.UserName).FirstOrDefault();
    //            if (existingUser == null)
    //            {
    //                uow.Users.Create(user);
    //            }
    //        }
    //        uow.Commit();

    //    }
    //    private void SeedUserRoles()
    //    {
    //        var userRoles = new List<UserRoleSchema>
    //        {
    //            new UserRoleSchema { UserId = uow.Users.FindByName("admin").First().Id, RoleId = uow.Roles.FindByName("Admin").Id },
    //            new UserRoleSchema { UserId = uow.Users.FindByName("user").First().Id, RoleId = uow.Roles.FindByName("User").Id },
    //            new UserRoleSchema { UserId = uow.Users.FindByName("manager").First().Id, RoleId = uow.Roles.FindByName("Manager").Id }
    //        };

    //        foreach (var userRole in userRoles)
    //        {
    //            var existingUserRole = uow.UserRoles.GetUserRole(userRole.UserId, userRole.RoleId);
    //            if (existingUserRole == null)
    //            {
    //                uow.UserRoles.Create(userRole);
    //            }
    //        }
    //        uow.Commit();

    //    }
    //}
}
