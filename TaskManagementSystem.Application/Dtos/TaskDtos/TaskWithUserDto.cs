using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Application.Dtos.UserDtos;

namespace TaskManagementSystem.Application.Dtos.TaskDtos
{
    public class TaskWithUserDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public UserDto CreatedUser { get; set; }
    }
}
