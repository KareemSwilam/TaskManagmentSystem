using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Application.Dtos.ProjectDtos;
using TaskManagementSystem.Application.Dtos.TeamDtos;
using TaskManagementSystem.Application.Dtos.UserDtos;

namespace TaskManagementSystem.Application.Dtos.TaskDtos
{
    public class TaskWithAllDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public UserDto CreatedUser { get; set; }
        public TeamDto Team {  get; set; }
        public ProjectDto Project {  get; set; }
    }
}
