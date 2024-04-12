using Microsoft.EntityFrameworkCore;
using RareNFTs.Infraestructure.Data;
using RareNFTs.Infraestructure.Models;
using RareNFTs.Infraestructure.Repository.Interfaces;


namespace RareNFTs.Infraestructure.Repository.Implementation;

public class RepositoryClient : IRepositoryClient
{

    private readonly RareNFTsContext _context;

    public RepositoryClient(RareNFTsContext context)
    {
        _context = context;
    }

    public async Task<Guid> AddAsync(Client entity)
    {
        await _context.Set<Client>().AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity.Id;
    }

    public async Task DeleteAsync(Guid id)
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

    public async Task<Client> FindByIdAsync(Guid id)
    {
        var @object = await _context.Set<Client>().FindAsync(id);

        return @object!;
    }

    public async Task<IEnumerable<ClientNft>> FindByNftNameAsync(string name)
    {
        var result = await (from clientNft in _context.ClientNft
                            join client in _context.Client on clientNft.IdClient equals client.Id
                            join nft in _context.Nft on clientNft.IdNft equals nft.Id
                            where nft.Description.ToLower().Contains(name.ToLower())
                            select new ClientNft
                            {
                                IdClient = clientNft.IdClient,
                                IdNft = clientNft.IdNft
                            })
                         .Distinct()
                         .ToListAsync();

        return result;
    }

    public async Task<ICollection<Client>> ListAsync()
    {
        var collection = await _context.Set<Client>().AsNoTracking().ToListAsync();
        return collection;
    }

    public async Task<ICollection<Client>> ListOwnersAsync()
    {
        try
        {
            // Query to get clients who are owners of one or more NFTs
            var owners = await _context.Set<Client>()
                .Where(c => _context.Set<ClientNft>().Any(cn => cn.IdClient == c.Id))
                .ToListAsync();

            return owners;
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while retrieving the list of NFT owners.", ex);
        }        
    }

    public async Task UpdateAsync()
    {
        await _context.SaveChangesAsync();
    }
}
