using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementSystem.Domain.Models
{
    public class ApplicationUser: IdentityUser
    {
        public string FullName { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public List<TeamUser> TeamUsers { get; set; }
        public List<TaskUser> TaskUsers { get; set; }
        public List<Project> CreatedProjects { get; set; }
        public List<TAsk> CreatedTasks { get; set; }
        public List<Team> CreatedTeams { get; set; }
        
        

    }
}
