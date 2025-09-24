using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementSystem.Domain.Models
{
    public class TAsk
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        
        public string CreatedUserId { get; set; }
        public int TeamId { get; set; }
        public int ProjectId { get; set; }
        public ApplicationUser CreatedUser { get; set; }
        public List<TaskUser> TaskUsers { get; set; }
        public Project Project { get; set; }
        public Team Team { get; set; }
    }
}
