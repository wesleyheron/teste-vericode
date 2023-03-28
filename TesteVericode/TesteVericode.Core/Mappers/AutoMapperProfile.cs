using AutoMapper;
using TesteVericode.Domain.DTO;
using TesteVericode.Domain.Entities;

namespace TesteVericode.Core.Mappers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Tarefa, TarefaDTO>();
            CreateMap<CreateTarefaDTO, Tarefa>();
            CreateMap<UpdateTarefaDTO, Tarefa>();
        }
    }
}
