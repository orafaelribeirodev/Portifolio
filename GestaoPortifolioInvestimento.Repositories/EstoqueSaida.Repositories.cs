using Dapper;
using GestaoPortifolioInvestimento.Repositories.Helps;
using Npgsql;

namespace GestaoPortifolioInvestimento.Repositories
{
    public interface IEstoqueSaidaRepositories
    {
        Task<int> Gravar(Domain.EstoqueSaida obj);
        Task<bool> Excluir(int sai_id);
        Task<List<Domain.EstoqueSaida>?> Listar();
    }

    public class EstoqueSaidaRepositories : IEstoqueSaidaRepositories
    {
        private NpgsqlConnection _conn;

        public EstoqueSaidaRepositories()
        {
            _conn = new NpgsqlConnection(Settings.StrConnection);
        }

        public async Task<int> Gravar(Domain.EstoqueSaida obj)
        {
            return obj.Sai_Id == 0 ? await Inserir(obj) : obj.Sai_Id;
        }

        public async Task<int> Inserir(Domain.EstoqueSaida obj)
        {
            try
            {
                var sql = "insert into tb_estoque_saida (sai_dtreg)values(@sai_dtreg) returning sai_id";
                return await _conn.ExecuteScalarAsync<int>(sql, obj);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public async Task<bool> Excluir(int sai_id)
        {
            try
            {
                var sql = "delete from tb_estoque_saida where sai_id = @sai_id";
                return await _conn.ExecuteAsync(sql, new { sai_id }) > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<List<Domain.EstoqueSaida>?> Listar()
        {
            try
            {
                var sql = $"select * from tb_estoque_saida";
                var rsp = await _conn.QueryAsync<Domain.EstoqueSaida>(sql);
                return rsp?.ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Domain.EstoqueSaida?> Registro(int sai_id)
        {
            try
            {
                var sql = "select * from tb_estoque_saida where sai_id = @sai_id";
                var rsp = await _conn.QueryAsync<Domain.EstoqueSaida>(sql, new { sai_id });
                return rsp?.FirstOrDefault();
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
