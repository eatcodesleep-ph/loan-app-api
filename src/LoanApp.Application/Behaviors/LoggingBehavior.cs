using LoanApp.Application.Abstractions.Handlers;
using LoanApp.Domain.Common;
using Microsoft.Extensions.Logging;

namespace LoanApp.Application.Behaviors;

public static class LoggingBehavior
{
    public sealed class CommandHandler<TCommand, TResult>(
        ICommandHandler<TCommand, TResult> innerHandler,
        ILogger<CommandHandler<TCommand, TResult>> logger)
        : ICommandHandler<TCommand, TResult>
        where TCommand : ICommand<TResult>
    {
        public async Task<Result<TResult>> Handle(TCommand command, CancellationToken cancellationToken)
        {
            string commandName = typeof(TCommand).Name;

            logger.LogInformation("Processing command {Command}", commandName);

            var result = await innerHandler.Handle(command, cancellationToken);

            if (result.IsSuccess)
            {
                logger.LogInformation("Completed command {Command}", commandName);
            }
            else
            {
                logger.LogError("Completed command {Command} with error", commandName);
            }

            return result;
        }
    }

    public sealed class QueryHandler<TQuery, TResult>(
        IQueryHandler<TQuery, TResult> innerHandler,
        ILogger<QueryHandler<TQuery, TResult>> logger)
        : IQueryHandler<TQuery, TResult>
        where TQuery : IQuery<TResult>
    {
        public async Task<Result<TResult>> Handle(TQuery query, CancellationToken cancellationToken)
        {
            string queryName = typeof(TQuery).Name;

            logger.LogInformation("Processing query {Query}", queryName);

            var result = await innerHandler.Handle(query, cancellationToken);

            if (result.IsSuccess)
            {
                logger.LogInformation("Completed query {Query}", queryName);
            }
            else
            {
                logger.LogError("Completed query {Query} with error", queryName);
            }

            return result;
        }
    }
}