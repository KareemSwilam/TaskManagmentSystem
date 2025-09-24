using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Application.Dtos.UserDtos;
using TaskManagementSystem.Domain.Models;

namespace TaskManagementSystem.Application.Mapping
{
    public class MappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<ApplicationUser, UserDto>();
        }
    }
}
