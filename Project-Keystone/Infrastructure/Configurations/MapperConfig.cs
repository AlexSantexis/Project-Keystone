﻿using AutoMapper;
using Project_Keystone.Api.Models.DTOs;
using Project_Keystone.Core.Entities;

namespace Project_Keystone.Infrastructure.Configurations
{
    public class MapperConfig : Profile
    {
        public MapperConfig() {
            CreateMap<User,UserLoginDTO>().ReverseMap();

            CreateMap<UserRegisterDTO,User>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email))
                .ReverseMap();


            CreateMap<UserUpdateDTO, User>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.NormalizedEmail, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.NormalizedUserName, opt => opt.MapFrom(src => src.Email))
                .ReverseMap();

            CreateMap<User, UserDTO>().ReverseMap();
        }
    }
}
