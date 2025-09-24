using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Application.Dtos.TeamDtos;
using TaskManagementSystem.Application.Dtos.UserDtos;
using TaskManagementSystem.Domain.Models;

namespace TaskManagementSystem.Application.Dtos.TeamUserDtos
{
    public class TeamUserDto
    {
        public UserDto User { get; set; }
        public TeamDto Team { get; set; }
        public DateTime JoinedAt { get; set; }
        
    }
}
