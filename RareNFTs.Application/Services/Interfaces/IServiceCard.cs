using RareNFTs.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RareNFTs.Application.Services.Interfaces
{
    public interface IServiceCard
    {
        Task<ICollection<CardDTO>> FindByDescriptionAsync(string description);
        Task<ICollection<CardDTO>> ListAsync();
        Task<CardDTO> FindByIdAsync(string id);
        Task<string> AddAsync(CardDTO dto);
        Task DeleteAsync(string id);
        Task UpdateAsync(string id, CardDTO dto);
    }
}
