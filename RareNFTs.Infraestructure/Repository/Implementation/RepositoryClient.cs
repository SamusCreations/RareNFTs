using Microsoft.EntityFrameworkCore;
using RareNFTs.Infraestructure.Data;
using RareNFTs.Infraestructure.Models;
using RareNFTs.Infraestructure.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RareNFTs.Infraestructure.Repository.Implementation;

internal class RepositoryClient:IRepositoryClient
{

    private readonly RareNFTsContext _context;

    public RepositoryClient(RareNFTsContext context)
    {
        _context = context;
    }

    public async Task<string> AddAsync(Client entity)
    {
        await _context.Set<Client>().AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity.Id;
    }

    public async Task DeleteAsync(string id)
    {
        // Raw Query
        //https://www.learnentityframeworkcore.com/raw-sql/execute-sql
        // int rowAffected = _context.Database.ExecuteSql($"Delete Client Where IdClient = {id}");
        // await Task.FromResult(1);

        var @object = await FindByIdAsync(id);
        _context.Remove(@object);
        _context.SaveChanges();
    }

    public async Task<ICollection<Client>> FindByDescriptionAsync(string description)
    {
        var collection = await _context
                                     .Set<Client>()
                                     .Where(p => p.Name.Contains(description))
                                     .ToListAsync();
        return collection;
    }

    public async Task<Client> FindByIdAsync(string id)
    {
        var @object = await _context.Set<Client>().FindAsync(id);

        return @object!;
    }

    public async Task<ICollection<Client>> ListAsync()
    {
        var collection = await _context.Set<Client>().AsNoTracking().ToListAsync();
        return collection;
    }

    public async Task UpdateAsync()
    {
        await _context.SaveChangesAsync();
    }
}
