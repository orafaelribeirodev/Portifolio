namespace GestaoPortifolioInvestimento.Domain
{
    public class ProdutoEstoque
    {
        public int Est_Id { get; set; } 
        public int Est_Qtd { get; set; } 
        public int Est_Qtd_Atual { get; set; } 
        public DateTime? Est_DtVenc { get; set; } 
        public DateTime? Est_DtCad { get; set; }  = DateTime.Now;
        public DateTime? Est_DtMod { get; set; }  = DateTime.Now;
        public int Pro_Id { get; set; } 
        public decimal Est_Preco { get; set; }
        public string? Pro_Nm { get; set; }
    }
}
