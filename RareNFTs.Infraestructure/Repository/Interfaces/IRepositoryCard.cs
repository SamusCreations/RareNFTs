using RareNFTs.Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RareNFTs.Infraestructure.Repository.Interfaces;

public interface IRepositoryCard
{
    Task<ICollection<Card>> FindByDescriptionAsync(string description);
    Task<ICollection<Card>> ListAsync();
    Task<Card> FindByIdAsync(string id);
    Task<string> AddAsync(Card entity);
    Task DeleteAsync(string id);
    Task UpdateAsync();

}
