using AutoMapper;
using GestaoPortifolioInvestimento.Application.Dto;

namespace GestaoPortifolioInvestimento.Application
{
    public interface IProdutoEstoqueService
    {
        Task<string?> Gravar(ProdutoEstoqueDto obj);
        Task<string?> Excluir(int est_id);
        Task<List<ProdutoEstoqueDto>?> Listar(int pro_id);
        Task<ProdutoEstoqueDto?> Registro(int est_id);
        Task<List<ProdutoEstoqueDto>?> Pesquisar(string filtro, bool emEstoque);
        Task<bool> AlterarQuantidade(int est_id, int qtd_saida);
    }
    public class ProdutoEstoqueService : IProdutoEstoqueService
    {
        private Repositories.IProdutoEstoqueRepositories _produtoestoque;
        private readonly IMapper _mapper;

        public ProdutoEstoqueService(IMapper mapper, Repositories.IProdutoEstoqueRepositories produtoestoque)
        {
            _mapper = mapper;
            _produtoestoque = produtoestoque;
        }

        public async Task<string?> Gravar(ProdutoEstoqueDto obj)
        {
            var validar = Validar(obj);
            if (!string.IsNullOrEmpty(validar)) return validar;
            var _map = _mapper.Map<Domain.ProdutoEstoque>(obj);
            return await _produtoestoque.Gravar(_map) ? "" : "Não foi possível inserir a informação.";
        }

        public async Task<bool> AlterarQuantidade(int est_id, int qtd_saida)
        {
            var estoqueAtual = await _produtoestoque.Registro(est_id);
            if (estoqueAtual != null)
            {
                var novoEstoque = (estoqueAtual.Est_Qtd_Atual - qtd_saida);
                return await _produtoestoque.AlterarQuantidade(est_id, novoEstoque);
            }
            return false;
        }

        public async Task<string?> Excluir(int est_id)
        {
            return await _produtoestoque.Excluir(est_id) ? "" : "Não foi possível excluir a informação.";
        }

        public async Task<GestaoPortifolioInvestimento.Application.Dto.ProdutoEstoqueDto?> Registro(int est_id)
        {
            var rsp = await _produtoestoque.Registro(est_id);
            return _mapper.Map<ProdutoEstoqueDto>(rsp);
        }

        public async Task<List<GestaoPortifolioInvestimento.Application.Dto.ProdutoEstoqueDto>?> Listar(int pro_id)
        {
            var rsp = await _produtoestoque.Listar(pro_id);
            return _mapper.Map<List<ProdutoEstoqueDto>>(rsp);
        }

        public async Task<List<GestaoPortifolioInvestimento.Application.Dto.ProdutoEstoqueDto>?> Pesquisar(string filtro, bool emEstoque)
        {
            var rsp = await _produtoestoque.Pesquisar(filtro, emEstoque);
            return _mapper.Map<List<ProdutoEstoqueDto>>(rsp);
        }
        private string Validar(GestaoPortifolioInvestimento.Application.Dto.ProdutoEstoqueDto obj)
        {
            var msg = "";
            if (obj.Produto == 0)
            {
                msg += "- Produto é obrigatório.";
            }
            return msg;
        }
    }
}
