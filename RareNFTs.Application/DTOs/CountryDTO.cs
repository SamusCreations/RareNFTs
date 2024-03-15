using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RareNFTs.Application.DTOs;

public record CountryDTO
{
    [Required(ErrorMessage = "{0} is required")]
    [Display(Name = "Code")]
    public string Id { get; set; }

    [Display(Name = "Country Name")]
    [Required(ErrorMessage = "{0} is required")]
    public string Name { get; set; } = null!;

}
