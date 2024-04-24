using Cepedi.Serasa.Pagamento.Dominio.Entidades;
using Cepedi.Serasa.Pagamento.Dominio.Repositorio;
using Microsoft.EntityFrameworkCore;

namespace Cepedi.Serasa.Pagamento.Dados.Repositories
{
    public class PagamentoRepository : IPagamentoRepository
    {

        private readonly ApplicationDbContext _context;

        public PagamentoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PagamentoEntity> AtualizarPagamentoAsync(PagamentoEntity pagamento)
        {
            _context.Pagamento.Update(pagamento);

            await _context.SaveChangesAsync();

            return pagamento;
        }

        public async Task<PagamentoEntity> CriarPagamentoAsync(PagamentoEntity pagamento)
        {
            _context.Pagamento.Add(pagamento);

            await _context.SaveChangesAsync();

            return pagamento;
        }

        public async Task<List<PagamentoEntity>> GetPagamentosAsync()
        {
            return await _context.Pagamento.ToListAsync();
        }

        public async Task<PagamentoEntity> ObterPagamentoAsync(int id)
        {
            return await
                _context.Pagamento.Where(e => e.Id == id).FirstOrDefaultAsync();
        }

        public async Task<CredorEntity> ObterCredorPagamentoAsync(int id)
        {
            return await
                _context.Credor.Where(e => e.Id == id).FirstOrDefaultAsync();
        }

        public async Task<PagamentoEntity> DeletarPagamentoAsync(int id)
        {
            var pagamentoEntity = await ObterPagamentoAsync(id);

            if (pagamentoEntity == null)
            {
                return null;
            }

            _context.Pagamento.Remove(pagamentoEntity);

            await _context.SaveChangesAsync();

            return pagamentoEntity;
        }
    }
}
