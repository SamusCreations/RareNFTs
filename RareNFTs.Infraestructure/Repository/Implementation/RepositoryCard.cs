using Microsoft.EntityFrameworkCore;
using RareNFTs.Infraestructure.Data;
using RareNFTs.Infraestructure.Models;
using RareNFTs.Infraestructure.Repository.Interfaces;

namespace RareNFTs.Infraestructure.Repository.Implementation;

public class RepositoryCard : IRepositoryCard
{
    private readonly RareNFTsContext _context;

    public RepositoryCard(RareNFTsContext context)
    {
        _context = context;
    }

    public async Task<Guid> AddAsync(Card entity)
    {
        await _context.Set<Card>().AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity.Id;
    }

    public async Task DeleteAsync(Guid id)
    {
        // Raw Query
        //https://www.learnentityframeworkcore.com/raw-sql/execute-sql
        // int rowAffected = _context.Database.ExecuteSql($"Delete Card Where IdCard = {id}");
        // await Task.FromResult(1);

        var @object = await FindByIdAsync(id);
        _context.Remove(@object);
        _context.SaveChanges();
    }

    public async Task<ICollection<Card>> FindByDescriptionAsync(string description)
    {
        var collection = await _context
                                     .Set<Card>()
                                     .Where(p => p.Description.Contains(description))
                                     .ToListAsync();
        return collection;
    }

    public async Task<Card> FindByIdAsync(Guid id)
    {
        var @object = await _context.Set<Card>().FindAsync(id);

        return @object!;
    }

    public async Task<ICollection<Card>> ListAsync()
    {
        var collection = await _context.Set<Card>().AsNoTracking().ToListAsync();
        return collection;
    }

    public async Task UpdateAsync()
    {
        await _context.SaveChangesAsync();
    }

}
