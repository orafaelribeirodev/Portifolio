using GestaoPortifolioInvestimento.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace GestaoPortifolioInvestimento.Application
{
    public class EmailNotificacaoService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<EmailNotificacaoService> _logger;

        public EmailNotificacaoService(
            IServiceScopeFactory scopeFactory,
            ILogger<EmailNotificacaoService> logger)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _scopeFactory.CreateScope())
                {
                    var produtoEstoqueRepositories = scope.ServiceProvider.GetRequiredService<IProdutoEstoqueRepositories>();
                    var venc = await produtoEstoqueRepositories.EstoquesVencidos();
                    if (venc?.Count() > 0)
                    {
                        foreach (var e in venc)
                        {
                            Console.WriteLine($" E-mail enviado ao destinatário. Produto: {e.Pro_Nm}, Quant.: {e.Est_Qtd_Atual}, Date de Venc.: {e.Est_DtVenc?.ToString("dd/MM/yyyy")}");
                        }
                    }
                }
                await Task.Delay(TimeSpan.FromDays(1), stoppingToken);
            }
        }
    }
}
