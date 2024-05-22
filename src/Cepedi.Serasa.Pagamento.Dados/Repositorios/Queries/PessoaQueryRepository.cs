using Dapper;
using Cepedi.Serasa.Pagamento.Dominio.Entidades;
using Cepedi.Serasa.Pagamento.Dominio.Repositorio.Queries;
using Microsoft.Extensions.Configuration;

namespace Cepedi.Serasa.Pagamento.Dados.Repositorios.Queries;

public class PessoaQueryRepository : BaseDapperRepository, IPessoaQueryRepository
{
    public PessoaQueryRepository(IConfiguration configuration)
        : base(configuration)
    {
    }

    public async Task<PessoaEntity?> ObterPessoasDapperAsync(int id)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@Id", id, System.Data.DbType.String);

        var sql = @"SELECT 
                        Id, 
                        Nome,
                        Cpf
                    FROM Pessoa WITH(NOLOCK)
                    Where
                        Id = @Id";

        var retorno = await ExecuteQueryAsync
            <PessoaEntity>(sql, parametros);

        return retorno.FirstOrDefault();
    }

    public async Task<PessoaEntity?> AtualizarPessoaDapperAsync(PessoaEntity pessoa)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@Id", pessoa.Id, System.Data.DbType.Int32);
        parametros.Add("@Nome", pessoa.Nome, System.Data.DbType.String);
        parametros.Add("@Cpf", pessoa.Cpf, System.Data.DbType.String);

        var sql = @"UPDATE Pessoa
                    SET
                        Nome = @Nome,
                        Cpf = @Cpf
                    WHERE
                        Id = @Id";

        var retorno = await ExecuteQueryAsync<PessoaEntity>(sql, parametros);

        return retorno.FirstOrDefault();
    }
}
