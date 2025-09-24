using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementSystem.Application.Dtos.TeamDtos
{
    public class TeamCreateDto
    {
        public string Name { get; set; }
        public int ProjectId { get; set; }
        public string CreatedUserEmail { get; set; }
    }
}
