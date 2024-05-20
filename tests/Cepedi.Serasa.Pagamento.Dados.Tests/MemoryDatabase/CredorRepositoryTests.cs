using Cepedi.Serasa.Pagamento.Dados;
using Cepedi.Serasa.Pagamento.Dados.Repositories;
using Cepedi.Serasa.Pagamento.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Cepedi.Serasa.Pagamento.Data.Tests;

public class CredorRepositoryTests
{
    [Fact]

    public async Task Can_Get_Credores_From_Database()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        using (var context = new ApplicationDbContext(options))
        {
            context.Credor.Add(new CredorEntity { Id = 1, Nome = "Joao" });
            context.Credor.Add(new CredorEntity { Id = 2, Nome = "Estivaldo" });
            context.SaveChanges();
        }

        // Act
        using (var context = new ApplicationDbContext(options))
        {
            var credorRepository = new CredorRepository(context);
            var credores = await credorRepository.ObterCredoresAsync();

            // Assert
            Assert.NotNull(credores);
            Assert.Equal(2, credores.Count);
            Assert.Equal("Joao", credores[0].Nome);
            Assert.Equal("Estivaldo", credores[1].Nome);
        }
    }

}
