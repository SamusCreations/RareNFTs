using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using RareNFTs.Infraestructure.Models;


namespace RareNFTs.Application.DTOs;

public record ClientDTO
{
    [Required(ErrorMessage = "{0} is required")]
    [Display(Name = "Code")]
    public string Id { get; set; }

    [Required(ErrorMessage = "{0} is required")]
    [Display(Name = "Country Name")]
    public string Name { get; set; }

    [Required(ErrorMessage = "{0} is required")]
    [Display(Name = "Surname")]
    public string Surname { get; set; }

    [Required(ErrorMessage = "{0} is required")]
    [Display(Name = "Genre")]
    public string Genre { get; set; }

    [Required(ErrorMessage = "{0} is required")]
    [Display(Name = "Country ID")]
    public string IdCountry { get; set; }

    [Required(ErrorMessage = "{0} is required")]
    [Display(Name = "Wallet ID")]
    public string IdWallet { get; set; }

    [Required(ErrorMessage = "{0} is required")]
    [Display(Name = "User ID")]
    public string IdUser { get; set; }

    [Required(ErrorMessage = "Client NFTs are required")]
    [Display(Name = "Client NFTs")]
    public  ICollection<ClientNft> ClientNft { get; set; } = new List<ClientNft>();

    [Required(ErrorMessage = "Country information is required")]
    [Display(Name = "Country Information")]
    public Country IdCountryNavigation { get; set; }

    [Required(ErrorMessage = "User information is required")]
    [Display(Name = "User Information")]
    public User IdUserNavigation { get; set; }

    [Required(ErrorMessage = "Wallet information is required")]
    [Display(Name = "Wallet Information")]
    public Wallet IdWalletNavigation { get; set; }

    [Required(ErrorMessage = "Invoice Headers are required")]
    [Display(Name = "Invoice Headers")]
    public ICollection<InvoiceHeader> InvoiceHeader { get; set; } = new List<InvoiceHeader>();
}
