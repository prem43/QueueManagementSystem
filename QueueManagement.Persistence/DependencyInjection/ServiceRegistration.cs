using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QueueManagement.Application.Interfaces;
using QueueManagement.Domain.Entities;
using QueueManagement.Persistence.Context;
using QueueManagement.Persistence.Repositories;
namespace QueueManagement.Persistence.DependencyInjection;

public static class ServiceRegistration
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection")));
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        return services;
    }
}