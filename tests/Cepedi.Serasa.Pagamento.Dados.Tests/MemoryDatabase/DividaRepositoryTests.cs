using System.Data.Common;
using Cepedi.Serasa.Pagamento.Dados;
using Cepedi.Serasa.Pagamento.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Cepedi.Serasa.Pagamento.Data.Tests.MemoryDatabase;

public class DividaRepositoryTests
{
    [Fact]
    public async Task Can_Get_Dividas_From_Database(){

        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
        .UseInMemoryDatabase(databaseName: "TestDatabase").Options;


        // Act
        using (var context = new ApplicationDbContext(options)){
            context.Divida.Add(new DividaEntity {Id = 1, IdCredor = 1, IdPessoa = 1, Valor = 190});
            context.Divida.Add(new DividaEntity {Id = 2, IdCredor = 2, IdPessoa = 2, Valor = 200});
            context.SaveChanges();
        }

        using (var context = new ApplicationDbContext(options)){
            var dividaRepository = new DividaRepository(context);
            var dividas = await dividaRepository.GetDividasAsync();

        // Assert
        Assert.Equal(2, dividas.Count);
        Assert.Equal(1, dividas[0].Id);
        Assert.Equal(2, dividas[1].Id);
        Assert.Equal(190, dividas[0].Valor);
        Assert.Equal(200, dividas[1].Valor);
        Assert.Equal(1, dividas[0].IdCredor);
        Assert.Equal(2, dividas[1].IdPessoa);
        }

    }
}
