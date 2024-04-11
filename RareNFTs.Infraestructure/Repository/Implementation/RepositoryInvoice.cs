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

public class RepositoryInvoice : IRepositoryInvoice
{
    private readonly RareNFTsContext _context;

    public RepositoryInvoice(RareNFTsContext context)
    {
        _context = context;

    }
    public async Task<Guid> AddAsync(InvoiceHeader entity)
    {

        try
        {
            // Get No Receipt
            
            // Reenumerate
            entity.InvoiceDetail.ToList().ForEach(p => p.IdInvoice = entity.Id);
            // Begin Transaction
            await _context.Database.BeginTransactionAsync();
            await _context.Set<InvoiceHeader>().AddAsync(entity);

            // Withdraw from inventory
            foreach (var item in entity.InvoiceDetail)
            {
                // find the product
                var ProductNft = await _context.Set<Nft>().FindAsync(item.IdNft);
                // update stock
                ProductNft!.Quantity = ProductNft.Quantity - item.Quantity;

                // update entity product
                _context.Set<Nft>().Update(ProductNft);
            }

            await _context.SaveChangesAsync();
            // Commit
            await _context.Database.CommitTransactionAsync();

            return entity.Id;
        }
        catch (Exception ex)
        {
            Exception exception = ex;
            // Rollback 
            await _context.Database.RollbackTransactionAsync();
            throw;
        }
    }

   


    public async Task<InvoiceHeader> FindByIdAsync(Guid id)
    {

        var response = await _context.Set<InvoiceHeader>()
                    .Include(detail => detail.InvoiceDetail)
                    .ThenInclude(detail => detail.IdNftNavigation)
                    .Include(client => client.IdClientNavigation)
                    .Where(p => p.Id == id).FirstOrDefaultAsync();

        return response!;
    }
}
