
using ChatBotPlan.Application;
using ChatBotPlan.Domain.Interfaces;
using ChatBotPlan.Infrastructure.Repositories;
using ChatBotPlan.Infrastructure.Security;
using Microsoft.EntityFrameworkCore;
using ChatBotPlan.Application.Interfaces;
using FluentValidation;
using Microsoft.Extensions.AI;
using OllamaSharp;
using Microsoft.Extensions.Options;

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

        services.AddScoped<ILLMService, OllamaAdapter>();
        services.Configure<OllamaSettings>(configuration.GetSection("Ollama"));

        services.AddSingleton<IChatClient>(sp =>
        {
            var settings = sp.GetRequiredService<IOptions<OllamaSettings>>().Value;
            return new OllamaChatClient(settings.Url, settings.Model);
        });

        services.AddScoped<IChatMemory, RedisChatMemory>();

        services.AddHttpContextAccessor();
        services.AddScoped<IUserContext, UserContext>();

        return services;
    }

    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<CreateUsersUseCase>();
        services.AddScoped<SendMessageUseCase>();
        services.AddScoped<GetByIdUserUseCase>();
        services.AddScoped<UpdateUserUseCase>();
        services.AddScoped<DeleteUserUseCase>();
        services.AddScoped<UpdatePasswordUseCase>();
        services.AddScoped<AuthUserUseCase>();
        services.AddScoped<UpdateEmailUseCase>();
        services.AddScoped<IUserValidator, UserValidator>();
        services.AddAutoMapper(typeof(UserProfile));
        services.AddValidatorsFromAssemblyContaining<CreateUserValidator>();
        return services;
    }
}

