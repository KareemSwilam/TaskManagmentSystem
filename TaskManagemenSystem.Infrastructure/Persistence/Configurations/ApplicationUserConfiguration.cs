using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Domain.Models;

namespace TaskManagemenSystem.Infrastructure.Persistence.Configurations
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.HasMany(u => u.TeamUsers)
               .WithOne(tu => tu.User)
               .HasForeignKey(tu => tu.UserId);

            builder.HasMany(u => u.TaskUsers)
                   .WithOne(tu => tu.User)
                   .HasForeignKey(tu => tu.UserId);

            builder.HasMany(u => u.CreatedProjects)
                   .WithOne(p => p.CreatedUser)
                   .HasForeignKey(p => p.CreatedUserId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(u => u.CreatedTasks)
                   .WithOne(p => p.CreatedUser)
                   .HasForeignKey(p => p.CreatedUserId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(u => u.CreatedTeams)
                   .WithOne(p => p.CreatedUser)
                   .HasForeignKey(p => p.CreatedUserId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
