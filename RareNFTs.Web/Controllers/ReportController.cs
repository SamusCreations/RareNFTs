using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Mvc;
using RareNFTs.Application.Services.Interfaces;

namespace RareNFTs.Web.Controllers;

public class ReportController : Controller
{
    private readonly IServiceReport _serviceReport;
    public ReportController(IServiceReport serviceReport)
    {
        _serviceReport = serviceReport;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult ProductReport()
    {
        return View();
    }

    [HttpPost]
    [RequireAntiforgeryToken]
    public async Task<FileResult> ProductReportPDF()
    {

        byte[] bytes = await _serviceReport.ProductReport();
        return File(bytes, "text/plain", "file.pdf");

    }
}
