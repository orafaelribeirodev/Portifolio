using Dapper;
using GestaoPortifolioInvestimento.Repositories.Helps;
using Npgsql;

namespace GestaoPortifolioInvestimento.Repositories
{
    public interface IProdutoRepositories
    {
        Task<bool> Gravar(Domain.Produto obj);
        Task<bool> Excluir(int pro_id);
        Task<List<Domain.Produto>?> Listar(string filtro);
        Task<Domain.Produto?> Registro(int pro_id);
    }

    public class ProdutoRepositories : IProdutoRepositories
    {
        private NpgsqlConnection _conn;

        public ProdutoRepositories()
        {
            _conn = new NpgsqlConnection(Settings.StrConnection);
        }

        public async Task<bool> Gravar(Domain.Produto obj)
        {
            return obj.Pro_Id == 0 ? await Inserir(obj) : await Editar(obj);
        }

        public async Task<bool> Inserir(Domain.Produto obj)
        {
            try
            {
                var sql = "insert into tb_produto (pro_nm, pro_status, pro_dtcad, pro_dtmod)values(@pro_nm, @pro_status, @pro_dtcad, @pro_dtmod)";
                return await _conn.ExecuteAsync(sql, obj) > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> Editar(Domain.Produto obj)
        {
            try
            {
                var sql = "update tb_produto set pro_nm = @pro_nm, pro_status = @pro_status, pro_dtmod = @pro_dtmod where pro_id = @pro_id";
                return await _conn.ExecuteAsync(sql, obj) > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> Excluir(int pro_id)
        {
            try
            {
                var sql = "delete from tb_produto where pro_id = @pro_id";
                return await _conn.ExecuteAsync(sql, new { pro_id }) > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<List<Domain.Produto>?> Listar(string filtro)
        {
            try
            {
                filtro = !string.IsNullOrEmpty(filtro) ? $"where upper(pro_nm) like '%{filtro?.ToUpper()}%'" : "";
                var sql = $"select * from tb_produto {filtro} order by pro_nm asc";
                var rsp = await _conn.QueryAsync<Domain.Produto>(sql);
                return rsp?.ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Domain.Produto?> Registro(int pro_id)
        {
            try
            {
                var sql = "select * from tb_produto where pro_id = @pro_id";
                var rsp = await _conn.QueryAsync<Domain.Produto>(sql, new { pro_id });
                return rsp?.FirstOrDefault();
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
