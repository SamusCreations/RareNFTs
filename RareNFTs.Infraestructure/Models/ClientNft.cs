using System;
using System.Collections.Generic;

namespace RareNFTs.Infraestructure.Models;

public partial class ClientNft
{
    public string IdClient { get; set; } = null!;

    public string IdNft { get; set; } = null!;

    public DateTime? Date { get; set; }

    public virtual Client IdClientNavigation { get; set; } = null!;

    public virtual Nft IdNftNavigation { get; set; } = null!;
}
