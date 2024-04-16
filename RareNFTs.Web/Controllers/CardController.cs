using RareNFTs.Application.DTOs;
using RareNFTs.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.AspNetCore.Authorization;

namespace RareNFTs.Web.Controllers;
[Authorize(Roles = "admin,process")]

public class CardController : Controller
{
    private readonly IServiceCard _serviceCard;

    public CardController(IServiceCard serviceCard)
    {
        _serviceCard = serviceCard;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var collection = await _serviceCard.ListAsync();
        return View(collection);
    }

    // GET: CardController/Create
    public IActionResult Create()
    {
        return View();
    }


    // POST: CardController/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CardDTO dto)
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

        await _serviceCard.AddAsync(dto);
        return RedirectToAction("Index");

    }


    // GET: CardController/Details/5

    public async Task<IActionResult> Details(Guid id)
    {
        var @object = await _serviceCard.FindByIdAsync(id);
        return PartialView("_Details",@object);
    }

    // GET: CardController/Edit/5
    public async Task<IActionResult> Edit(Guid id)
    {
        var @object = await _serviceCard.FindByIdAsync(id);
        return View(@object);
    }

    // POST: CardController/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, CardDTO dto)
    {
        await _serviceCard.UpdateAsync(id, dto);
        return RedirectToAction("Index");
    }


    // GET: CardController/Delete/5
    public async Task<IActionResult> Delete(Guid id)
    {
        var @object = await _serviceCard.FindByIdAsync(id);
        return View(@object);
    }

    // POST: CardController/Delete/5
    [HttpPost]

    public async Task<IActionResult> Delete(Guid id, IFormCollection collection)
    {
        await _serviceCard.DeleteAsync(id);
        return RedirectToAction("Index");
    }
}
