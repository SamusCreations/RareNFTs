using RareNFTs.Application.DTOs;
using RareNFTs.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RareNFTs.Application.Services.Implementations;

namespace Electronics.Web.Controllers;

[Authorize(Roles = "admin")]
public class UserController : Controller
{
    private readonly IServiceUser _serviceUser;

    public UserController(IServiceUser serviceUsuario)
    {
        _serviceUser = serviceUsuario;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var collection = await _serviceUser.ListAsync();
        return View(collection);
    }

    // GET: UsuarioController/Create
    public async Task<IActionResult> Create()
    {
        ViewBag.ListRole = await _serviceUser.ListRoleAsync();
        return View();
    }

    // POST: UsuarioController/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(UserDTO dto)
    {
        ModelState.Remove("IdRoleNavigation");
        if (!ModelState.IsValid)
        {
            // Lee del ModelState todos los errores que
            // vienen para el lado del server
            string errors = string.Join("; ", ModelState.Values
                               .SelectMany(x => x.Errors)
                               .Select(x => x.ErrorMessage));
            return BadRequest(errors);
        }

        await _serviceUser.AddAsync(dto);
        return RedirectToAction("Index");

    }


    // GET: UsuarioController/Details/5
    public async Task<IActionResult> Details(Guid id)
    {
        var @object = await _serviceUser.FindByIdAsync(id);
        return PartialView("_Details", @object);
    }

    // GET: UsuarioController/Edit/5
    public async Task<IActionResult> Edit(Guid id)
    {
        ViewBag.ListRole = await _serviceUser.ListRoleAsync();

        var @object = await _serviceUser.FindByIdAsync(id);
        return View(@object);
    }

    // POST: UsuarioController/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, UserDTO dto)
    {
        await _serviceUser.UpdateAsync(id, dto);
        return RedirectToAction("Index");
    }

    // GET: UsuarioController/Delete/5
    public async Task<IActionResult> Delete(Guid id)
    {
        var @object = await _serviceUser.FindByIdAsync(id);
        return View(@object);
    }

    // POST: UsuarioController/Delete/5
    [HttpPost]
    public async Task<IActionResult> Delete(Guid id, IFormCollection collection)
    {
        await _serviceUser.DeleteAsync(id);
        return RedirectToAction("Index");
    }
}
