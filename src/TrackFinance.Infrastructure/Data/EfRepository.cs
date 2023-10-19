using Ardalis.Specification.EntityFrameworkCore;
using TrackFinance.SharedKernel.Interfaces;

namespace TrackFinance.Infrastructure.Data;
// inherit from Ardalis.Specification type
public class EfRepository<T> : RepositoryBase<T>, IReadRepository<T>, IRepository<T> where T : class, IAggregateRoot
{
  private readonly AppDbContext dbContext;
  public EfRepository(AppDbContext dbContext) : base(dbContext)
  {
    this.dbContext = dbContext;
  }
}
