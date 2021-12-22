namespace API.Models.Competitor {
  using System;
  using System.Collections.Generic;
  using System.ComponentModel.DataAnnotations;

  public class CompetitorCreateModel : IValidatableObject {

    [Required]
    [MinLength(1)]
    [MaxLength(30)]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    [MinLength(1)]
    [MaxLength(30)]
    public string LastName { get; set; } = string.Empty;

    [Required]
    public int? BirthYear { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext) {
      if (BirthYear < 1800 || BirthYear > DateTime.UtcNow.Year) {
        yield return new ValidationResult(
          $"{nameof(BirthYear)} should be between 1800 and {DateTime.UtcNow.Year}",
          new[] { nameof(BirthYear) });
      }
    }
  }
}
