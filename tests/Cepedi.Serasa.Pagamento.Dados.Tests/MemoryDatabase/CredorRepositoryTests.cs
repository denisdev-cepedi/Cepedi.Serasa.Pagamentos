// using Cepedi.Serasa.Pagamento.Dados;
// using Cepedi.Serasa.Pagamento.Dados.Repositories;
// using Cepedi.Serasa.Pagamento.Dominio.Entidades;
// using Microsoft.EntityFrameworkCore;

// namespace Cepedi.Serasa.Pagamento.Data.Tests;

// public class CredorRepositoryTest
// {
//     [Fact]
//     public async Task Can_Get_Credors_From_Database()
//     {
//         var options = new DbContextOptionsBuilder<ApplicationDbContext>()
//             .UseInMemoryDatabase(databaseName: "TestDatabase")
//             .Options;
//         using (var context = new ApplicationDbContext(options))
//         {
//             context.Credor.Add(new CredorEntity { Id = 1, Nome = "Credor01" });
//             context.Credor.Add(new CredorEntity { Id = 2, Nome = "Credor02" });
//             context.SaveChanges();
//         }

//         using (var context = new ApplicationDbContext(options))
//         {
//             var CredorRepository = new CredorRepository(context);
//             var Credores = await CredorRepository.GetCredorsAsync();

//             Assert.Equal(2, Credores.Count);
//             Assert.Equal("Credor01", Credores[0].Nome);
//             Assert.Equal("Credor02", Credores[1].Nome);
//         }
//     }
// }
