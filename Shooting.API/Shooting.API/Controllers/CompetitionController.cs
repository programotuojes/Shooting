namespace Shooting.API.Controllers {
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using AutoMapper;
  using Database;
  using Database.Entities;
  using Microsoft.AspNetCore.Mvc;
  using Models.Competition;
  using Models.Post;

  [ApiController]
  [Route("API/competitions")]
  public class CompetitionController : ControllerBase {
    private readonly IMapper mapper;

    public CompetitionController(IMapper mapper) {
      this.mapper = mapper;
    }

    [HttpPost]
    public ActionResult<CompetitionReadModel> Create(CompetitionCreateModel competitionModel) {
      var competition = new Competition {
        Id = Guid.NewGuid(),
        Name = competitionModel.Name,
        Date = competitionModel.Date,
        Competitors = mapper.Map<ICollection<Competitor>>(competitionModel.Competitors)
      };

      MockData.Competitions.Add(competition);
      var result = mapper.Map<CompetitionReadModel>(competition);
      return Created($"API/competitions/{result.Id}", result);
    }

    [HttpGet("{competitionId:guid}")]
    public ActionResult<CompetitionReadModel> Read(Guid competitionId) {
      var competition = MockData.Competitions.FirstOrDefault(x => x.Id == competitionId);
      if (competition == null) return NotFound();

      return Ok(mapper.Map<CompetitionReadModel>(competition));
    }

    [HttpGet]
    public ActionResult<CompetitionReadAllModel> ReadAll() {
      var competitions = mapper.Map<ICollection<CompetitionReadAllModel>>(MockData.Competitions);
      return Ok(competitions);
    }

    [HttpPut("{competitionId:guid}")]
    public ActionResult<CompetitionReadModel> Update(Guid competitionId, [FromBody] CompetitionCreateModel competitionModel) {
      var originalCompetition = MockData.Competitions.FirstOrDefault(x => x.Id == competitionId);
      if (originalCompetition == null) return NotFound();

      originalCompetition.Name = competitionModel.Name;
      originalCompetition.Date = competitionModel.Date;
      originalCompetition.Competitors = mapper.Map<ICollection<Competitor>>(competitionModel.Competitors);
      return Ok(mapper.Map<CompetitionReadModel>(originalCompetition));
    }

    [HttpDelete("{competitionId:guid}")]
    public ActionResult Delete(Guid competitionId) {
      var originalCompetition = MockData.Competitions.FirstOrDefault(x => x.Id == competitionId);
      if (originalCompetition == null) return NotFound();

      MockData.Competitions = MockData.Competitions.Where(x => x.Id != competitionId).ToList();
      return NoContent();
    }
  }
}
