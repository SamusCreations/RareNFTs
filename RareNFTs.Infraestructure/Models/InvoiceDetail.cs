using System;
using System.Collections.Generic;

namespace RareNFTs.Infraestructure.Models;

public partial class InvoiceDetail
{
    public string IdInvoice { get; set; } = null!;

    public string IdNft { get; set; } = null!;

    public decimal? Price { get; set; }

    public decimal? Tax { get; set; }

    public virtual InvoiceHeader IdInvoiceNavigation { get; set; } = null!;

    public virtual Nft IdNftNavigation { get; set; } = null!;
}
