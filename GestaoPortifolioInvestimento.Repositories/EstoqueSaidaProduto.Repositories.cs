using Dapper;
using GestaoPortifolioInvestimento.Repositories.Helps;
using Npgsql;

namespace GestaoPortifolioInvestimento.Repositories
{
    public interface IEstoqueSaidaProdutoRepositories
    {
        Task<bool> Gravar(Domain.EstoqueSaidaProdutos obj);
        Task<bool> Excluir(int ite_id);
        Task<bool> ExcluirTudo(int sai_id);
        Task<List<Domain.EstoqueSaidaProdutos>?> Listar(int sai_id);
    }

    public class EstoqueSaidaProdutoRepositories : IEstoqueSaidaProdutoRepositories
    {
        private NpgsqlConnection _conn;

        public EstoqueSaidaProdutoRepositories()
        {
            _conn = new NpgsqlConnection(Settings.StrConnection);
        }

        public  async Task<bool> Gravar(Domain.EstoqueSaidaProdutos obj)
        {
            return obj.Ite_Id == 0 ? await Inserir(obj) : await Editar(obj);
        }

        public  async Task<bool> Inserir(Domain.EstoqueSaidaProdutos obj)
        {
            try
            {
                var sql = "insert into tb_estoque_saida_produto (est_id, sai_id, est_qtd, est_preco)values(@est_id, @sai_id, @est_qtd, @est_preco)";
                return await _conn.ExecuteAsync(sql, obj) > 0;
            }
            catch(Exception)
            {
                return false;
            }
        }

        public  async Task<bool> Editar(Domain.EstoqueSaidaProdutos obj)
        {
            try
            {
                var sql = "update tb_estoque_saida_produto set est_id = @est_id, sai_id = @sai_id, est_qtd = @est_qtd, est_preco = @est_preco where ite_id = @ite_id";
                return await _conn.ExecuteAsync(sql, obj) > 0;
            }
            catch(Exception)
            {
                return false;
            }
        }

        public  async Task<bool> Excluir(int ite_id)
        {
            try
            {
                var sql = "delete from tb_estoque_saida_produto where ite_id = @ite_id";
                return await _conn.ExecuteAsync(sql, new {ite_id}) > 0;
            }
            catch(Exception)
            {
                return false;
            }
        }

        public async Task<bool> ExcluirTudo(int sai_id)
        {
            try
            {
                var sql = "delete from tb_estoque_saida_produto where sai_id = @sai_id";
                return await _conn.ExecuteAsync(sql, new { sai_id }) > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public  async Task<List<Domain.EstoqueSaidaProdutos>?> Listar(int sai_id)
        {
            try
            {
                var sql = $"select tb_produto.pro_id, tb_produto.pro_nm, tb_estoque_saida_produto.ite_id, tb_estoque_saida_produto.est_id, tb_estoque_saida_produto.sai_id, tb_estoque_saida_produto.est_qtd, " +
                    $"tb_estoque_saida_produto.est_preco, ( tb_estoque_saida_produto.est_preco * tb_estoque_saida_produto.est_qtd) as est_total from tb_estoque_saida_produto inner join tb_produto_estoque " +
                    $"on tb_estoque_saida_produto.est_id = tb_produto_estoque.est_id inner join tb_produto on tb_produto_estoque.pro_id = tb_produto.pro_id where tb_estoque_saida_produto.sai_id = @sai_id";
                var rsp = await _conn.QueryAsync<Domain.EstoqueSaidaProdutos>(sql, new { sai_id });
                return rsp?.ToList();
            }
            catch(Exception)
            {
                return null;
            }
        }

        public  async Task<Domain.EstoqueSaidaProdutos?> Registro(int ite_id)
        {
            try
            {
                var sql = "select * from tb_estoque_saida_produto where ite_id = @ite_id";
                var rsp = await _conn.QueryAsync<Domain.EstoqueSaidaProdutos>(sql, new {ite_id});
                return rsp?.FirstOrDefault();
            }
            catch(Exception)
            {
                return null;
            }
        }

        public  async Task<bool> Existe(Domain.EstoqueSaidaProdutos obj)
        {
            try
            {
                var sql = "select * from tb_estoque_saida_produto where ite_id = @ite_id";
                var rsp = await _conn.QueryAsync<Domain.EstoqueSaidaProdutos>(sql, new {obj.Ite_Id});
                return rsp?.Count() > 0;
            }
            catch(Exception)
            {
                return false;
            }
        }
    }
}
