using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Task2.Application.Result;
using TaskManagementSystem.Application.Dtos.AuthDtos;
using TaskManagementSystem.Application.IServices;
using TaskManagementSystem.Domain.Models;

namespace TaskManagemenSystem.Infrastructure.Rspository
{
    public class AuthServices : IAuthServices
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        public AuthServices(UserManager<ApplicationUser> userManager ,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }
        public async Task<CustomResult<string>> Login(LoginDto user)
        {
            var User = await  _userManager.FindByEmailAsync(user.Email);
            if (User == null) return CustomResult<string>.Failure(CustomError.NotFoundError("User OR Password Not Correct"));
            var CheckPassword = await _userManager.CheckPasswordAsync(User, user.Password);
            if(!CheckPassword) return CustomResult<string>.Failure(CustomError.NotFoundError("User OR Password Not Correct"));
            var token = await GenerateToken(User);
            return CustomResult<string>.Success(token);

        }

        public async Task<CustomResult<string>> Registration(RegistrationDto user)
        {
            var ApplicationUser = new ApplicationUser {
                UserName = user.Email,
                FullName = user.FullName,
                Email = user.Email,
            };
            var completed = await _userManager.CreateAsync(ApplicationUser,user.Password);
            if (!completed.Succeeded) return CustomResult<string>.Failure(CustomError.ServerError("Can't Register this User")) ;
            var token = await GenerateToken(ApplicationUser);
            return CustomResult<string>.Success(token);
        }
        public async Task<string> GenerateToken(ApplicationUser user)
        {
            var claim = await GetAllClaim(user);
            var key = _configuration["JWTConfig:Secert"];
            var symmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key!));
            var Crendential = new SigningCredentials(symmetricKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken
            (
                claims: claim,
                signingCredentials: Crendential,
                expires: DateTime.Now.AddDays(1)
            );
            return new JwtSecurityTokenHandler().WriteToken(token);


        }
        public async Task<List<Claim>> GetAllClaim(ApplicationUser user)
        {
            var _option = new IdentityOptions();
            var claims = new List<Claim>
            {
                new Claim("Email",user.Email! ),
                new Claim("Name", user.UserName!),
                new Claim("UserId", Guid.NewGuid().ToString())
            };
            var userCliams = await _userManager.GetClaimsAsync(user);
            claims.AddRange(userCliams);
            var UserRole = await _userManager.GetRolesAsync(user);
            foreach (var userrole in UserRole)
            {
                var role = await _roleManager.FindByNameAsync(userrole);
                if (role != null)
                {
                    var roleClaim = await _roleManager.GetClaimsAsync(role);
                    claims.Add(new Claim("Role", userrole));
                    claims.AddRange(userCliams);
                }

            }
            return claims;
        }

        public async Task<bool> UserExist(string email)
        {
            var UserExist =await  _userManager.FindByEmailAsync(email);
            if (UserExist == null) return false;
            return true;
        }
    }
}
