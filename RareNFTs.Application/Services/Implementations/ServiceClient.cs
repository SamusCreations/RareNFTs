using AutoMapper;
using RareNFTs.Application.DTOs;
using RareNFTs.Application.Services.Interfaces;
using RareNFTs.Infraestructure.Models;
using RareNFTs.Infraestructure.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RareNFTs.Application.Services.Implementations;

public class ServiceClient: IServiceClient
{
    private readonly IRepositoryClient _repository;
    private readonly IMapper _mapper;

    public ServiceClient(IRepositoryClient repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Guid> AddAsync(ClientDTO dto)
    {
        // Map ClientDTO to Client
        var objectMapped = _mapper.Map<Client>(dto);

        // Return
        return await _repository.AddAsync(objectMapped);
    }

    public async Task DeleteAsync(Guid id)
    {
        await _repository.DeleteAsync(id);
    }

    public async Task<ICollection<ClientDTO>> FindByDescriptionAsync(string description)
    {
        var list = await _repository.FindByDescriptionAsync(description);
        var collection = _mapper.Map<ICollection<ClientDTO>>(list);
        return collection;

    }

    public async Task<ClientDTO> FindByIdAsync(Guid id)
    {
        var @object = await _repository.FindByIdAsync(id);
        var objectMapped = _mapper.Map<ClientDTO>(@object);
        return objectMapped;
    }

    public async Task<IEnumerable<ClientNftDTO>> FindByNftNameAsync(string name)
    {
        // Get data from Repository
        var list = await _repository.FindByNftNameAsync(name);
        // Map List<Bodega> to ICollection<BodegaDTO>
        var collection = _mapper.Map<ICollection<ClientNftDTO>>(list);
        // Return Data
        return collection;
    }

    public async Task<ICollection<ClientDTO>> ListAsync()
    {
        // Get data from Repository
        var list = await _repository.ListAsync();
        // Map List<Client> to ICollection<ClientDTO>
        var collection = _mapper.Map<ICollection<ClientDTO>>(list);
        // Return Data
        return collection;
    }

    public async Task UpdateAsync(Guid id, ClientDTO dto)
    {
        var @object = await _repository.FindByIdAsync(id);
        //       source, destination
        _mapper.Map(dto, @object!);
        await _repository.UpdateAsync();

    }

   
}
