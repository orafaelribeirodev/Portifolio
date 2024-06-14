using AutoMapper;
using GestaoPortifolioInvestimento.Domain;
using GestaoPortifolioInvestimento.Application.Dto;

namespace GestaoPortifolioInvestimento.Application.Mapping
{
    public class EstoqueSaidaProdutoMapping : Profile
    {
        public EstoqueSaidaProdutoMapping()
        {
            CreateMap<EstoqueSaidasProdutosDto, EstoqueSaidaProdutos>()
                .ForMember(dest => dest.Ite_Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Est_Id, opt => opt.MapFrom(src => src.Estoque))
                .ForMember(dest => dest.Sai_Id, opt => opt.MapFrom(src => src.Saida))
                .ForMember(dest => dest.Est_Qtd, opt => opt.MapFrom(src => src.Qtd))
                .ForMember(dest => dest.Est_Preco, opt => opt.MapFrom(src => src.Preco))
                .ForMember(dest => dest.Est_Total, opt => opt.MapFrom(src => src.PrecoTotal))
                .ForMember(dest => dest.Pro_Id, opt => opt.MapFrom(src => src.ProdutoCod))
                .ForMember(dest => dest.Pro_Nm, opt => opt.MapFrom(src => src.ProdutoNome))

                .ReverseMap();;
        }
    }
}
