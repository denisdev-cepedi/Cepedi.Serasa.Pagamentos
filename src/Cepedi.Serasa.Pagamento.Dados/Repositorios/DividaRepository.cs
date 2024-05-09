using Cepedi.Serasa.Pagamento.Dominio;
using Cepedi.Serasa.Pagamento.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Cepedi.Serasa.Pagamento.Dados;

public class DividaRepository : IDividaRepository
{
    private readonly ApplicationDbContext _context;

    public DividaRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<DividaEntity> AtualizarDividaAsync(DividaEntity divida)
    {
        _context.Divida.Update(divida);
        await _context.SaveChangesAsync();

        return divida;
    }

    public async Task<DividaEntity> CriarDividaAsync(DividaEntity divida)
    {
        _context.Divida.Add(divida);
        await _context.SaveChangesAsync();
        return divida;
    }

    public async Task<DividaEntity> DeletarDividaAsync(int id)
    {
        var divida = await ObterDividaAsync(id);

        if (divida == null)
        {
            return null!;
        }

        _context.Divida.Remove(divida);
        await _context.SaveChangesAsync();
        return divida;
    }
    public async Task<DividaEntity> ObterDividaAsync(int id)
    {
        return await _context.Divida.Where(e => e.Id == id).FirstOrDefaultAsync();
    }

    public Task<PessoaEntity> ObterPessoaAsync(int idPessoa)
    {
        throw new NotImplementedException();
    }

    public async Task<List<DividaEntity>> GetDividasAsync(){
        return await _context.Divida.ToListAsync();
    }
}
