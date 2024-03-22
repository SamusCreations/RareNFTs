using AutoMapper;
using RareNFTs.Application.DTOs;
using RareNFTs.Application.Services.Interfaces;
using RareNFTs.Infraestructure.Models;
using RareNFTs.Infraestructure.Repository.Interfaces;

namespace RareNFTs.Application.Services.Implementations;

public class ServiceCard : IServiceCard
{
    private readonly IRepositoryCard _repository;
    private readonly IMapper _mapper;

    public ServiceCard(IRepositoryCard repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Guid> AddAsync(CardDTO dto)
    {
        // Map CardDTO to Card
        var objectMapped = _mapper.Map<Card>(dto);

        // Return
        return await _repository.AddAsync(objectMapped);
    }

    public async Task DeleteAsync(Guid id)
    {
        await _repository.DeleteAsync(id);
    }

    public async Task<ICollection<CardDTO>> FindByDescriptionAsync(string description)
    {
        var list = await _repository.FindByDescriptionAsync(description);
        var collection = _mapper.Map<ICollection<CardDTO>>(list);
        return collection;

    }

    public async Task<CardDTO> FindByIdAsync(Guid id)
    {
        var @object = await _repository.FindByIdAsync(id);
        var objectMapped = _mapper.Map<CardDTO>(@object);
        return objectMapped;
    }

    public async Task<ICollection<CardDTO>> ListAsync()
    {
        // Get data from Repository
        var list = await _repository.ListAsync();
        // Map List<Card> to ICollection<CardDTO>
        var collection = _mapper.Map<ICollection<CardDTO>>(list);
        // Return Data
        return collection;
    }

    public async Task UpdateAsync(Guid id, CardDTO dto)
    {
        var @object = await _repository.FindByIdAsync(id);
        //       source, destination
        _mapper.Map(dto, @object!);
        await _repository.UpdateAsync();

    }
}

