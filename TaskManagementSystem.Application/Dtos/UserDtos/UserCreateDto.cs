using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementSystem.Application.Dtos.UserDtos
{
    public class UserCreateDto
    {
       public string Name { get; set; }
        public string Email { get; set; }
       public string Role { get; set; } 
    }
}
