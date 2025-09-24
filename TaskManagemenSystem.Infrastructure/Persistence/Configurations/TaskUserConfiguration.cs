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
    public class TaskUserConfiguration : IEntityTypeConfiguration<TaskUser>
    {
        public void Configure(EntityTypeBuilder<TaskUser> builder)
        {
            builder.Property(p => p.UserId).HasMaxLength(450);
        }
    }
}
