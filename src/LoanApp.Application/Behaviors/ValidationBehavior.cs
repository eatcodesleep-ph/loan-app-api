using FluentValidation;
using LoanApp.Application.Abstractions.Handlers;
using LoanApp.Domain.Common;

namespace LoanApp.Application.Behaviors;

public static class ValidationBehavior
{
    public sealed class CommandHandler<TCommand, TResult>(
        ICommandHandler<TCommand, TResult> innerHandler,
        IEnumerable<IValidator<TCommand>> validators) : ICommandHandler<TCommand, TResult>
        where TCommand : ICommand<TResult>
    {
        public async Task<Result<TResult>> Handle(TCommand command, CancellationToken ct)
        {
            if (validators.Any())
            {
                var context = new ValidationContext<TCommand>(command);
                var validationTasks = validators.Select(v => v.ValidateAsync(context, ct));
                var results = await Task.WhenAll(validationTasks);

                var failures = results.SelectMany(r => r.Errors).Where(f => f is not null).ToList();
                if (failures.Count > 0)
                {
                    var map = failures.GroupBy(e => e.PropertyName ?? string.Empty)
                        .ToDictionary(g => g.Key, g => g.Select(e => e.ErrorMessage).Where(m => !string.IsNullOrWhiteSpace(m)).Distinct().ToArray());
                    return Result<TResult>.ValidationFailure(map);
                }
            }

            return await innerHandler.Handle(command, ct);
        }
    }

    public sealed class QueryHandler<TQuery, TResult>(
       IQueryHandler<TQuery, TResult> innerHandler,
       IEnumerable<IValidator<TQuery>> validators) : IQueryHandler<TQuery, TResult>
       where TQuery : IQuery<TResult>
    {
        public async Task<Result<TResult>> Handle(TQuery command, CancellationToken ct)
        {
            if (validators.Any())
            {
                var context = new ValidationContext<TQuery>(command);
                var validationTasks = validators.Select(v => v.ValidateAsync(context, ct));
                var results = await Task.WhenAll(validationTasks);

                var failures = results.SelectMany(r => r.Errors).Where(f => f is not null).ToList();
                if (failures.Count > 0)
                {
                    var map = failures.GroupBy(e => e.PropertyName ?? string.Empty)
                        .ToDictionary(g => g.Key, g => g.Select(e => e.ErrorMessage).Where(m => !string.IsNullOrWhiteSpace(m)).Distinct().ToArray());
                    return Result<TResult>.ValidationFailure(map);
                }
            }

            return await innerHandler.Handle(command, ct);
        }
    }
}
