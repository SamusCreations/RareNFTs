using Microsoft.AspNetCore.Mvc;
using RareNFTs.Application.Services.Interfaces;

namespace RareNFTs.Web.Controllers;

public class InvoiceController : Controller
{
    private readonly IServiceNft _serviceNft;
    private readonly IServiceCard _serviceCard;
    private readonly IServiceInvoice _serviceInvoice;
 

    public InvoiceController(IServiceNft serviceNft,
                            IServiceCard serviceCard,
                            IServiceInvoice serviceInvoice)
    {
        _serviceNft = serviceNft;
        _serviceCard = serviceCard;
        _serviceInvoice= serviceInvoice;
        
    }

    public async Task<IActionResult> Index()
    {

        var nextReceiptNumber = await _serviceInvoice.GetNextReceiptNumber();
        ViewBag.CurrentReceipt = nextReceiptNumber;
        var collection = await _serviceTarjeta.ListAsync();
        ViewBag.ListTarjeta = collection;

        // Clear CarShopping
        TempData["CartShopping"] = null;
        TempData.Keep();

        return View();
    }


    public async Task<IActionResult> AddProduct(int id, int cantidad)
    {
        FacturaDetalleDTO facturaDetalleDTO = new FacturaDetalleDTO();
        List<FacturaDetalleDTO> lista = new List<FacturaDetalleDTO>();
        string json = "";
        decimal impuesto = 0;
        var producto = await _serviceProducto.FindByIdAsync(id);

        // Stock ??

        if (cantidad > producto.Cantidad)
        {
            return BadRequest("No hay inventario suficiente!");
        }

        impuesto = await _serviceImpuesto.GetImpuesto();
        facturaDetalleDTO.DescripcionProducto = producto.DescripcionProducto;
        facturaDetalleDTO.Cantidad = cantidad;
        facturaDetalleDTO.Precio = producto.Precio;
        facturaDetalleDTO.IdProducto = id;
        facturaDetalleDTO.TotalLinea = (facturaDetalleDTO.Precio * facturaDetalleDTO.Cantidad) + impuesto;
        facturaDetalleDTO.Impuesto = (cantidad * producto.Precio) * (impuesto / 100);

        if (TempData["CartShopping"] == null)
        {
            lista.Add(facturaDetalleDTO);
            // Reenumerate 
            int idx = 1;
            lista.ForEach(p => p.Secuencia = idx++);
            json = JsonSerializer.Serialize(lista);
            TempData["CartShopping"] = json;
        }
        else
        {
            json = (string)TempData["CartShopping"]!;
            lista = JsonSerializer.Deserialize<List<FacturaDetalleDTO>>(json!)!;
            lista.Add(facturaDetalleDTO);
            // Reenumerate 
            int idx = 1;
            lista.ForEach(p => p.Secuencia = idx++);
            json = JsonSerializer.Serialize(lista);
            TempData["CartShopping"] = json;
        }

        TempData.Keep();
        return PartialView("_DetailFactura", lista);
    }

    public IActionResult GetDetailFactura()
    {
        List<FacturaDetalleDTO> lista = new List<FacturaDetalleDTO>();
        string json = "";
        json = (string)TempData["CartShopping"]!;
        lista = JsonSerializer.Deserialize<List<FacturaDetalleDTO>>(json!)!;
        // Reenumerate 
        int idx = 1;
        lista.ForEach(p => p.Secuencia = idx++);
        json = JsonSerializer.Serialize(lista);
        TempData["CartShopping"] = json;
        TempData.Keep();

        return PartialView("_DetailFactura", lista);
    }

    public IActionResult DeleteProduct(int id)
    {
        FacturaDetalleDTO facturaDetalleDTO = new FacturaDetalleDTO();
        List<FacturaDetalleDTO> lista = new List<FacturaDetalleDTO>();
        string json = "";

        if (TempData["CartShopping"] != null)
        {
            json = (string)TempData["CartShopping"]!;
            lista = JsonSerializer.Deserialize<List<FacturaDetalleDTO>>(json!)!;
            // Remove from list by Index
            int idx = lista.FindIndex(p => p.Secuencia == id);
            lista.RemoveAt(idx);
            json = JsonSerializer.Serialize(lista);
            TempData["CartShopping"] = json;
        }

        TempData.Keep();

        // return Content("Ok");
        return PartialView("_DetailFactura", lista);

    }


    public async Task<IActionResult> Create(FacturaEncabezadoDTO facturaEncabezadoDTO)
    {
        string json;
        try
        {

            if (!ModelState.IsValid)
            {

            }

            json = (string)TempData["CartShopping"]!;

            if (string.IsNullOrEmpty(json))
            {
                return BadRequest("No hay datos por facturar");
            }

            var lista = JsonSerializer.Deserialize<List<FacturaDetalleDTO>>(json!)!;

            //Mismo numero de factura para el detalle FK
            lista.ForEach(p => p.IdFactura = facturaEncabezadoDTO.IdFactura);
            facturaEncabezadoDTO.ListFacturaDetalle = lista;

            await _serviceFactura.AddAsync(facturaEncabezadoDTO);


            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            // Keep Cache data
            TempData.Keep();
            return BadRequest(ex.Message);
        }
    }
}
