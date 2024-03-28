﻿using Microsoft.AspNetCore.Mvc;
using RareNFTs.Application.DTOs;
using RareNFTs.Application.Services.Interfaces;
using System.Text.Json;

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

        var newId = _serviceInvoice.GetNewId();
        ViewBag.InvoiceId = newId;
        var collection = await _serviceCard.ListAsync();
        ViewBag.ListCard = collection;

        // Clear CartShopping
        TempData["CartShopping"] = null;
        TempData.Keep();

        return View();
    }


    public async Task<IActionResult> AddProduct(Guid id, int quantity)
    {
        InvoiceDetailDTO invoiceDetailDTO = new InvoiceDetailDTO();
        List<InvoiceDetailDTO> list = new List<InvoiceDetailDTO>();
        string json = "";
        var nft = await _serviceNft.FindByIdAsync(id);

        // Stock ??

        if (quantity > nft.Quantity)
        {
            return BadRequest("No stock available!");
        }

        invoiceDetailDTO.NftDescription = nft.Description!;
        invoiceDetailDTO.Quantity = quantity;
        invoiceDetailDTO.Price = nft.Price;
        invoiceDetailDTO.IdNft = id;
        invoiceDetailDTO.TotalLine = (invoiceDetailDTO.Price * invoiceDetailDTO.Quantity);

        if (TempData["CartShopping"] == null)
        {
            list.Add(invoiceDetailDTO);
            // Reenumerate 
            int idx = 1;
            list.ForEach(p => p.Sequence = idx++);
            json = JsonSerializer.Serialize(list);
            TempData["CartShopping"] = json;
        }
        else
        {
            json = (string)TempData["CartShopping"]!;
            list = JsonSerializer.Deserialize<List<InvoiceDetailDTO>>(json!)!;
            list.Add(invoiceDetailDTO);
            // Reenumerate 
            int idx = 1;
            list.ForEach(p => p.Sequence = idx++);
            json = JsonSerializer.Serialize(list);
            TempData["CartShopping"] = json;
        }

        TempData.Keep();
        return PartialView("_InvoiceDetail", list);
    }

    public IActionResult GetInvoiceDetail()
    {
        List<InvoiceDetailDTO> list = new List<InvoiceDetailDTO>();
        string json = "";
        json = (string)TempData["CartShopping"]!;
        list = JsonSerializer.Deserialize<List<InvoiceDetailDTO>>(json!)!;
        // Reenumerate 
        int idx = 1;
        list.ForEach(p => p.Sequence = idx++);
        json = JsonSerializer.Serialize(list);
        TempData["CartShopping"] = json;
        TempData.Keep();

        return PartialView("_InvoiceDetail", list);
    }

    public IActionResult DeleteProduct(int id)
    {
        InvoiceDetailDTO invoiceDetailDTO = new InvoiceDetailDTO();
        List<InvoiceDetailDTO> list = new List<InvoiceDetailDTO>();
        string json = "";

        if (TempData["CartShopping"] != null)
        {
            json = (string)TempData["CartShopping"]!;
            list = JsonSerializer.Deserialize<List<InvoiceDetailDTO>>(json!)!;
            // Remove from list by Index
            int idx = list.FindIndex(p => p.Sequence == id);
            list.RemoveAt(idx);
            json = JsonSerializer.Serialize(list);
            TempData["CartShopping"] = json;
        }

        TempData.Keep();

        // return Content("Ok");
        return PartialView("_DetailFactura", list);

    }


    public async Task<IActionResult> Create(InvoiceHeaderDTO InvoiceHeaderDTO)
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

            var list = JsonSerializer.Deserialize<List<InvoiceDetailDTO>>(json!)!;

            //Mismo numero de factura para el detalle FK
            list.ForEach(p => p.IdInvoice = InvoiceHeaderDTO.Id);
            InvoiceHeaderDTO.ListInvoiceDetail = list;

            await _serviceInvoice.AddAsync(InvoiceHeaderDTO);


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