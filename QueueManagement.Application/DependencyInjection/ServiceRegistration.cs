using Microsoft.Extensions.DependencyInjection;
using QueueManagement.Application.Interfaces.Services;
using QueueManagement.Application.Services.Category;
using System.Reflection;
using QueueManagement.Application.Services.SubCategory;
using QueueManagement.Application.Services.Token;
using QueueManagement.Domain.Entities;
using QueueManagement.Application.Services.Counter;
using QueueManagement.Application.Services.Dashboard;
using QueueManagement.Application.Services.User;
using QueueManagement.Application.Interfaces;

namespace QueueManagement.Application.DependencyInjection;

public static class ServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {

        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<ISubCategoryService, SubCategoryService>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<ICounterService, CounterService>();
        services.AddScoped<IDashboardService,DashboardService>();
        services.AddScoped<IUserService, UserService>();
        return services;
    }
}