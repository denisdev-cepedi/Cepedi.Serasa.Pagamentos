using Cepedi.Serasa.Pagamento.Dominio.Entidades;
using Cepedi.Serasa.Pagamento.Dominio.Repositorio;
using Microsoft.EntityFrameworkCore;

namespace Cepedi.Serasa.Pagamento.Dados.Repositories;

public class PessoaRepository : IPessoaRepository
{
    private readonly ApplicationDbContext _context;
    public PessoaRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<PessoaEntity> AtualizarPessoaAsync(PessoaEntity pessoa)
    {
        _context.Pessoa.Update(pessoa);



        return pessoa;
    }

    public async Task<PessoaEntity> CriarPessoaAsync(PessoaEntity pessoa)
    {
        _context.Pessoa.Add(pessoa);

        return pessoa;
    }

    public async Task<PessoaEntity?> DeletarPessoaAsync(int id)
    {
        var pessoaEntity = await ObterPessoaAsync(id);
        if (pessoaEntity == null) return null;

        _context.Pessoa.Remove(pessoaEntity);

        return pessoaEntity;
    }

    public async Task<List<PessoaEntity>> GetPessoasAsync()
    {
        return await _context.Pessoa.ToListAsync();
    }

    public async Task<PessoaEntity> ObterPessoaAsync(int id)
    {
        return await
                _context.Pessoa.Where(e => e.Id == id).FirstOrDefaultAsync();
    }
}
