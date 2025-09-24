using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Application.Dtos.UserDtos;

namespace TaskManagementSystem.Application.Dtos.TeamDtos
{
    public class TeamWithCreatedUserDto
    {
        public string Name { get; set; }
        public UserDto CreatedUser { get; set; }
    }
}
