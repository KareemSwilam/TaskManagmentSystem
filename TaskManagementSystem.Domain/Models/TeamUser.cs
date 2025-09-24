using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementSystem.Domain.Models
{
    public class TeamUser
    {
        public int Id { get; set; }
        public int TeamId { get; set; }
        public string UserId { get; set; }
        public string Role { get; set; }
        public DateTime JoinedAt { get; set; } 
        public Team Team { get; set; }
        public ApplicationUser User { get; set; }
    }
}
