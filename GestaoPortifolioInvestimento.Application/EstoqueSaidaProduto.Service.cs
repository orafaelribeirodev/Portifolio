using AutoMapper;

namespace GestaoPortifolioInvestimento.Application
{
    public interface IEstoqueSaidaProdutoService
    {
        Task<string?> Gravar(Dto.EstoqueSaidaDto obj);
        Task<List<Dto.EstoqueSaidasProdutosDto>?> Listar(int sai_id);
        Task<string?> Excluir(int ite_id);
        Task<string?> ExcluirTudo(int sai_id);
    }

    public class EstoqueSaidaProdutoService : IEstoqueSaidaProdutoService
    {
        private GestaoPortifolioInvestimento.Repositories.IEstoqueSaidaProdutoRepositories _estoqueSaidaProduto;
        private readonly IMapper _mapper;

        public EstoqueSaidaProdutoService(IMapper mapper, GestaoPortifolioInvestimento.Repositories.IEstoqueSaidaProdutoRepositories estoqueSaidaProduto)
        {
            _mapper = mapper;
            _estoqueSaidaProduto = estoqueSaidaProduto;
        }

        public async Task<string?> Gravar(Dto.EstoqueSaidaDto obj)
        {
            var _map = _mapper.Map<Domain.EstoqueSaidaProdutos>(obj);
            return await _estoqueSaidaProduto.Gravar(_map) ? "" : "Não foi possível inserir a informação.";
        }

        public async Task<string?> Excluir(int ite_id)
        {
            return await _estoqueSaidaProduto.Excluir(ite_id) ? "" : "Não foi possível excluir a informação.";
        }

        public async Task<string?> ExcluirTudo(int sai_id)
        {
            return await _estoqueSaidaProduto.ExcluirTudo(sai_id) ? "" : "Não foi possível excluir a informação.";
        }

        public async Task<List<Dto.EstoqueSaidasProdutosDto>?> Listar(int sai_id)
        {
            var rsp = await _estoqueSaidaProduto.Listar(sai_id);
            return _mapper.Map<List<Dto.EstoqueSaidasProdutosDto>>(rsp);
        }
    }
}
