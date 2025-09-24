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
    public class TaskConfiguration : IEntityTypeConfiguration<TAsk>
    {
        public void Configure(EntityTypeBuilder<TAsk> builder)
        {
            builder.HasMany(t => t.TaskUsers)
               .WithOne(tu => tu.Task)
               .HasForeignKey(tu => tu.TaskId);
            builder.Property(p => p.CreatedUserId).HasMaxLength(450);
            builder.HasOne(p => p.CreatedUser)
               .WithMany(u => u.CreatedTasks)
               .HasForeignKey(p => p.CreatedUserId)
               .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(t => t.Project)
               .WithMany(p => p.Tasks)
               .HasForeignKey(t => t.ProjectId)
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
