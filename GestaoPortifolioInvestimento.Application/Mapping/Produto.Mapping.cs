using AutoMapper;
using GestaoPortifolioInvestimento.Domain;
using GestaoPortifolioInvestimento.Application.Dto;

namespace GestaoPortifolioInvestimento.Application.Mapping
{
    public class ProdutoMapping : Profile
    {
        public ProdutoMapping()
        {
            CreateMap<ProdutoDto, Produto>()
                .ForMember(dest => dest.Pro_Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Pro_Nm, opt => opt.MapFrom(src => src.Nome))
                .ForMember(dest => dest.Pro_Status, opt => opt.MapFrom(src => src.Status))
                .ReverseMap();;
        }
    }
}
