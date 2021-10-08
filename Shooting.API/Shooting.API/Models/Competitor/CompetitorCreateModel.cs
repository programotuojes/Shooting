namespace Shooting.API.Models.Competitor {
  using System.ComponentModel.DataAnnotations;

  public class CompetitorCreateModel {
    [Required]
    [MinLength(1)]
    [MaxLength(30)]
    public string FirstName { get; set; }

    [Required]
    [MinLength(1)]
    [MaxLength(30)]
    public string LastName { get; set; }
  }
}
