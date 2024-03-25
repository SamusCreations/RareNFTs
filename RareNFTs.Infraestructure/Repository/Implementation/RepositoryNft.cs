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

public class RepositoryNft : IRepositoryNft
{
    private readonly RareNFTsContext _context;

    public RepositoryNft(RareNFTsContext context)
    {
        _context = context;
    }

    public async Task<Guid> AddAsync(Nft entity)
    {
        await _context.Set<Nft>().AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity.Id;
    }

 

    public async Task DeleteAsync(Guid id)
    {
        var @object = await FindByIdAsync(id);
        _context.Remove(@object);
        _context.SaveChanges();
    }

  
    public async Task<ICollection<Nft>> FindByDescriptionAsync(string description)
    {
        var collection = await _context
                                     .Set<Nft>()
                                     .Where(p => p.Description.Contains(description))
                                     .ToListAsync();
        return collection;
    }

    public async Task<Nft> FindByIdAsync(int id)
    {
        var @object = await _context.Set<Nft>().FindAsync(id);
        return @object!;
    }

    public Task<Nft> FindByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<ICollection<Nft>> ListAsync()
    {
        var collection = await _context.Set<Nft>().
                                            Include(b => b.ClientNft).
                                            AsNoTracking().ToListAsync();
        return collection;
    }

    public async Task UpdateAsync()
    {
        await _context.SaveChangesAsync();
    }

}
