using AutoMapper;
using Microsoft.Extensions.Options;
using RareNFTs.Application.Config;
using RareNFTs.Application.DTOs;
using RareNFTs.Application.Services.Interfaces;
using RareNFTs.Application.Utils;
using RareNFTs.Infraestructure.Models;
using RareNFTs.Infraestructure.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RareNFTs.Application.Services.Implementations;

public class ServiceUser : IServiceUser
{
    private readonly IRepositoryUser _repository;
    private readonly IMapper _mapper;
    private readonly IOptions<AppConfig> _options;

    public ServiceUser(IRepositoryUser repository, IMapper mapper, IOptions<AppConfig> options)
    {
        _repository = repository;
        _mapper = mapper;
        _options = options;
    }

    public async Task<string> AddAsync(UserDTO dto)
    {
        // Read secret
        string secret = _options.Value.Crypto.Secret;
        //  Get Encrypted password
        string passwordEncrypted = Cryptography.Encrypt(dto.Password!, secret);
        // Set Encrypted password to dto
        dto.Password = passwordEncrypted;
        var objectMapped = _mapper.Map<User>(dto);
        // Return
        return await _repository.AddAsync(objectMapped);
    }

    public async Task DeleteAsync(string id)
    {
        await _repository.DeleteAsync(id);
    }


    public async Task<ICollection<UserDTO>> FindByDescriptionAsync(string description)
    {
        var list = await _repository.FindByDescriptionAsync(description);
        var collection = _mapper.Map<ICollection<UserDTO>>(list);
        return collection;
    }

    public async Task<UserDTO> FindByIdAsync(string id)
    {
        var @object = await _repository.FindByIdAsync(id);
        var objectMapped = _mapper.Map<UserDTO>(@object);
        return objectMapped;
    }


    public async Task<ICollection<UserDTO>> ListAsync()
    {
        // Get data from Repository
        var list = await _repository.ListAsync();
        // Map List<*> to ICollection<*>
        var collection = _mapper.Map<ICollection<UserDTO>>(list);
        // Return Data
        return collection;
    }

    public async Task<UserDTO> LoginAsync(string id, string password)
    {
        UserDTO UserDTO = null!;

        // Read secret
        string secret = _options.Value.Crypto.Secret;
        //  Get Encrypted password
        string passwordEncrypted = Cryptography.Encrypt(password, secret);

        var @object = await _repository.LoginAsync(id, passwordEncrypted);

        if (@object != null)
        {
            UserDTO = _mapper.Map<UserDTO>(@object);
        }

        return UserDTO;
    }

    public async Task UpdateAsync(string id, UserDTO dto)
    {
        var @object = await _repository.FindByIdAsync(id);
        //       source, destination
        _mapper.Map(dto, @object!);
        await _repository.UpdateAsync();
    }
}
