using AutoMapper;
using Project_Keystone.Api.Models.DTOs;
using Project_Keystone.Core.Entities;

namespace Project_Keystone.Infrastructure.Configurations
{
    public class MapperConfig : Profile
    {
        public MapperConfig() {
            CreateMap<User,UserDTO>().ReverseMap(); // <from, to>
        }
    }
}
