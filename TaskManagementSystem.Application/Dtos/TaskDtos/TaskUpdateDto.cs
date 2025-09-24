using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementSystem.Application.Dtos.TaskDtos
{
    public class TaskUpdateDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int ProjectId { get; set; }
        public string CreatedUserEmail { get; set; }
        public int TeamId { get; set; }
    }
}
