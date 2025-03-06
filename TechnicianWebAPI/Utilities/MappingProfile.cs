using AutoMapper;
using TmsWebApi.Models;
using TechnicianWebAPI.DTOs;

// This class defines the mappings between the source and destination objects using AutoMapper
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, AddTechnicianDto>().ReverseMap();
        CreateMap<User, LoginUserDto>().ReverseMap();

        CreateMap<Tasks, AddTaskDto>().ReverseMap();
        CreateMap<Tasks, TaskDto>().ReverseMap();
        CreateMap<Tasks, UpdateTaskDto>().ReverseMap();
    }
}
