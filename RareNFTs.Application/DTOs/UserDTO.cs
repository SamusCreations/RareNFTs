using RareNFTs.Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RareNFTs.Application.DTOs;

public record class UserDTO
{

    public Guid Id { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public Guid IdRole { get; set; }
    public string? DesciptionRole { get; set; } = default;

    public virtual Role IdRolNavigation { get; set; } = null!;
}
