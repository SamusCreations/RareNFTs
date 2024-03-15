using Microsoft.EntityFrameworkCore;
using RareNFTs.Infraestructure.Data;
using RareNFTs.Infraestructure.Models;
using RareNFTs.Infraestructure.Repository.Interfaces;

namespace RareNFTs.Infraestructure.Repository.Implementation;

public class RepositoryCountry : IRepositoryCountry
{
    private readonly RareNFTsContext _context;

    public RepositoryCountry(RareNFTsContext context)
    {
        _context = context;
    }

    public async Task<string> AddAsync(Country entity)
    {
        await _context.Set<Country>().AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity.Id;
    }

    public async Task DeleteAsync(string id)
    {
        // Raw Query
        //https://www.learnentityframeworkcore.com/raw-sql/execute-sql
        // int rowAffected = _context.Database.ExecuteSql($"Delete Country Where IdCountry = {id}");
        // await Task.FromResult(1);

        var @object = await FindByIdAsync(id);
        _context.Remove(@object);
        _context.SaveChanges();
    }

    public async Task<ICollection<Country>> FindByDescriptionAsync(string description)
    {
        var collection = await _context
                                     .Set<Country>()
                                     .Where(p => p.Name.Contains(description))
                                     .ToListAsync();
        return collection;
    }

    public async Task<Country> FindByIdAsync(string id)
    {
        var @object = await _context.Set<Country>().FindAsync(id);

        return @object!;
    }

    public async Task<ICollection<Country>> ListAsync()
    {
        var collection = await _context.Set<Country>().AsNoTracking().ToListAsync();
        return collection;
    }

    public async Task UpdateAsync()
    {
        await _context.SaveChangesAsync();
    }

}
