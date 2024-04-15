using RareNFTs.Application.DTOs;
using RareNFTs.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.AspNetCore.Authorization;

namespace RareNFTs.Web.Controllers;
[Authorize(Roles = "admin,process")]

public class CountryController : Controller
{
    private readonly IServiceCountry _serviceCountry;

    public CountryController(IServiceCountry serviceCountry)
    {
        _serviceCountry = serviceCountry;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var collection = await _serviceCountry.ListAsync();
        return View(collection);
    }

    // GET: CountryController/Create
    public IActionResult Create()
    {
        return View();
    }


    // POST: CountryController/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CountryDTO dto)
    {

        if (!ModelState.IsValid)
        {
            // Lee del ModelState todos los errores que
            // vienen para el lado del server
            string errors = string.Join("; ", ModelState.Values
                               .SelectMany(x => x.Errors)
                               .Select(x => x.ErrorMessage));
            return BadRequest(errors);
        }

        await _serviceCountry.AddAsync(dto);
        return RedirectToAction("Index");

    }


    // GET: CountryController/Details/5
    public async Task<IActionResult> Details(string id)
    {
        var @object = await _serviceCountry.FindByIdAsync(id);
        return View(@object);
    }

    // GET: CountryController/Edit/5
    public async Task<IActionResult> Edit(string id)
    {
        var @object = await _serviceCountry.FindByIdAsync(id);
        return View(@object);
    }

    // POST: CountryController/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(string id, CountryDTO dto)
    {
        await _serviceCountry.UpdateAsync(id, dto);
        return RedirectToAction("Index");
    }

    // GET: CountryController/Delete/5
    public async Task<IActionResult> Delete(string id)
    {
        var @object = await _serviceCountry.FindByIdAsync(id);
        return View(@object);
    }

    // POST: CountryController/Delete/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(string id, IFormCollection collection)
    {
        await _serviceCountry.DeleteAsync(id);
        return RedirectToAction("Index");
    }
}
