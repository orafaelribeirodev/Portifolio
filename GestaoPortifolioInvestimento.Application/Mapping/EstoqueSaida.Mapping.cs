using AutoMapper;
using GestaoPortifolioInvestimento.Domain;
using GestaoPortifolioInvestimento.Application.Dto;

namespace GestaoPortifolioInvestimento.Application.Mapping
{
    public class EstoqueSaidaMapping : Profile
    {
        public EstoqueSaidaMapping()
        {
            CreateMap<EstoqueSaidaDto, EstoqueSaida>()
                .ForMember(dest => dest.Sai_Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Sai_DtReg, opt => opt.MapFrom(src => src.DataReg))
                .ReverseMap(); ;
        }
    }
}
