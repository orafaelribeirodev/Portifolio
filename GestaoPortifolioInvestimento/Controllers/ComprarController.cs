using GestaoPortifolioInvestimento.Application.Dto;
using GestaoPortifolioInvestimento.Domain;
using Microsoft.AspNetCore.Mvc;

namespace GestaoPortifolioInvestimento.Controllers
{
    [Route("api/comprar")]
    [ApiController]
    public class ComprarController : ControllerBase
    {
        private readonly Application.IProdutoEstoqueService _produtoService;

        public ComprarController(Application.IProdutoEstoqueService produtoService)
        {
            _produtoService = produtoService;
        }

        [HttpPost]
        public async Task<IActionResult> Post(ProdutoEstoqueDto obj)
        {
            return Ok(new {data = await _produtoService.Gravar(obj) });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(new { data = await _produtoService.Excluir(id) });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Registro(int id)
        {
            return Ok(new { data = await _produtoService.Registro(id) });
        }

        [HttpGet("listar")]
        public async Task<IActionResult> Listar()
        {
            return Ok(new { data = await _produtoService.Pesquisar("", false) });
        }

        [HttpGet("filtrar/{filtro}")]
        public async Task<IActionResult> ComEstoque(string filtro)
        {
            return Ok(new { data = await _produtoService.Pesquisar(filtro, true) });
        }
    }
}
