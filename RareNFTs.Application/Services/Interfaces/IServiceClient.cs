using RareNFTs.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RareNFTs.Application.Services.Interfaces;

public interface IServiceClient
{
    Task<ICollection<ClientDTO>> FindByDescriptionAsync(string description);
    Task<ICollection<ClientDTO>> ListAsync();
    Task<ClientDTO> FindByIdAsync(string id);
    Task<string> AddAsync(ClientDTO dto);
    Task DeleteAsync(string id);
    Task UpdateAsync(string id, ClientDTO dto);
}
