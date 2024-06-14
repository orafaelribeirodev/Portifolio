using Dapper;
using Npgsql;
using GestaoPortifolioInvestimento.Repositories.Helps;

namespace GestaoPortifolioInvestimento.Repositories
{
    public interface IProdutoEstoqueRepositories
    {
        Task<bool> Gravar(Domain.ProdutoEstoque obj);
        Task<bool> Excluir(int est_id);
        Task<List<Domain.ProdutoEstoque>?> Listar(int pro_id);
        Task<Domain.ProdutoEstoque?> Registro(int est_id);
        Task<List<Domain.ProdutoEstoque>?> Pesquisar(string filtro, bool emEstoque);
        Task<bool> AlterarQuantidade(int est_id, int est_qtd_atual);
        Task<List<Domain.ProdutoEstoque>?> EstoquesVencidos();
    }

    public class ProdutoEstoqueRepositories : IProdutoEstoqueRepositories
    {
        private NpgsqlConnection _conn;

        public ProdutoEstoqueRepositories()
        {
            _conn = new NpgsqlConnection(Settings.StrConnection);
        }

        public async Task<bool> Gravar(Domain.ProdutoEstoque obj)
        {
            return obj.Est_Id == 0 ? await Inserir(obj) : await Editar(obj);
        }

        public async Task<bool> Inserir(Domain.ProdutoEstoque obj)
        {
            try
            {
                var sql = "insert into tb_produto_estoque (est_qtd, est_qtd_atual, est_dtvenc, est_dtcad, est_dtmod, pro_id, est_preco)" +
                    "values(@est_qtd, @est_qtd_atual, @est_dtvenc, @est_dtcad, @est_dtmod, @pro_id, @est_preco)";
                return await _conn.ExecuteAsync(sql, obj) > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> Editar(Domain.ProdutoEstoque obj)
        {
            try
            {
                var sql = "update tb_produto_estoque set est_qtd = @est_qtd, est_qtd_atual = @est_qtd_atual, est_dtvenc = @est_dtvenc, est_dtmod = @est_dtmod, est_preco = @est_preco where est_id = @est_id";
                return await _conn.ExecuteAsync(sql, obj) > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> AlterarQuantidade(int est_id, int est_qtd_atual)
        {
            try
            {
                var sql = "update tb_produto_estoque set est_qtd_atual = @est_qtd_atual where est_id = @est_id";
                return await _conn.ExecuteAsync(sql, new { est_id, est_qtd_atual }) > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> Excluir(int est_id)
        {
            try
            {
                var sql = "delete from tb_produto_estoque where est_id = @est_id";
                return await _conn.ExecuteAsync(sql, new { est_id }) > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<List<Domain.ProdutoEstoque>?> Listar(int pro_id)
        {
            try
            {
                var sql = $"select * from tb_produto_estoque where pro_id = @pro_id";
                var rsp = await _conn.QueryAsync<Domain.ProdutoEstoque>(sql, new { pro_id });
                return rsp?.ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<List<Domain.ProdutoEstoque>?> Pesquisar(string filtro, bool emEstoque)
        {
            try
            {
                filtro = !string.IsNullOrEmpty(filtro) ? $"where upper(tb_produto.pro_nm) like '%{filtro.ToUpper()}%'" : "";
                filtro += emEstoque ? "and tb_produto_estoque.est_qtd_atual > 0" : "";
                var sql = $"select tb_produto_estoque.est_id, tb_produto_estoque.est_qtd_atual, tb_produto_estoque.est_dtvenc, " +
                          $"tb_produto.pro_id, tb_produto.pro_nm, tb_produto_estoque.est_preco from tb_produto_estoque " +
                          $"inner join tb_produto on tb_produto_estoque.pro_id = tb_produto.pro_id {filtro}";
                var rsp = await _conn.QueryAsync<Domain.ProdutoEstoque>(sql);
                return rsp?.ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Domain.ProdutoEstoque?> Registro(int est_id)
        {
            try
            {
                var sql = "select * from tb_produto_estoque where est_id = @est_id";
                var rsp = await _conn.QueryAsync<Domain.ProdutoEstoque>(sql, new { est_id });
                return rsp?.FirstOrDefault();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<List<Domain.ProdutoEstoque>?> EstoquesVencidos()
        {
            try
            {
                var sql = "select tb_produto.pro_nm, tb_produto_estoque.est_id, tb_produto_estoque.est_qtd_atual, tb_produto_estoque.est_dtvenc from tb_produto_estoque " +
                          "inner join tb_produto on tb_produto_estoque.pro_id = tb_produto.pro_id where est_dtvenc <= current_date and tb_produto_estoque.est_qtd_atual > 0";
                var rsp = await _conn.QueryAsync<Domain.ProdutoEstoque>(sql);
                return rsp?.ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
