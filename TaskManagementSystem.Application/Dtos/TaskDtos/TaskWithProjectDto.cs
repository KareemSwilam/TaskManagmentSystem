using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Application.Dtos.ProjectDtos;

namespace TaskManagementSystem.Application.Dtos.TaskDtos
{
    public class TaskWithProjectDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public ProjectDto Project { get; set; }
    }
}
