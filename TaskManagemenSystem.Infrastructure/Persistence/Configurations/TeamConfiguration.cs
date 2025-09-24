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
    public class TeamConfiguration : IEntityTypeConfiguration<Team>
    {
        public void Configure(EntityTypeBuilder<Team> builder)
        {
            builder.HasMany(t => t.TeamUsers)
              .WithOne(tu => tu.Team)
              .HasForeignKey(tu => tu.TeamId);
            builder.Property(p => p.CreatedUserId).HasMaxLength(450);
            builder.HasOne(p => p.CreatedUser)
               .WithMany(u => u.CreatedTeams)
               .HasForeignKey(p => p.CreatedUserId)
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
