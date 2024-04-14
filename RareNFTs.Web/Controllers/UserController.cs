using RareNFTs.Application.DTOs;
using RareNFTs.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
    public IActionResult Create()
    {
        return View();
    }

    // GET:  
    public async Task<IActionResult> Login(string email, string password)
    {
        var @object = await _serviceUser.LoginAsync(email, password);
        if (@object == null)
        {
            ViewBag.Message = "Error en Login o Password";
            return View("Login");
        }
        else
        {
            return RedirectToAction("Index", "Home");
        }


    }


    // POST: UsuarioController/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(UserDTO dto)
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

        await _serviceUser.AddAsync(dto);
        return RedirectToAction("Index");

    }


    // GET: UsuarioController/Details/5
    public async Task<IActionResult> Details(string id)
    {
        var @object = await _serviceUser.FindByIdAsync(id);
        return View(@object);
    }

    // GET: UsuarioController/Edit/5
    public async Task<IActionResult> Edit(string id)
    {
        var @object = await _serviceUser.FindByIdAsync(id);
        return View(@object);
    }

    // POST: UsuarioController/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(string id, UserDTO dto)
    {
        await _serviceUser.UpdateAsync(id, dto);
        return RedirectToAction("Index");
    }

    // GET: UsuarioController/Delete/5
    public async Task<IActionResult> Delete(string id)
    {
        var @object = await _serviceUser.FindByIdAsync(id);
        return View(@object);
    }

    // POST: UsuarioController/Delete/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(string id, IFormCollection collection)
    {
        await _serviceUser.DeleteAsync(id);
        return RedirectToAction("Index");
    }
}
