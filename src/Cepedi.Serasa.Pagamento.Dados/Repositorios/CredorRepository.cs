using Cepedi.Serasa.Pagamento.Dominio.Entidades;
using Cepedi.Serasa.Pagamento.Dominio.Repositorio;
using Microsoft.EntityFrameworkCore;

namespace Cepedi.Serasa.Pagamento.Dados.Repositories;

public class CredorRepository : ICredorRepository
{
    private readonly ApplicationDbContext _context;
    public CredorRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<CredorEntity> CriarCredorAsync(CredorEntity Credor)
    {
        _context.Credor.Add(Credor);
        await _context.SaveChangesAsync();
        return Credor;
    }
    public async Task<CredorEntity> AtualizarCredorAsync(CredorEntity Credor)
    {
        _context.Credor.Update(Credor);

        await _context.SaveChangesAsync();

        return Credor;
    }

    public async Task<List<CredorEntity>> ObterCredorsAsync()
    {
        return await _context.Credor.ToListAsync();
    }

    public async Task<CredorEntity> ObterCredorAsync(int id)
    {
        return await _context.Credor.Where(e => e.Id == id).FirstOrDefaultAsync();
    }
}

