using RareNFTs.Application.Services.Implementations;
using RareNFTs.Application.Services.Interfaces;
using RareNFTs.Web.ViewModels;
using Humanizer;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.AspNetCore.Authorization;
using RareNFTs.Web.ViewModels;
using RareNFTs.Application.Services.Interfaces;

namespace Electronics.Web.Controllers;

public class LoginController : Controller
{

    private readonly IServiceUser _serviceUser;
    private readonly ILogger<LoginController> _logger;
    public LoginController(IServiceUser serviceUsuario, ILogger<LoginController> logger)
    {
        _serviceUser = serviceUsuario;
        _logger = logger;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> LogIn(ViewModelLogin viewModelLogin)
    {

        if (!ModelState.IsValid)
        {
            // Lee del ModelState todos los errores que
            // vienen para el lado del server
            string errors = string.Join("; ", ModelState.Values
                               .SelectMany(x => x.Errors)
                               .Select(x => x.ErrorMessage));
            ViewBag.Message = $"Error de Acceso {errors}";

            _logger.LogInformation($"Error en login de {viewModelLogin}, Errores --> {errors}");
            return View("Index");
        }
        // User exist ?
        var usuarioDTO = await _serviceUser.LoginAsync(viewModelLogin.User, viewModelLogin.Password);
        if (usuarioDTO == null)
        {
            ViewBag.Message = "Error en acceso";
            _logger.LogInformation($"Error en login de {viewModelLogin.User}, Error --> {ViewBag.Message}");
            return View("Index");
        }

        // Claim stores  User information like Name, role and others.  
        List<Claim> claims = new List<Claim>() {
                new Claim(ClaimTypes.Email, usuarioDTO.Email),
                new Claim(ClaimTypes.Role, usuarioDTO.DesciptionRole!)
            };

        ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        AuthenticationProperties properties = new AuthenticationProperties()
        {
            AllowRefresh = true
        };
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity),
            properties);

        _logger.LogInformation($"Correct connection of {viewModelLogin.User}");

        return RedirectToAction("Index", "Home");
    }

    /*Only user connected can logoff*/
    [Authorize]
    public async Task<IActionResult> LogOff()
    {
        _logger.LogInformation($"Correct disconnection of {User!.Identity!.Name}");
        await HttpContext.SignOutAsync();

        return RedirectToAction("Index", "Login");
    }
}
