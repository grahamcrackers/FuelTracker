using AutoMapper;
using GasTracker.API.Data.DTO;
using GasTracker.API.Data.Models;

public class MappingProfile : Profile {
    public MappingProfile() {
        CreateMap<User, UserDTO>();
        CreateMap<UserDTO, User>();
    }
}