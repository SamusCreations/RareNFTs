using Microsoft.AspNetCore.Mvc;
using RareNFTs.Application.DTOs;
using RareNFTs.Application.Services.Interfaces;

namespace RareNFTs.Web.Controllers;

public class NftController : Controller
{

    private readonly IServiceNft _serviceNft;
    public NftController(IServiceNft servicioNft)
    {
        _serviceNft = servicioNft;
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
        if (dto.Image  == null)
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

        await _serviceNft.UpdateAsync(id,dto);
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

    public async Task<IActionResult> GetNftByName(string filtro)
    {

        var collection = await _serviceNft.FindByDescriptionAsync(filtro);
        return Json(collection);

    }
}
