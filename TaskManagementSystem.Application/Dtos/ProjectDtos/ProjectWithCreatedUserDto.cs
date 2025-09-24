using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Application.Dtos.UserDtos;
using TaskManagementSystem.Domain.Models;

namespace TaskManagementSystem.Application.Dtos.ProjectDtos
{
    public class ProjectWithCreatedUserDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public UserDto CreatedUser { get; set; }
    }
}
