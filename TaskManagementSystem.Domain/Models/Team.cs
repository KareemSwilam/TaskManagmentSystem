using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementSystem.Domain.Models
{
    public class Team
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public int ProjectId { get; set; }
        public string CreatedUserId { get; set; }
        public ApplicationUser CreatedUser { get; set; }
        
        public List<TeamUser> TeamUsers { get; set; }
        public List<TAsk> Tasks { get; set; }
        public Project project { get; set; }
    }
}
