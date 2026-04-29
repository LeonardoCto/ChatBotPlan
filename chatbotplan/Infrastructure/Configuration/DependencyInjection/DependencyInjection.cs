
using ChatBotPlan.Application;
using ChatBotPlan.Domain.Interfaces;
using ChatBotPlan.Domain;
using ChatBotPlan.Infrastructure.Repositories;
using ChatBotPlan.Infrastructure.Security;
using Microsoft.EntityFrameworkCore;

namespace ChatBotPlan.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(opts =>
            opts.UseNpgsql(configuration.GetConnectionString("Default")));

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddSingleton<IPasswordHasher, BcryptPasswordHasher>();
        return services;
    }

    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<CreateUsersCases>();
        services.AddScoped<GetByIdUserCase>();
        services.AddScoped<UpdateUserCase>();
        services.AddScoped<DeleteUserCase>();
        services.AddAutoMapper(typeof(UserProfile));
        return services;
    }
}