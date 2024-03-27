using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RareNFTs.Application.DTOs;

public record InvoiceHeaderDTO
{


    [Display(Name = "Number Invoice")]
    [ValidateNever]
    public Guid Id { get; set; }

    [Display(Name = "Card")]
    [Required(ErrorMessage = "{0} es requerido")]
    public Guid IdCard { get; set; }

    [Display(Name = "Client")]
    [Required(ErrorMessage = "{0} es requerido")]
    public Guid IdClient { get; set; }

    [Display(Name = "Date")]
    [Required(ErrorMessage = "{0} es requerido")]
    public DateTime? Date { get; set; }

    [Display(Name = "Status")]
    [Required(ErrorMessage = "{0} es requerido")]
    public Guid IdStatus { get; set; }

    [Display(Name = "NumCard")]
    [Required(ErrorMessage = "{0} es requerido")]
    public int? NumCard { get; set; }

    [Display(Name = "Total")]
    [Required(ErrorMessage = "{0} es requerido")]
    public decimal? Total { get; set; }


    public List<InvoiceDetailDTO> ListInvoiceDetail = null!;


}
