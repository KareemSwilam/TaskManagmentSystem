using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Application.Dtos.TaskDtos;

namespace TaskManagementSystem.Application.Dtos.TaskUSerDtos
{
    public class TaskUserWithTaskDto
    {
        public TaskDto Task { get; set; }
        public DateTime DueDate { get; set; }
        public string Status { get; set; }
        public string Role { get; set; }
        public DateTime AssignedAt { get; set; }
    }
}
