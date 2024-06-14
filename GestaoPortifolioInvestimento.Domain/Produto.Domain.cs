namespace GestaoPortifolioInvestimento.Domain
{
    public class Produto
    {
        public int Pro_Id { get; set; }
        public string? Pro_Nm { get; set; }
        public bool Pro_Status { get; set; }
        public DateTime? Pro_DtCad { get; set; } = DateTime.Now;
        public DateTime? Pro_DtMod { get; set; } = DateTime.Now;
    }
}
