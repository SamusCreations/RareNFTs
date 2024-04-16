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

public class RepositoryUser : IRepositoryUser
{
    private readonly RareNFTsContext _context;

    public RepositoryUser(RareNFTsContext context)
    {
        _context = context;
    }

    public async Task<string> AddAsync(User entity)
    {
        await _context.Set<User>().AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity.Email!;
    }

    public async Task DeleteAsync(Guid id)
    {

        var @object = await FindByIdAsync(id);
        _context.Remove(@object);
        _context.SaveChanges();
    }

    public async Task<ICollection<User>> FindByDescriptionAsync(string description)
    {
        var collection = await _context
                                     .Set<User>()
                                     .Where(p => p.Email!.Contains(description))
                                     .ToListAsync();
        return collection;
    }

    public async Task<User> FindByIdAsync(Guid id)
    {
        var @object = await _context.Set<User>()
                                       .Include(b => b.IdRoleNavigation)
                                       .AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);

        return @object!;
    }

    public async Task<ICollection<User>> ListAsync()
    {
        var collection = await _context.Set<User>()
                                       .Include(b => b.IdRoleNavigation)
                                       .AsNoTracking().ToListAsync();
        return collection;
    }

    public async Task<ICollection<Role>> ListRoleAsync()
    {
        var collection = await _context.Set<Role>().ToListAsync();
        return collection;
    }

    public async Task<User> LoginAsync(string email, string password)
    {
        var @object = await _context.Set<User>()
                                    .Include(b => b.IdRoleNavigation)
                                    .Where(p => p.Email == email && p.Password == password)
                                    .FirstOrDefaultAsync();
        return @object!;
    }

    public async Task UpdateAsync()
    {
        await _context.SaveChangesAsync();
    }
}
