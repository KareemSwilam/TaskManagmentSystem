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
    public class ProjectConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.HasMany(p => p.Teams)
                   .WithOne(t => t.project)
                   .HasForeignKey(t => t.ProjectId)
                   .OnDelete(DeleteBehavior.Cascade);
            builder.Property(p => p.CreatedUserId).HasMaxLength(450);
            builder.HasOne(p => p.CreatedUser)
               .WithMany(u => u.CreatedProjects)
               .HasForeignKey(p => p.CreatedUserId)
               .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
