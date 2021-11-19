namespace API.Services {
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using AutoMapper;
  using AutoMapper.QueryableExtensions;
  using DB;
  using DB.Entities;
  using Microsoft.EntityFrameworkCore;
  using Models.Competition;

  public class CompetitionService {

    private readonly DataContext dataContext;
    private readonly IMapper mapper;

    public CompetitionService(DataContext dataContext, IMapper mapper) {
      this.dataContext = dataContext;
      this.mapper = mapper;
    }

    public CompetitionReadModel Create(CompetitionCreateModel model) {
      var competition = mapper.Map<Competition>(model);
      dataContext.Add(competition);
      dataContext.SaveChanges();

      return mapper.Map<CompetitionReadModel>(competition);
    }

    public CompetitionReadModel? Read(Guid id) {
      return dataContext.Competitions
        .Where(x => x.Id == id)
        .AsNoTracking()
        .ProjectTo<CompetitionReadModel>(mapper.ConfigurationProvider)
        .FirstOrDefault();
    }

    public IEnumerable<CompetitionReadAllModel> ReadAll() {
      return dataContext.Competitions
        .AsNoTracking()
        .ProjectTo<CompetitionReadAllModel>(mapper.ConfigurationProvider);
    }

    public CompetitionReadModel? Update(Guid id, CompetitionCreateModel model) {
      var competition = dataContext.Competitions
        .Include(x => x.Competitors)
        .FirstOrDefault(x => x.Id == id);

      if (competition == null) return null;

      mapper.Map(model, competition);
      dataContext.Update(competition);
      dataContext.SaveChanges();

      return mapper.Map<CompetitionReadModel>(competition);
    }

    public bool Delete(Guid id) {
      var competition = dataContext.Competitions.Find(id);
      if (competition == null) return false;

      dataContext.Entry(competition).State = EntityState.Deleted;
      dataContext.SaveChanges();

      return true;
    }
  }
}
