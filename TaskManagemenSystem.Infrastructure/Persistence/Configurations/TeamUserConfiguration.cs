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
    public class TeamUserConfiguration : IEntityTypeConfiguration<TeamUser>
    {
        public void Configure(EntityTypeBuilder<TeamUser> builder)
        {
            builder.Property(p => p.UserId).HasMaxLength(450);
            builder.Property(p => p.JoinedAt).HasDefaultValue(DateTime.UtcNow);
        }
    }
}
