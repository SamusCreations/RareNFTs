using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RareNFTs.Application.DTOs;

public record CardDTO
{
    [Required(ErrorMessage = "{0} is required")]
    [Display(Name = "Code")]
    public string Id { get; set; }

    [Display(Name = "Card Description")]
    [Required(ErrorMessage = "{0} is required")]
    public string Description { get; set; } = null!;

}
