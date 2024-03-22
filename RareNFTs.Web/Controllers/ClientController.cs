using Microsoft.AspNetCore.Mvc;
using RareNFTs.Application.DTOs;
using RareNFTs.Application.Services.Interfaces;

namespace RareNFTs.Web.Controllers
{
    public class ClientController : Controller
    {
        private readonly IServiceClient _serviceClient;

        public ClientController(IServiceClient serviceClient)
        {
            _serviceClient = serviceClient;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var collection = await _serviceClient.ListAsync();
            return View(collection);
        }

        // GET: ClientController/Create
        public IActionResult Create()
        {
            return View();
        }


        // POST: ClientController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ClientDTO dto)
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

            await _serviceClient.AddAsync(dto);
            return RedirectToAction("Index");

        }


        // GET: ClientController/Details/5
        public async Task<IActionResult> Details(string id)
        {
            var @object = await _serviceClient.FindByIdAsync(id);
            return View(@object);
        }

        // GET: ClientController/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            var @object = await _serviceClient.FindByIdAsync(id);
            return View(@object);
        }

        // POST: ClientController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, ClientDTO dto)
        {
            await _serviceClient.UpdateAsync(id, dto);
            return RedirectToAction("Index");
        }

        // GET: ClientController/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            var @object = await _serviceClient.FindByIdAsync(id);
            return View(@object);
        }

        // POST: ClientController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id, IFormCollection collection)
        {
            await _serviceClient.DeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}
