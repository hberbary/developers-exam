
using Domain.Interfaces.Repositories;
using Domain.Entities;
using Domain.Services;
using Infrastructure.Data.Contexts;
using Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Domain.Interfaces;
using Infrastructure.Events;

namespace Application.DI {

    public class Initializer 
    {
        public static void Configure (IServiceCollection services, string connection) 
        {
            services.AddDbContext<SqlDbContext> (options => options.UseSqlServer (connection));
            services.AddScoped<IDomainEventHandler, DomainEventHandler>();
            services.AddScoped (typeof (IRepository<UserEntity>), typeof (UserRepository));
            services.AddScoped (typeof (IRepository<>), typeof (Repository<>));
            services.AddScoped (typeof (UserService));
            services.AddScoped (typeof (IUnitOfWork), typeof (UnitOfWork));
        }
    }
}  