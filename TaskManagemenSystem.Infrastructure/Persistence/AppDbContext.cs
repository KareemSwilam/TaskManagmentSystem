using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using TaskManagementSystem.Domain.Models;

namespace TaskManagemenSystem.Infrastructure.Persistence
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Project> Projects { get; set; }
        public DbSet<TAsk> Tasks { get; set; }
        public DbSet<TeamUser> TeamUsers { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<TaskUser> TaskUsers { get; set; }
       
    }

    
    
}
