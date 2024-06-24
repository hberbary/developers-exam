
using System.Threading.Tasks;
using Domain.Interfaces;
using Infrastructure.Data.Contexts;

namespace Infrastructure.Data.Repositories;

public class UnitOfWork : IUnitOfWork
{
  private readonly SqlDbContext _context;

  public UnitOfWork(SqlDbContext context)
  {
    _context = context;
  }

  public async Task Commit()
  {
    await _context.SaveChangesAsync();
  }
}