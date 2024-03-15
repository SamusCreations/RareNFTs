using System;
using System.Collections.Generic;

namespace RareNFTs.Infraestructure.Models;

public partial class Wallet
{
    public string Id { get; set; } = null!;

    public decimal? Purse { get; set; }

    public virtual ICollection<Client> Client { get; set; } = new List<Client>();
}
