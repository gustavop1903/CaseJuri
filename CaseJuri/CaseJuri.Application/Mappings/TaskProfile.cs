

using AutoMapper;
using CaseJuri.Application.DTOs;
using CaseJuri.Domain.Entities;

namespace CaseJuri.Application.Mappings;

public class TaskProfile : Profile
{
    public TaskProfile()
    {
        CreateMap<CreateToDoTaskRequest, ToDoTask>();
        CreateMap<ToDoTask, ToDoTaskResponseDto>()
            .ForMember(
                dest => dest.Status,
                opt => opt.MapFrom(src => src.Status.ToString())
            );
    }
}
