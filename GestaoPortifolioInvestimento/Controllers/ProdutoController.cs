using GestaoPortifolioInvestimento.Domain;
using Microsoft.AspNetCore.Mvc;

namespace GestaoPortifolioInvestimento.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly Application.IProdutoService _produtoService;
        public ProdutoController(Application.IProdutoService produtoService)
        {
            _produtoService = produtoService;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Application.Dto.ProdutoDto obj)
        {
            var rsp = await _produtoService.Gravar(obj);
            return Ok(new { data = rsp });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var rsp = await _produtoService.Excluir(id);
            return Ok(new { data = rsp });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Registro(int id)
        {
            var rsp = await _produtoService.Registro(id);
            return Ok(new { data = rsp });
        }

        [HttpGet()]
        public async Task<IActionResult> Listar()
        {
            var rsp = await _produtoService.Listar("");
            return Ok(new { data = rsp });
        }

        [HttpGet("pesquisar/{filtro}")]
        public async Task<IActionResult> Listar(string filtro)
        {
            var rsp = await _produtoService.Listar(filtro);
            return Ok(new { data = rsp });
        }
    }
}
