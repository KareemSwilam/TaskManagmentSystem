using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementSystem.Application.Dtos.TeamUserDtos
{
    public class TeamUserUpdateDto
    {
        public int TeamId { get; set; }
        public string UserEmail { get; set; }
        public string NewRole { get; set; }
    }
}
