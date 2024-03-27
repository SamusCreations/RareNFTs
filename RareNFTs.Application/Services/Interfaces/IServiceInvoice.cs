using RareNFTs.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RareNFTs.Application.Services.Interfaces;

public interface IServiceInvoice
{
    Task<Guid> AddAsync(InvoiceHeaderDTO dto);
}
