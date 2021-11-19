namespace API.Models.Competitor {
  public class CompetitorReadModel {
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public int BirthYear { get; set; }
    public float? Result { get; set; }
  }
}
