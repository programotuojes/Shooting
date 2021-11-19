namespace API.Models.Competition {
  using System;
  using System.Collections.Generic;
  using System.ComponentModel.DataAnnotations;
  using Competitor;

  public class CompetitionCreateModel : IValidatableObject {
    [Required]
    [MinLength(2)]
    [MaxLength(50)]
    public string Name { get; set; } = string.Empty;

    [Required]
    public DateTime? DateFrom { get; set; }

    [Required]
    public DateTime? DateTo { get; set; }

    public ICollection<CompetitorCreateModel>? Competitors { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext) {
      if (DateFrom?.Date > DateTo?.Date) {
        yield return new ValidationResult(
          $"{nameof(DateTo)} cannot be before {nameof(DateFrom)}",
          new[] { nameof(DateTo) });
      }
    }
  }
}
