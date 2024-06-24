using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Domain.Interfaces;
using Domain.Entities;

namespace Infrastructure.Data.Contexts;

public class SqlDbContext : DbContext
{
    private readonly IDomainEventHandler _domainEventService;
    public DbSet<UserEntity> Users { get; set; }

    public SqlDbContext(DbContextOptions<SqlDbContext> options, IDomainEventHandler domainEventService) : base(options)
        => _domainEventService = domainEventService;

    public SqlDbContext(DbContextOptions<SqlDbContext> option) : base(option) { }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var result = await base.SaveChangesAsync(cancellationToken);

        await DispatchEvents();

        return result;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        var dateTimeConverter = new ValueConverter<DateTime, DateTime>(v => v, v => DateTime.SpecifyKind(v, DateTimeKind.Utc));

        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            foreach (var property in entityType.GetProperties())
            {
                if (property.ClrType == typeof(DateTime) || property.ClrType == typeof(DateTime?))
                    property.SetValueConverter(dateTimeConverter);
            }
        }

        base.OnModelCreating(modelBuilder);
    }

    private async Task DispatchEvents()
    {
        while (true)
        {
            try
            {
                var domainEventEntity = ChangeTracker.Entries<IHasDomainEvent>()
                  .Select(x => x.Entity.DomainEvents)
                  .SelectMany(x => x)
                  .Where(domainEvent => !domainEvent.IsPublished)
                  .FirstOrDefault();

                if (domainEventEntity is null) break;

                domainEventEntity.IsPublished = true;

                await _domainEventService.Publish(domainEventEntity);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}