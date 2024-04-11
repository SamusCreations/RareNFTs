using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RareNFTs.Application.Config;
using RareNFTs.Application.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using RareNFTs.Infraestructure.Models;
using RareNFTs.Application.DTOs;
using RareNFTs.Infraestructure.Repository.Interfaces;
using System.Security.Principal;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace RareNFTs.Application.Services.Implementations;

public class ServiceInvoice : IServiceInvoice
{
    private readonly IRepositoryInvoice _repositoryInvoice;
    private readonly IRepositoryClient _repositoryClient;
    private readonly IRepositoryNft _repositoryNft;
    private readonly IMapper _mapper;
    private readonly IOptions<AppConfig> _options;
    private readonly ILogger<ServiceInvoice> _logger;
    public ServiceInvoice(IRepositoryInvoice repositoryFactura,
                          IRepositoryClient repositoryclient,
                          IRepositoryNft repositoryNft,
                          IMapper mapper,
                          IOptions<AppConfig> options,
                          ILogger<ServiceInvoice> logger)
    {
        _repositoryInvoice = repositoryFactura;
        _repositoryClient = repositoryclient;
        _repositoryNft = repositoryNft;
        _mapper = mapper;
        _options = options;
        _logger = logger;
    }

    public async Task<Guid> AddAsync(InvoiceHeaderDTO dto)
    {
        decimal total = 0;
        // Validate Stock availability
        foreach (var item in dto.ListInvoiceDetail)
        {
            var Nft = await _repositoryNft.FindByIdAsync(item.IdNft);

            if (Nft.Quantity - item.Quantity < 0)
            {
                throw new Exception($"There isn't stock available for {Nft.Description}, stock available: {Nft.Quantity}");
            }

            total += (item.Price ?? 0m) * (item.Quantity ?? 0);
        }

        dto.Total = total;
        dto.Date = DateTime.UtcNow;
        dto.Status = 1;

        var @object = _mapper.Map<InvoiceHeader>(dto);
        var client = await _repositoryClient.FindByIdAsync(dto.IdClient);
        // Create report
        await InvoiceReport(dto, client);
        // Send email
        await SendEmail(client!.Email!);
        return await _repositoryInvoice.AddAsync(@object);
    }

    public Guid GetNewId()
    {
        Guid newId = Guid.NewGuid();
        return newId;
    }

    /// <summary>
    /// Sends an email 
    /// </summary>
    /// <param name="email"></param>
    private async Task<bool> SendEmail(string email)
    {

        if (_options.Value.SmtpConfiguration == null)
        {
            _logger.LogError($"No se encuentra configurado ningun valor para SMPT en {MethodBase.GetCurrentMethod()!.DeclaringType!.FullName}");
            return false;
        }
        if (string.IsNullOrEmpty(_options.Value.SmtpConfiguration.UserName) || string.IsNullOrEmpty(_options.Value.SmtpConfiguration.FromName))
        {
            _logger.LogError($"No se encuentra configurado UserName o FromName en appSettings.json (Dev | Prod) {MethodBase.GetCurrentMethod()!.DeclaringType!.FullName}");
            return false;
        }
        var mailMessage = new MailMessage(
                new MailAddress(_options.Value.SmtpConfiguration.UserName, _options.Value.SmtpConfiguration.FromName),
                new MailAddress(email))
        {
            Subject = "Invoice for " + email,
            Body = "Attached Invoice of RareNFTs",
            IsBodyHtml = true
        };
        Attachment attachment = new Attachment(@"C:\temp\Invoice.pdf");
        mailMessage.Attachments.Add(attachment);
        using var smtpClient = new SmtpClient(_options.Value.SmtpConfiguration.Server,
                                              _options.Value.SmtpConfiguration.PortNumber)
        {
            Credentials = new NetworkCredential(_options.Value.SmtpConfiguration.UserName,
                                                _options.Value.SmtpConfiguration.Password),
            EnableSsl = _options.Value.SmtpConfiguration.EnableSsl,
        };
        await smtpClient.SendMailAsync(mailMessage);
        return true;

    }

    private async Task<byte[]> InvoiceReport(InvoiceHeaderDTO invoice, Client client)
    {
        // Get Data
        var collection = invoice.ListInvoiceDetail;

        // License config ******  IMPORTANT ******
        QuestPDF.Settings.License = QuestPDF.Infrastructure.LicenseType.Community;

        // return ByteArrays
        var pdfByteArray = QuestPDF.Fluent.Document.Create(document =>
        {
            document.Page(page =>
            {

                page.Size(PageSizes.Letter);
                page.Margin(2, Unit.Centimetre);
                page.PageColor(Colors.White);
                page.Margin(30);

                page.Header().Row(row =>
                {
                    row.RelativeItem().Column(col =>
                    {
                        col.Item().AlignLeft().Text("RareNFTs ").Bold().FontSize(16).Bold();
                        col.Item().AlignLeft().Text($"Date: {DateTime.Now} ").FontSize(12);
                        col.Item().AlignLeft().Text($"Client's ID: {client.Id} ").FontSize(12);
                        col.Item().AlignLeft().Text($"Client's name: {client.Name + " " + client.Surname} ").FontSize(12);
                        col.Item().AlignLeft().Text($"Client's email: {client.Email} ").FontSize(12);
                    });

                });


                page.Content().PaddingVertical(10).Column(col1 =>
                {
                    col1.Item().AlignCenter().Text("RareNFTs Invoice").FontSize(16).Bold();
                    col1.Item().AlignLeft().Text($"Invoice ID: {invoice.Id} ").FontSize(12);
                    col1.Item().Text("");
                    col1.Item().LineHorizontal(0.5f);

                    col1.Item().Table(async tabla =>
                    {
                        tabla.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                            columns.RelativeColumn();

                        });

                        tabla.Header(header =>
                        {
                            header.Cell().Background("#4666FF")
                            .Padding(2).AlignCenter().Text("ID").FontColor("#fff");

                            header.Cell().Background("#4666FF")
                            .Padding(2).AlignCenter().Text("NFT").FontColor("#fff");

                            header.Cell().Background("#4666FF")
                           .Padding(2).AlignCenter().Text("Image").FontColor("#fff");

                            header.Cell().Background("#4666FF")
                           .Padding(2).AlignCenter().Text("Date").FontColor("#fff");

                            header.Cell().Background("#4666FF")
                           .Padding(2).AlignCenter().Text("QTY").FontColor("#fff");

                            header.Cell().Background("#4666FF")
                           .Padding(2).AlignCenter().Text("Author").FontColor("#fff");

                            header.Cell().Background("#4666FF")
                           .Padding(2).AlignCenter().Text("Price").FontColor("#fff");
                        });

                        foreach (var item in collection)
                        {
                            // Get NFT data
                            var @object = await _repositoryNft.FindByIdAsync(item.IdNft);
                            if (@object != null)
                            {
                                // Column 1
                                tabla.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9")
                                .Padding(2).Text(@object.Id.ToString());
                                // Column 2
                                tabla.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9")
                                .Padding(2).Text(@object.Description?.ToString());
                                // Column 3
                                tabla.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9")
                                .Padding(2).Image(@object.Image).UseOriginalImage();

                                // Column 4
                                tabla.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9")
                                .Padding(2).Text(@object.Date?.ToString());

                                // Column 5
                                tabla.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9")
                                                    .Padding(2).AlignRight().Text(@object.Quantity.ToString()).FontSize(10);
                                // Column 6
                                tabla.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9")
                                                    .Padding(2).AlignRight().Text(@object.Author?.ToString()).FontSize(10);

                                // Column 7
                                tabla.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9")
                                                    .Padding(2).AlignRight().Text(@object.Price.ToString()).FontSize(10);
                            }
                        }

                    });

                    var granTotal = collection.Sum(p => p.Quantity * p.Price);

                    col1.Item().AlignRight().Text("Total " + granTotal.ToString()).FontSize(12).Bold();

                });

                page.Footer()
                .AlignRight()
                .Text(txt =>
                {
                    txt.Span("Page ").FontSize(10);
                    txt.CurrentPageNumber().FontSize(10);
                    txt.Span(" of ").FontSize(10);
                    txt.TotalPages().FontSize(10);
                });
            });
        }).GeneratePdf();

        File.WriteAllBytes(@"C:\temp\Invoice.pdf", pdfByteArray);
        return pdfByteArray;

    }
}
