namespace GestaoPortifolioInvestimento.Domain
{
    public class EstoqueSaidaProdutos
    {
        public int Ite_Id { get; set; } 
        public int Est_Id { get; set; } 
        public int Sai_Id { get; set; } 
        public int Est_Qtd { get; set; } 
        public decimal Est_Preco { get; set; }
        public int Pro_Id { get; set; }
        public string? Pro_Nm { get; set; }
        public decimal Est_Total { get; set; }
    }
}
