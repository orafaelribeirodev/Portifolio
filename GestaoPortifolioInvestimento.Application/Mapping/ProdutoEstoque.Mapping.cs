using AutoMapper;
using GestaoPortifolioInvestimento.Application.Dto;
using GestaoPortifolioInvestimento.Domain;

namespace GestaoPortifolioInvestimento.Application.Mapping
{
    public class ProdutoEstoqueMapping : Profile
    {
        public ProdutoEstoqueMapping()
        {
            CreateMap<ProdutoEstoqueDto, ProdutoEstoque>()
                .ForMember(dest => dest.Est_Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Est_Qtd, opt => opt.MapFrom(src => src.Qtd))
                .ForMember(dest => dest.Est_Qtd_Atual, opt => opt.MapFrom(src => src.QtdAtual))
                .ForMember(dest => dest.Est_DtVenc, opt => opt.MapFrom(src => src.DtVenc))
                .ForMember(dest => dest.Pro_Id, opt => opt.MapFrom(src => src.Produto))
                .ForMember(dest => dest.Est_Preco, opt => opt.MapFrom(src => src.Preco))
                .ForMember(dest => dest.Pro_Nm, opt => opt.MapFrom(src => src.ProdutoNome))
                .ReverseMap(); ;
        }
    }
}
