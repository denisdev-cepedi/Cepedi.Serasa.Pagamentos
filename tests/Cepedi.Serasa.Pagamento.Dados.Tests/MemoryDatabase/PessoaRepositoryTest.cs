using Cepedi.Serasa.Pagamento.Dados;
using Cepedi.Serasa.Pagamento.Dados.Repositories;
using Cepedi.Serasa.Pagamento.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Cepedi.Serasa.Pagamento.Data.Tests;

public class PessoaRepositoryTest
{
    [Fact]
    public async Task Can_Get_Pessoas_From_Database()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;
        using (var context = new ApplicationDbContext(options))
        {
            context.Pessoa.Add(new PessoaEntity { Id = 1, Nome = "pessoa01", Cpf = "1235567789" });
            context.Pessoa.Add(new PessoaEntity { Id = 2, Nome = "pessoa02", Cpf = "1235567789" });
            context.SaveChanges();
        }

        using (var context = new ApplicationDbContext(options))
        {
            var pessoaRepository = new PessoaRepository(context);
            var pessoas = await pessoaRepository.GetPessoasAsync();

            Assert.Equal(2, pessoas.Count);
            Assert.Equal("pessoa01", pessoas[0].Nome);
            Assert.Equal("pessoa02", pessoas[1].Nome);
        }
    }
}
