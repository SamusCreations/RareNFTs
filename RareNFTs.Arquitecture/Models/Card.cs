using System;
using System.Collections.Generic;

namespace RareNFTs.Infraestructure.Models;

public partial class Card
{
    public string Id { get; set; } = null!;

    public string? Description1 { get; set; }

    public virtual ICollection<InvoiceHeader> InvoiceHeader { get; set; } = new List<InvoiceHeader>();
}
