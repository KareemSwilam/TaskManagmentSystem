using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Application.Dtos.TeamDtos;

namespace TaskManagementSystem.Application.Dtos.TaskDtos
{
    public class TaskWithTeamDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public TeamDto Team { get; set; }
    }
}
