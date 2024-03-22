using RareNFTs.Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RareNFTs.Infraestructure.Repository.Interfaces;

public interface IRepositoryClient
{
    Task<ICollection<Client>> FindByDescriptionAsync(string description);
    Task<ICollection<Client>> ListAsync();
    Task<Client> FindByIdAsync(string id);
    Task<string> AddAsync(Client entity);
    Task DeleteAsync(string id);
    Task UpdateAsync();
}
