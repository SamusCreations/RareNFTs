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
        //Attachment attachment = new Attachment(@"c:\\temp\\factura.pdf");
        //mailMessage.Attachments.Add(attachment);
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
}
