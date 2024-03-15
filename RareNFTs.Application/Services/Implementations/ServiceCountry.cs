using AutoMapper;
using RareNFTs.Application.DTOs;
using RareNFTs.Application.Services.Interfaces;
using RareNFTs.Infraestructure.Models;
using RareNFTs.Infraestructure.Repository.Interfaces;

namespace Electronics.Application.Services.Implementations;

public class ServiceCountry : IServiceCountry
{
    private readonly IRepositoryCountry _repository;
    private readonly IMapper _mapper;

    public ServiceCountry(IRepositoryCountry repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<string> AddAsync(CountryDTO dto)
    {
        // Map CountryDTO to Country
        var objectMapped = _mapper.Map<Country>(dto);

        // Return
        return await _repository.AddAsync(objectMapped);
    }

    public async Task DeleteAsync(string id)
    {
        await _repository.DeleteAsync(id);
    }

    public async Task<ICollection<CountryDTO>> FindByDescriptionAsync(string description)
    {
        var list = await _repository.FindByDescriptionAsync(description);
        var collection = _mapper.Map<ICollection<CountryDTO>>(list);
        return collection;

    }

    public async Task<CountryDTO> FindByIdAsync(string id)
    {
        var @object = await _repository.FindByIdAsync(id);
        var objectMapped = _mapper.Map<CountryDTO>(@object);
        return objectMapped;
    }

    public async Task<ICollection<CountryDTO>> ListAsync()
    {
        // Get data from Repository
        var list = await _repository.ListAsync();
        // Map List<Country> to ICollection<CountryDTO>
        var collection = _mapper.Map<ICollection<CountryDTO>>(list);
        // Return Data
        return collection;
    }

    public async Task UpdateAsync(string id, CountryDTO dto)
    {
        var @object = await _repository.FindByIdAsync(id);
        //       source, destination
        _mapper.Map(dto, @object!);
        await _repository.UpdateAsync();

    }
}

