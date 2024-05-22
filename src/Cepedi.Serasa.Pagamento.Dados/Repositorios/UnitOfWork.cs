﻿using Cepedi.Serasa.Pagamento.Dados.Repositorios;
using Cepedi.Serasa.Pagamento.Dominio;
using Cepedi.Serasa.Pagamento.Dominio.Repositorio;

namespace Cepedi.Serasa.Pagamento.Dados;
public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    private Dictionary<Type, object> _repositories;

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
        _repositories = new Dictionary<Type, object>();
    }

    public IRepository<T> Repository<T>()
        where T : class
    {
        if (_repositories.ContainsKey(typeof(T)))
        {
            return (IRepository<T>)_repositories[typeof(T)];
        }

        var repository = new Repository<T>(_context);
        _repositories[typeof(T)] = repository;

        return repository;
    }

    public async Task<bool> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var retorno = await _context.SaveChangesAsync(cancellationToken);

        return retorno > 0;
    }

    public void Dispose()
    {
        throw new NotImplementedException();
    }
}