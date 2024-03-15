using System;
using System.Collections.Generic;

namespace RareNFTs.Infraestructure.Models;

public partial class InvoiceHeader
{
    public string Id { get; set; } = null!;

    public string? IdCard { get; set; }

    public string? IdClient { get; set; }

    public DateTime? Date { get; set; }

    public string? IdStatus { get; set; }

    public int? NumCard { get; set; }

    public decimal? Total { get; set; }

    public virtual Card? IdCardNavigation { get; set; }

    public virtual Client? IdClientNavigation { get; set; }

    public virtual InvoiceStatus? IdStatusNavigation { get; set; }

    public virtual ICollection<InvoiceDetail> InvoiceDetail { get; set; } = new List<InvoiceDetail>();
}
