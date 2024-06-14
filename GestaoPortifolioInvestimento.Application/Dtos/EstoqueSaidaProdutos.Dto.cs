namespace GestaoPortifolioInvestimento.Application.Dto
{
    public class EstoqueSaidasProdutosDto
    {
        public int Id { get; set; } 
        public int Estoque { get; set; } 
        public int Saida { get; set; } 
        public int Qtd { get; set; } 
        public decimal Preco { get; set; }
        public decimal PrecoTotal { get; set; }
        public int ProdutoCod { get; set; }
        public string? ProdutoNome { get; set; }

    }
}
