using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task2.Application.Result;
using TaskManagementSystem.Application.Dtos.AuthDtos;
using TaskManagementSystem.Domain.Models;

namespace TaskManagementSystem.Application.IServices
{
    public interface IAuthServices
    {
        public Task<bool> UserExist(string email);
        public Task<CustomResult<string>> Login(LoginDto user);
        public Task<CustomResult<string>> Registration(RegistrationDto user);

    }
}
