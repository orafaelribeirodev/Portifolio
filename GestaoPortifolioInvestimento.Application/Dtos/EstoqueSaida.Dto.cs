namespace GestaoPortifolioInvestimento.Application.Dto
{
    public class EstoqueSaidaDto
    {
        public int Id { get; set; }
        public string? DataReg { get; set; }
        public List<EstoqueSaidasProdutosDto>? Items { get; set; }
    }
}
