using Microsoft.AspNetCore.Mvc;

namespace GestaoPortifolioInvestimento.Controllers
{
    [Route("api/venda")]
    [ApiController]
    public class VendaController : ControllerBase
    {
        private readonly Application.IEstoqueSaidaService _produtoService;
        public VendaController(Application.IEstoqueSaidaService produtoService)
        {
            _produtoService = produtoService;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Application.Dto.EstoqueSaidaDto obj)
        {
            return Ok(new { data = await _produtoService.Gravar(obj) });
        }

        [HttpGet("extrato")]
        public async Task<IActionResult> Extrato()
        {
            return Ok(new { data = await _produtoService.Extrato() });
        }

        [HttpDelete("excluir/{id}")]
        public async Task<IActionResult> Excluir(int id)
        {
            return Ok(new { data = await _produtoService.Excluir(id) });
        }
    }
}
