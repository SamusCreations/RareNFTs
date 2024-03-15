using System;
using System.Collections.Generic;

namespace RareNFTs.Infraestructure.Models;

public partial class Client
{
    public string Id { get; set; } = null!;

    public string? Name { get; set; }

    public string? Surname { get; set; }

    public string? Genre { get; set; }

    public string? IdCountry { get; set; }

    public string? IdWallet { get; set; }

    public string? IdUser { get; set; }

    public virtual ICollection<ClientNft> ClientNft { get; set; } = new List<ClientNft>();

    public virtual Country? IdCountryNavigation { get; set; }

    public virtual User IdNavigation { get; set; } = null!;

    public virtual Wallet? IdWalletNavigation { get; set; }

    public virtual ICollection<InvoiceHeader> InvoiceHeader { get; set; } = new List<InvoiceHeader>();
}
