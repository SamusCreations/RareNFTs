using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RareNFTs.Application.DTOs;
using RareNFTs.Application.Services.Implementations;
using RareNFTs.Application.Services.Interfaces;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RareNFTs.Web.Controllers
{
    [Authorize(Roles = "admin")]

    public class ClientController : Controller
    {
        private readonly IServiceClient _serviceClient;
        private readonly IServiceCountry _serviceCountry;

        public ClientController(IServiceClient serviceClient, IServiceCountry serviceCountry)
        {
            _serviceClient = serviceClient;
            _serviceCountry = serviceCountry;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var collection = await _serviceClient.ListAsync();
            return View(collection);
        }

        // GET: ClientController/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.ListCountry = await _serviceCountry.ListAsync();
            return View();
        }


        // POST: ClientController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ClientDTO dto)
        {
            ModelState.Remove("Id");
            if (!ModelState.IsValid)
            {
                // Lee del ModelState todos los errores que
                // vienen para el lado del server
                string errors = string.Join("; ", ModelState.Values
                                   .SelectMany(x => x.Errors)
                                   .Select(x => x.ErrorMessage));
                return BadRequest(errors);
            }

            await _serviceClient.AddAsync(dto);
            return RedirectToAction("Index");

        }


        // GET: ClientController/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            var @object = await _serviceClient.FindByIdAsync(id);
            return View(@object);
        }

        // GET: ClientController/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            ViewBag.ListCountry = await _serviceCountry.ListAsync();
            var @object = await _serviceClient.FindByIdAsync(id);
            return View(@object);
        }

        // POST: ClientController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, ClientDTO dto)
        {
            await _serviceClient.UpdateAsync(id, dto);
            return RedirectToAction("Index");
        }

        // GET: ClientController/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            var @object = await _serviceClient.FindByIdAsync(id);
            return View(@object);
        }

        // POST: ClientController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id, IFormCollection collection)
        {
            await _serviceClient.DeleteAsync(id);
            return RedirectToAction("Index");
        }

        public IActionResult GetClientByName(string filtro)
        {

            var collections = _serviceClient.FindByDescriptionAsync(filtro).GetAwaiter().GetResult();

            return Json(collections);
        }

    }
}
