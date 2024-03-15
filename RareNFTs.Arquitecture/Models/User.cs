using System;
using System.Collections.Generic;

namespace RareNFTs.Infraestructure.Models;

public partial class User
{
    public string IdUser { get; set; } = null!;

    public string? Email { get; set; }

    public string? Password { get; set; }

    public string? IdRole { get; set; }

    public virtual Client? Client { get; set; }

    public virtual Role? IdRoleNavigation { get; set; }
}
