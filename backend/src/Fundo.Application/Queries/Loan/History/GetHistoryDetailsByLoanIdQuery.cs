using MediatR;

namespace Fundo.Application.Queries.Loan.History;

public record GetHistoryDetailsByLoanIdQuery(Guid Id) : IRequest<List<HistoryDetailsDto>>;