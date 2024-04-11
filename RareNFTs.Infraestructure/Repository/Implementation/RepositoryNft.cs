using Microsoft.EntityFrameworkCore;
using RareNFTs.Infraestructure.Data;
using RareNFTs.Infraestructure.Models;
using RareNFTs.Infraestructure.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

    public async Task<Nft> FindByIdAsync(Guid id)
    {
        var @object = await _context.Set<Nft>().FindAsync(id);
        return @object!;
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

    public async Task<bool> ChangeNFTOwnerAsync(Guid nftId, Guid clientId)
    {
        try
        {
            // Begin Transaction
            await _context.Database.BeginTransactionAsync();

            // Find the NFT
            var nft = await _context.Set<Nft>().FindAsync(nftId);

            if (nft == null)
            {
                // NFT not found
                throw new Exception("An error occurred while changing the NFT owner. NFT not found.");
            }

            var clientNft = await _context.Set<ClientNft>().FindAsync(clientId);

            if(clientNft != null && clientNft?.IdClient != clientId)
            {
                // If the new owner is the same
                throw new Exception("The new owner can not be the same.");
            }

            // Find the previous owner's ClientNFT entries
            var previousOwnerEntries = await _context.Set<ClientNft>()
                .Where(cn => cn.IdNft == nftId)
                .ToListAsync();

            // Remove previous owner's entries from ClientNFT table
            _context.Set<ClientNft>().RemoveRange(previousOwnerEntries);

            // Create new ClientNFT entry for the new owner
            await _context.Set<ClientNft>().AddAsync(new ClientNft
            {
                IdClient = clientId,
                IdNft = nftId,
                Date = DateTime.Now // Assuming you want to record the date of ownership change
            });

            // Save changes
            await _context.SaveChangesAsync();

            // Commit Transaction
            await _context.Database.CommitTransactionAsync();

            return true;
        }
        catch (Exception ex)
        {
            // Rollback Transaction
            await _context.Database.RollbackTransactionAsync();
            throw new Exception("An error occurred while changing the NFT owner.", ex);
        }
    }

    public async Task<ICollection<Nft>> FindOwnedByDescriptionAsync(string description)
    {
        try
        {
            // Query to get NFTs by description from ClientNFT table
            var nfts = await _context.Set<Nft>()
                .Where(nft => _context.Set<ClientNft>()
                    .Where(cn => cn.IdNft == nft.Id)
                    .Any(cn => nft.Description!.Contains(description)))
                .ToListAsync();

            return nfts!;
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while retrieving NFTs by description from ClientNFT table.", ex);
        }
    }

}
