using Application.TodoCommands;

using AutoMapper;

using Domain;

namespace Application
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<CreateCommand, TodoItem>()
                .ForMember(d => d.UserId, o => o.MapFrom(s => s.Item.UserId))
                .ForMember(d => d.Title, o => o.MapFrom(s => s.Item.Title))
                .ForMember(d => d.IsCompleted, o => o.MapFrom(s => s.Item.IsCompleted))
                .ForMember(d => d.CreatedOn, o => o.MapFrom(s => s.Item.CreatedOn))
                .ForMember(d => d.CompletedAt, o => o.MapFrom(s => s.Item.CompletedAt));
        }
    }
}
