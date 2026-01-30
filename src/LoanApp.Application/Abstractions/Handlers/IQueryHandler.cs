using LoanApp.Domain.Common;

namespace LoanApp.Application.Abstractions.Handlers;

public interface IQueryHandler<in TQuery, TResult>
    where TQuery : IQuery<TResult>
{
    Task<Result<TResult>> Handle(TQuery query, CancellationToken cancellationToken);
}