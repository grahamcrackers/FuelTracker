using AutoMapper;
using GasTracker.API.Data.DTO;
using GasTracker.API.Data.Models;

public class MappingProfile : Profile {
    public MappingProfile() {
        // Users
        CreateMap<User, UserDTO>()
            .ForMember(dto => dto.Id, user => user.MapFrom(u => u.UserId));
        CreateMap<UserDTO, User>()
            .ForMember(user => user.UserId, dto => dto.MapFrom(d => d.Id));

        // Vehicles
        CreateMap<Vehicle, VehicleDTO>()
            .ForMember(dto => dto.Id, vehicle => vehicle.MapFrom(v => v.VehicleId));
        CreateMap<VehicleDTO, Vehicle>()
            .ForMember(vehicle => vehicle.VehicleId, dto => dto.MapFrom(d => d.Id));

        // Trips
        CreateMap<Trip, TripDTO>()
            .ForMember(dto => dto.Id, trip => trip.MapFrom(v => v.TripId));
        CreateMap<TripDTO, Trip>()
            .ForMember(trip => trip.TripId, dto => dto.MapFrom(d => d.Id));
    }
}