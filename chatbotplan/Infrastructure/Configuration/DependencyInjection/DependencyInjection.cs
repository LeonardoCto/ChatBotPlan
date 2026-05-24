
using ChatBotPlan.Application;
using ChatBotPlan.Domain.Interfaces;
using ChatBotPlan.Domain;
using ChatBotPlan.Infrastructure.Repositories;
using ChatBotPlan.Infrastructure.Security;
using Microsoft.EntityFrameworkCore;
using ChatBotPlan.Application.Interfaces;
using FluentValidation;

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

        services.AddScoped<ITokenService, TokenService>();
        services.Configure<TokenSettings>(configuration.GetSection("JWT"));

        services.Configure<AzureEmailSettings>(configuration.GetSection("AzureEmail"));
        services.AddScoped<IEmailService, AzureEmailAdapter>();

        services.AddScoped<IUserVerificationCodeRepository, UserVerificationCodeRepository>();

        return services;
    }

    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<CreateUsersUseCase>();
        services.AddScoped<GetByIdUserUseCase>();
        services.AddScoped<UpdateUserUseCase>();
        services.AddScoped<DeleteUserUseCase>();
        services.AddScoped<AuthUserUseCase>();
        services.AddScoped<UpdateEmailUseCase>();
        services.AddScoped<IUserValidator, UserValidator>();
        services.AddAutoMapper(typeof(UserProfile));
        services.AddValidatorsFromAssemblyContaining<CreateUserValidator>();
        return services;
    }
}