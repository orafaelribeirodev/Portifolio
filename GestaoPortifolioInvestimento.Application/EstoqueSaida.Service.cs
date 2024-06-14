using AutoMapper;

namespace GestaoPortifolioInvestimento.Application
{
    public interface IEstoqueSaidaService
    {
        Task<string?> Gravar(GestaoPortifolioInvestimento.Application.Dto.EstoqueSaidaDto obj);
        Task<List<GestaoPortifolioInvestimento.Application.Dto.EstoqueSaidaDto>?> Extrato();
        Task<string?> Excluir(int sai_id);
    }

    public class EstoqueSaidaService: IEstoqueSaidaService
    {
        private Repositories.IEstoqueSaidaRepositories _estoqueSaida;
        private Repositories.IEstoqueSaidaProdutoRepositories _estoqueSaidaProduto;

        private Application.IProdutoEstoqueService _estoqueProdutoProduto;
        private Application.IEstoqueSaidaProdutoService _EstoqueSaidaProdutoService;
        private readonly IMapper _mapper;

        public EstoqueSaidaService(
            IMapper mapper, 
            Repositories.IEstoqueSaidaRepositories estoqueSaida, 
            Repositories.IEstoqueSaidaProdutoRepositories estoqueSaidaProduto,
            Application.IProdutoEstoqueService estoqueProdutoProduto,
            Application.IEstoqueSaidaProdutoService EstoqueSaidaProdutoService
            )
        {
            _mapper = mapper;
            _estoqueSaida = estoqueSaida;
            _estoqueSaidaProduto = estoqueSaidaProduto;
            _estoqueProdutoProduto = estoqueProdutoProduto;
            _EstoqueSaidaProdutoService = EstoqueSaidaProdutoService;
        }

        public async Task<string?> Gravar (GestaoPortifolioInvestimento.Application.Dto.EstoqueSaidaDto obj)
        {
            var _map = _mapper.Map<Domain.EstoqueSaida>(obj);
            _map.Sai_DtReg = _map.Sai_DtReg != null ? _map.Sai_DtReg : DateTime.Now;
            var id = await _estoqueSaida.Gravar(_map);
            if(id > 0)
            {
                foreach(var i in obj.Items)
                {
                    var item = new Domain.EstoqueSaidaProdutos();
                    item.Est_Id  = i.Estoque;
                    item.Sai_Id = id;
                    item.Est_Qtd = i.Qtd;
                    item.Est_Preco = i.Preco;
                    if(await _estoqueSaidaProduto.Gravar(item))
                    {
                        await _estoqueProdutoProduto.AlterarQuantidade(i.Estoque, i.Qtd);
                    }
                }
            }
            return id > 0 ? "" : "Não foi possível salvar a saida.";
        }

        public async Task<List<GestaoPortifolioInvestimento.Application.Dto.EstoqueSaidaDto>?> Extrato()
        {
            var rsp = await _estoqueSaida.Listar();
            var lista = _mapper.Map<List<Dto.EstoqueSaidaDto>>(rsp);
            foreach(var i in lista)
            {
                i.Items = await _EstoqueSaidaProdutoService.Listar(i.Id);
            }
            return lista;
        }

        public async Task<string?> Excluir(int sai_id)
        {
            await _EstoqueSaidaProdutoService.ExcluirTudo(sai_id);
            return await _estoqueSaida.Excluir(sai_id) ? "" : "Informação excluída com sucesso.";
        }
    }
}
