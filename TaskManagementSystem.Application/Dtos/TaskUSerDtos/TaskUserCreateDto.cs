using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementSystem.Application.Dtos.TaskUSerDtos
{
    public class TaskUserCreateDto
    {
        public int TaskId { get; set; }
        public string UserEmail { get; set; }
        //public DateTime DueDate { get; set; }
        //public string Status { get; set; }
        public string Role { get; set; }
    }
}
