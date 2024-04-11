using Microsoft.AspNetCore.Mvc;
using RareNFTs.Application.DTOs;
using RareNFTs.Application.Services.Interfaces;
using RareNFTs.Infraestructure.Models;
using RareNFTs.Web.ViewModels;
using System.Globalization;

namespace RareNFTs.Web.Controllers;

public class NftController : Controller
{

    private readonly IServiceNft _serviceNft;
    private readonly IServiceClient _serviceClient;
    public NftController(IServiceNft serviceNft, IServiceClient serviceClient)
    {
        _serviceNft = serviceNft;
        _serviceClient = serviceClient;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var collection = await _serviceNft.ListAsync();
        return View(collection);
    }

    // GET: NftController/Create
    public async Task<IActionResult> Create()
    {
        return View();
    }


    // POST: NftController/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(NftDTO dto, IFormFile imageFile)
    {
        MemoryStream target = new MemoryStream();

        // Cuando es Insert Image viene en null porque se pasa diferente
        if (dto.Image == null)
        {
            if (imageFile != null)
            {
                imageFile.OpenReadStream().CopyTo(target);

                dto.Image = target.ToArray();
                ModelState.Remove("Image");
            }
        }

        if (!ModelState.IsValid)
        {
            // Lee del ModelState todos los errores que
            // vienen para el lado del server
            string errors = string.Join("; ", ModelState.Values
                               .SelectMany(x => x.Errors)
                               .Select(x => x.ErrorMessage));
            // Response errores
            return BadRequest(errors);
        }

        await _serviceNft.AddAsync(dto);
        return RedirectToAction("Index");

    }


    // GET: NftController/Details/5
    public async Task<IActionResult> Details(Guid id)
    {
        var @object = await _serviceNft.FindByIdAsync(id);
        return View(@object);
    }

    // GET: NftController/Edit/5
    public async Task<IActionResult> Edit(Guid id)
    {
        var @object = await _serviceNft.FindByIdAsync(id);
        return View(@object);
    }

    // POST: NftController/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, NftDTO dto, IFormFile imageFile)
    {
        if (imageFile != null && imageFile.Length > 0)
        {
            using (var memoryStream = new MemoryStream())
            {
                await imageFile.CopyToAsync(memoryStream);
                dto.Image = memoryStream.ToArray();
            }
        }
        else
        {
            // Si decides mantener la imagen existente cuando no se sube una nueva,
            // asegúrate de que el servicio que obtiene la imagen existente esté funcionando correctamente.
            var originalNft = await _serviceNft.FindByIdAsync(id);
            dto.Image = originalNft?.Image;
        }

        // Aquí se quita el campo Image del ModelState
        ModelState.Remove("imageFile");
        ModelState.Remove("Image");

        if (!ModelState.IsValid)
        {
            string errors = string.Join("; ", ModelState.Values
                                    .SelectMany(x => x.Errors)
                                    .Select(x => x.ErrorMessage));
            return BadRequest(errors);
        }

        await _serviceNft.UpdateAsync(id, dto);

        return RedirectToAction("Index");
    }



    // GET: NftController/Delete/5
    public async Task<IActionResult> Delete(Guid id)
    {
        var @object = await _serviceNft.FindByIdAsync(id);
        return View(@object);
    }

    // POST: NftController/Delete/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(Guid id, IFormCollection collection)
    {
        await _serviceNft.DeleteAsync(id);
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> ChangeOwner()
    {
        ViewBag.ListClients = await _serviceClient.ListAsync();
        ViewBag.ListOwners = await _serviceClient.ListOwnersAsync();
        ViewBag.ListNfts = await _serviceNft.ListAsync();
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ChangeOwner(Guid nftId, Guid clientId)
    {
        var @object = await _serviceNft.ChangeNFTOwnerAsync(nftId, clientId);
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> GetNftByName(string filtro)
    {

        var collection = await _serviceNft.FindByDescriptionAsync(filtro);
        return Json(collection);

    }

    public async Task<IActionResult> GetNftOwnedByName(string name)
    {

        // Obtener la lista de ClientNft asociados al nombre del NFT
        var clientNftList = await _serviceClient.FindByNftNameAsync(name);


        var clientNft = clientNftList.FirstOrDefault();


        // Obtener la información completa del cliente utilizando el ID de cliente
        var client = await _serviceClient.FindByIdAsync(clientNft!.IdClient);

        // Obtener la información completa del NFT utilizando el ID de NFT
        var nft = await _serviceNft.FindByIdAsync(clientNft.IdNft);

        // Crear un nuevo objeto ClientNftViewModel con la información del cliente y del NFT
        var viewModel = new ClientNftViewModel
        {
            Id = client.Id,
            Name = client.Name,
            Surname = client.Surname,
            Genre = client.Genre,
            IdCountry = client.IdCountry,
            Email = client.Email,
            Description = nft.Description,
            Image = nft.Image // Suponiendo que Image es el campo que contiene la imagen del NFT
        };



        return PartialView("_ClientNft", viewModel);

    }
}
