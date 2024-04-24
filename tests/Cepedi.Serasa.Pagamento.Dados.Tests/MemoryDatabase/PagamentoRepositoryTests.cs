using Cepedi.Serasa.Pagamento.Dados.Repositories;
using Cepedi.Serasa.Pagamento.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Cepedi.Serasa.Pagamento.Dados.Tests.MemoryDatabase;

public class PagamentoRepositoryTests
{
    [Fact]
    public async Task Can_Get_Pagamentos_From_Database()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        using (var context = new ApplicationDbContext(options))
        {
            context.Pagamento.Add(new PagamentoEntity { Valor = 100, DataDePagamento = DateTime.Now, DataDeVencimento = DateTime.Now, IdCredor = 1 });
            context.Pagamento.Add(new PagamentoEntity { Valor = 200, DataDePagamento = DateTime.Now, DataDeVencimento = DateTime.Now, IdCredor = 2 });
            context.SaveChanges();
        }

        // Act
        using (var context = new ApplicationDbContext(options))
        {
            var userRepository = new PagamentoRepository(context);
            var pagamentos = await userRepository.GetPagamentosAsync();

            // Assert
            Assert.Equal(2, pagamentos.Count);
            Assert.Equal(100, pagamentos[0].Valor);
            Assert.Equal(200, pagamentos[1].Valor);
        }
    }

}
