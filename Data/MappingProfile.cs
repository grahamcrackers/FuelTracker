using AutoMapper;
using GasTracker.Data.DTO;
using GasTracker.Data.Models;

public class MappingProfile : Profile {
    public MappingProfile() {
        CreateMap<User, UserDTO>();
        CreateMap<UserDTO, User>();
    }
}