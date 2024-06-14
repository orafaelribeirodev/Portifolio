namespace GestaoPortifolioInvestimento.Application.Dto
{
    public class ProdutoEstoqueDto
    {
        public int Id { get; set; } 
        public int Qtd { get; set; } 
        public int QtdAtual { get; set; } 
        public string? DtVenc { get; set; } 
        public int Produto { get; set; }
        public decimal Preco { get; set; }
        public string? ProdutoNome { get; set; }
    }
}
