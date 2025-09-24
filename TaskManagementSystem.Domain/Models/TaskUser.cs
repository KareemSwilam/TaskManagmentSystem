using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementSystem.Domain.Models
{
    public class TaskUser
    {
        public int Id { get; set; }
        public int TaskId { get; set; }
        public string UserId { get; set; }
        public DateTime DueDate { get; set; }
        public string Status { get; set; }
        public string Role { get; set; }
        public DateTime AssignedAt { get; set; }
        public TAsk Task { get; set; }
        public ApplicationUser User { get; set; }
    }
}
