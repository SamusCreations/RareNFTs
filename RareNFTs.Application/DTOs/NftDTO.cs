using RareNFTs.Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RareNFTs.Application.DTOs;

public record NftDTO
{
    [Display(Name = "Id")]
    [Required(ErrorMessage = "{0} es requerido")]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "{0} es requerido")]
    [Display(Name = "Description")]
    public string? Description { get; set; }

    [Required(ErrorMessage = "{0} es requerido")]
    [Range(0, 999999999, ErrorMessage = "The minimun price is {0}")]
    [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:###,###.00}")]
    [Display(Name = "Price")]
    public decimal? Price { get; set; }

    [Display(Name = "Image")]
    //[Required(ErrorMessage = "{0} es requerido")]
    public byte[] Image { get; set; } = null!;

    [Display(Name = "Date")]
    [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:yyyy-MM-dd}")]
    public DateTime Date { get; set; } = DateTime.Now;

    [Required(ErrorMessage = "{0} es requerido")]
    [Display(Name = "Author")]
    public string? Author { get; set; }

    [Display(Name = "Quantity")]
    [Range(0, 999999999, ErrorMessage = "The minimum value is {0}")]
    [Required(ErrorMessage = "{0} es requerido")]
    public int? Quantity { get; set; }



}
