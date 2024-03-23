using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RareNFTs.Application.DTOs;

public record CardDTO
{
    [Display(Name = "Card ID")]
    public Guid Id { get; set; }

    [Display(Name = "Name")]
    [Required(ErrorMessage = "{0} is required")]
    public string Description { get; set; } = null!;

}
