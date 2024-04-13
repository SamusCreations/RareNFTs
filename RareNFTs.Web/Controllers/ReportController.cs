using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Mvc;
using RareNFTs.Application.Services.Interfaces;
using RareNFTs.Infraestructure.Models;
using RareNFTs.Web.ViewModels;

namespace RareNFTs.Web.Controllers;

public class ReportController : Controller
{
    private readonly IServiceReport _serviceReport;
    private readonly IServiceClient _serviceClient;
    private readonly IServiceNft _serviceNft;
    public ReportController(IServiceReport serviceReport, IServiceClient serviceClient, IServiceNft serviceNft)
    {
        _serviceReport = serviceReport;
        _serviceClient = serviceClient;
        _serviceNft = serviceNft;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult ProductReport()
    {
        return View();
    }

    public IActionResult ClientReport()
    {
        return View();
    }

    public IActionResult OwnerNftReport()
    {
        return View();
    }

    public IActionResult SalesReport()
    {
        return View();
    }

    [HttpPost]
    [RequireAntiforgeryToken]
    public async Task<FileResult> ProductReportPDF()
    {

        byte[] bytes = await _serviceReport.ProductReport();
        return File(bytes, "text/plain", "ProductReport.pdf");

    }

    [HttpPost]
    [RequireAntiforgeryToken]
    public async Task<FileResult> ClientReportPDF()
    {

        byte[] bytes = await _serviceReport.ClientReport();
        return File(bytes, "text/plain", "ClientReport.pdf");

    }

    [HttpPost]
    [RequireAntiforgeryToken]
    public async Task<FileResult> SalesReportPDF(DateTime startDate, DateTime endDate)
    {

        byte[] bytes = await _serviceReport.SalesReport(startDate, endDate);
        return File(bytes, "text/plain", "SalesReport.pdf");

    }

    [RequireAntiforgeryToken]

     public async Task<IActionResult> GetOwnerByNft(string name)
    {
        // Obtener la lista de ClientNft asociados al nombre del NFT
        var clientNftList = await _serviceClient.FindByNftNameAsync(name);

        // Lista para almacenar la información completa del cliente y del NFT
        var viewModelList = new List<ViewModelClientNft>();

        // Recorrer la lista de ClientNft
        foreach (var clientNft in clientNftList)
        {
            // Obtener la información completa del cliente utilizando el ID de cliente
            var client = await _serviceClient.FindByIdAsync(clientNft.IdClient);

            // Obtener la información completa del NFT utilizando el ID de NFT
            var nft = await _serviceNft.FindByIdAsync(clientNft.IdNft);

            // Crear un nuevo objeto ClientNftViewModel con la información del cliente y del NFT
            var viewModel = new ViewModelClientNft
            {
                IdClient = client.Id,
                Name = client.Name,
                Surname = client.Surname,
                Genre = client.Genre,
                IdCountry = client.IdCountry,
                Email = client.Email,
                Description = nft.Description,
                Image = nft.Image // Suponiendo que Image es el campo que contiene la imagen del NFT
            };

            // Agregar el objeto ClientNftViewModel a la lista
            viewModelList.Add(viewModel);
        }

        return PartialView("_ClientByNftReport", viewModelList);
    }


}
