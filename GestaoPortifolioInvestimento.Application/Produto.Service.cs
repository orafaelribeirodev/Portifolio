using AutoMapper;
using GestaoPortifolioInvestimento.Application.Dto;
using System.Security.Cryptography;

namespace GestaoPortifolioInvestimento.Application
{
    public interface IProdutoService
    {
        Task<string?> Gravar(ProdutoDto obj);
        Task<string?> Excluir(int pro_id);
        Task<List<ProdutoDto>?> Listar(string filtro);
        Task<ProdutoDto?> Registro(int pro_id);
    }

    public class ProdutoService: IProdutoService
    {
        private readonly GestaoPortifolioInvestimento.Repositories.IProdutoRepositories _produto;
        private readonly IMapper _mapper;
        public ProdutoService(IMapper mapper, GestaoPortifolioInvestimento.Repositories.IProdutoRepositories produto)
        {
            _mapper = mapper;
            _produto = produto;
        }

        public async Task<string?> Gravar (ProdutoDto obj)
        {
            var _validar = Validar(obj);
            if(!string.IsNullOrEmpty(_validar)) return  _validar;
            var _map = _mapper.Map<Domain.Produto>(obj);
            return await _produto.Gravar(_map) ? "" : "Não foi possível inserir a informação.";
        }

        public async Task<string?> Excluir (int pro_id)
        {
            return await _produto.Excluir(pro_id) ? "" : "Não foi possível excluir a informação.";
        }

        public async Task<ProdutoDto?> Registro (int pro_id)
        {
            var rsp =  await _produto.Registro(pro_id);
            return _mapper.Map<ProdutoDto>(rsp);
        }

        public async Task<List<ProdutoDto>?> Listar(string filtro)
        {
            var lista = await _produto.Listar(filtro);
            return _mapper.Map<List<ProdutoDto>>(lista);
        }

        public string? Validar (ProdutoDto obj)
        {
            var msg = "";
            if(string.IsNullOrEmpty(obj.Nome))
            {
                msg += "- Campo nome do produto é obrigatório.\n";
            }
            return msg;
        }
    }
}
