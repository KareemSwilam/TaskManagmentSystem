using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task2.Application.Result;
using TaskManagementSystem.Application.IServices;
using TaskManagementSystem.Domain.Models;

namespace TaskManagemenSystem.Infrastructure.Rspository
{
    public class SetupServices : ISetupServices
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public SetupServices(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;            
        }
        public async Task<CustomResult> AddRole(string name)
        {
            var roleExist = await _roleManager.RoleExistsAsync(name);
            if (roleExist) 
                return CustomResult.Failure(CustomError.ValidationError("Role Already Exist")) ;
            var role = new IdentityRole
            {
                Name = name,
            };
            var result = await _roleManager.CreateAsync(role);
            if (!result.Succeeded)
                return CustomResult.Failure(CustomError.ValidationError("Failed To Add Role "));
            return CustomResult.Success(); 

        }

        public async  Task<CustomResult> AddRoleToUser(string userEmail, string roleName)
        {
            var roleExist = await _roleManager.RoleExistsAsync(roleName);
            if (!roleExist)
                return CustomResult.Failure(CustomError.ValidationError("The Role You Try To Assign Not Exist"));
            var user = await _userManager.FindByEmailAsync(userEmail);
            if (user == null)
                return CustomResult.Failure(CustomError.NotFoundError("User Not Found"));
            var result = await _userManager.AddToRoleAsync(user, roleName);
            if(!result.Succeeded)
                return CustomResult.Failure(CustomError.ValidationError("Failed To Assign Role To User"));
            return CustomResult.Success();

        }

        public async  Task<CustomResult<bool>> CheckUserInRole(string userEmail, string role)
        {
            var roleExist = await _roleManager.RoleExistsAsync(role);
            if (!roleExist)
                return CustomResult<bool>.Failure(CustomError.NotFoundError("The Role Not Exist"));
            var user = await _userManager.FindByEmailAsync(userEmail);
            if (user == null)
                return CustomResult<bool>.Failure(CustomError.NotFoundError("User Not Found"));
            var result = await _userManager.IsInRoleAsync(user, role);
            if (result)
                return CustomResult<bool>.Failure(CustomError.NotFoundError("User not Have this role "));
            return CustomResult<bool>.Success(result);

        }

        public async Task<CustomResult<List<string>>> GetRoles()
        {
            var result = await _roleManager.Roles.Select(r => r.Name).ToListAsync();
            if (result.Count == 0)
            {
                return  CustomResult<List<string>>.Failure(CustomError.NotFoundError("No Rule Found"));
            }
            return CustomResult<List<string>>.Success(result!);
        }

        public async Task<CustomResult<IList<string>>> GetUserRoles(string userEmail)
        {
            var user = await _userManager.FindByEmailAsync(userEmail);
            if (user == null)
                return CustomResult<IList<string>>.Failure(CustomError.NotFoundError("User Not Found"));
            var result = await _userManager.GetRolesAsync(user);
            return CustomResult<IList<string>>.Success(result);
        }

        public async Task<CustomResult> RemoveRole(string name)
        {
            var role = await _roleManager.FindByNameAsync(name);
            if (role == null)
                return CustomResult.Failure(CustomError.NotFoundError("Role Not Found"));
            var result = await _roleManager.DeleteAsync(role);
            if (!result.Succeeded)
                return CustomResult.Failure(CustomError.ValidationError("Failed To Delete Role "));
            return CustomResult.Success();
        }

        public async Task<CustomResult> RemoveRoleFromUser(string userEmail, string roleName)
        {
            var roleExist = await _roleManager.RoleExistsAsync(roleName);
            if (!roleExist)
                return CustomResult.Failure(CustomError.NotFoundError("The Role You Try To Remove Not Found"));
            var user = await _userManager.FindByEmailAsync(userEmail);
            if (user == null)
                return CustomResult.Failure(CustomError.NotFoundError("User Not Found"));
            var result = await _userManager.RemoveFromRoleAsync(user, roleName);
            if (!result.Succeeded)
                return CustomResult.Failure(CustomError.ValidationError("Failed To Rome User From Role "));
            return CustomResult.Success();
        }
    }
}
