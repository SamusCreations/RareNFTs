using System.ComponentModel.DataAnnotations;

namespace RareNFTs.Web.ViewModels;
 
  
public record ViewModelChangeOwner
{
    [Required]
    [Display(Name = "NFT")]
    public Guid IdNft { get; set; }

    [Required]
    [Display(Name = "New owner")]
    public Guid IdClient { get; set; }

    [Required]  
    [Display(Name = "Current owner")]
    public Guid IdOwner { get; set; }

}