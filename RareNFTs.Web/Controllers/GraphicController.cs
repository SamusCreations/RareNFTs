using RareNFTs.Application.Services.Implementations;
using RareNFTs.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RareNFTs.Infraestructure.Models;
using static QuestPDF.Helpers.Colors;

namespace RareNFTs.Web.Controllers;

[Authorize]
public class GraphicController : Controller
{
    private readonly IServiceNft _serviceNft;
    private readonly IServiceReport _serviceReport;
    private readonly IServiceClient _serviceClient;
    private readonly IServiceInvoice _serviceInvoice;

    public GraphicController(IServiceNft serviceProducto,
                             IServiceReport serviceReporte,
                             IServiceClient serviceCliente,
                             IServiceInvoice serviceInvoice)
    {
        _serviceNft = serviceProducto;
        _serviceReport = serviceReporte;
        _serviceClient = serviceCliente;
        _serviceInvoice =   serviceInvoice;
    }

    public IActionResult Index()
    {
        return View();
    }



    public IActionResult GraphicSalesDateRange()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> GraphicSalesDateRange(DateTime StartDate, DateTime EndDate)
    {
        if (StartDate == default || EndDate == default)
        {
            ViewBag.Message = "Both start and end dates must be selected.";
            return View();
        }

        var salesData = await _serviceInvoice.FindByDateRangeAsync(StartDate, EndDate);

        if (salesData == null || !salesData.Any())
        {
            ViewBag.Message = "No sales data found for the selected date range.";
            return View();
        }

        ViewBag.Valores = salesData.Select(s => s.Total).ToArray();
        ViewBag.Etiquetas = salesData.Select(s => s.Date.ToString()).ToArray();
        ViewBag.GraphTitle = $"Sales from {StartDate.ToShortDateString()} to {EndDate.ToShortDateString()} -  Total Sales {salesData.Sum(s => s.Total)} " ;

        return View(salesData);
    }

}






