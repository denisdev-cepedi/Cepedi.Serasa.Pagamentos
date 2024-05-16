using Cepedi.Serasa.Pagamento.Dados;
using Cepedi.Serasa.Pagamento.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Cepedi.Serasa.Pagamento.Data.Tests;

public class DividaRepositoryTests
{

    public async Task Can_Get_Dividas_From_Database()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        using (var context = new ApplicationDbContext(options))
        {
            context.Divida.Add(new DividaEntity
            {
                Id = 1,
                Valor = 100.00,
                DataDeVencimento = DateTime.Now,
                IdCredor = 1,
                IdPessoa = 1,
                Credor = new CredorEntity { Id = 11, Nome = "Joao" },
                Pessoa = new PessoaEntity { Id = 11, Nome = "Maria", Cpf = "3423423423" }
            });
            context.Divida.Add(new DividaEntity
            {
                Id = 2,
                Valor = 150.00,
                DataDeVencimento = DateTime.Now.AddDays(7),
                IdCredor = 2,
                IdPessoa = 2,
                Credor = new CredorEntity { Id = 22, Nome = "Estivaldo" },
                Pessoa = new PessoaEntity { Id = 22, Nome = "Pedro", Cpf = "1231232132" }
            });
            context.SaveChanges();
        }

        // Act
        using (var context = new ApplicationDbContext(options))
        {
            var dividaRepository = new DividaRepository(context);
            var dividas = await dividaRepository.ObterDividasAsync();

            // Assert
            Assert.NotNull(dividas);
            Assert.Equal(2, dividas.Count);
            Assert.Equal(1, dividas[0].Id);
            Assert.Equal(2, dividas[1].Id);
            Assert.Equal("Joao", dividas[0].Credor.Nome);
            Assert.Equal("Estivaldo", dividas[1].Credor.Nome);
        }
    }
}

