using RareNFTs.Application.DTOs;
using RareNFTs.Infraestructure.Models;
using System.ComponentModel.DataAnnotations;

namespace RareNFTs.Web.ViewModels;

public class ClientNftViewModel
{
    [Display(Name = "Id")]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "{0} is required")]
    [Display(Name = "Name")]
    public string Name { get; set; }

    [Required(ErrorMessage = "{0} is required")]
    [Display(Name = "Surname")]
    public string Surname { get; set; }

    [Required(ErrorMessage = "{0} is required")]
    [Display(Name = "Genre")]
    public string Genre { get; set; }

    [Required(ErrorMessage = "{0} is required")]
    [Display(Name = "Country")]
    public string IdCountry { get; set; }

    [Required(ErrorMessage = "{0} is required")]
    [Display(Name = "Email")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Client NFTs are required")]
    [Display(Name = "Client NFTs")]
    public ICollection<ClientNft> ClientNft { get; set; } = new List<ClientNft>();

    [Required(ErrorMessage = "Invoice Headers are required")]
    [Display(Name = "Invoice Headers")]
    public ICollection<InvoiceHeader> InvoiceHeader { get; set; } = new List<InvoiceHeader>();
    [Required]
    [Display(Name = "Description")]
    public string? Description { get; set; }
    [Display(Name = "Image")]
    [Required]
    public byte[] Image { get; set; } = null!;
}
