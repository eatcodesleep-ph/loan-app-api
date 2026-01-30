using FluentValidation;
using LoanApp.Application.Abstractions.Handlers;
using LoanApp.Application.Behaviors;
using LoanApp.Application.Features.LoanApplications.Commands.Create;
using LoanApp.Application.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LoanApp.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<LoanAppOptions>(configuration.GetSection("LoanAppOptions"));

        var assembly = typeof(CreateLoanApplicationCommandValidator).Assembly;
        services.Scan(scan => scan
            .FromAssemblies(assembly)
            .AddClasses(classes => classes.AssignableTo(typeof(IValidator<>)))
            .AsImplementedInterfaces()
            .WithTransientLifetime());

        services.Scan(scan => scan
            .FromAssemblies(assembly)
            .AddClasses(classes => classes.AssignableTo(typeof(ICommandHandler<,>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        services.Scan(scan => scan
            .FromAssemblies(assembly)
            .AddClasses(classes => classes.AssignableTo(typeof(IQueryHandler<,>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        services.Decorate(typeof(ICommandHandler<,>), typeof(ValidationBehavior.CommandHandler<,>));
        services.Decorate(typeof(IQueryHandler<,>), typeof(ValidationBehavior.QueryHandler<,>));

        services.Decorate(typeof(IQueryHandler<,>), typeof(LoggingBehavior.QueryHandler<,>));
        services.Decorate(typeof(ICommandHandler<,>), typeof(LoggingBehavior.CommandHandler<,>));

        return services;
    }
}
