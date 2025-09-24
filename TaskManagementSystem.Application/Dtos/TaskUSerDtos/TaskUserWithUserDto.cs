using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Application.Dtos.TaskDtos;
using TaskManagementSystem.Application.Dtos.UserDtos;

namespace TaskManagementSystem.Application.Dtos.TaskUSerDtos
{
    public class TaskUserWithUserDto
    {
        public UserDto User { get; set; }
        public DateTime DueDate { get; set; }
        public string Status { get; set; }
        public string Role { get; set; }
        public DateTime AssignedAt { get; set; }
    }
}
