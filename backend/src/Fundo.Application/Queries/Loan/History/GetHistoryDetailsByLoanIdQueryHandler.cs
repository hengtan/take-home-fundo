using Fundo.Application.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Fundo.Application.Queries.Loan.History;

public class GetHistoryDetailsByLoanIdQueryHandler(
    IHistoryRepository historyRepository,
    ILogger<GetHistoryDetailsByLoanIdQueryHandler> logger)
    : IRequestHandler<GetHistoryDetailsByLoanIdQuery, List<HistoryDetailsDto>>
{
    public async Task<List<HistoryDetailsDto>> Handle(GetHistoryDetailsByLoanIdQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Retrieving history with Loan ID: {LoanId}", request.Id);

        var historyList = await historyRepository.GetByLoanIdAsync(request.Id, cancellationToken);

        if (historyList.Count != 0)
            return historyList.Select(h => new HistoryDetailsDto
            {
                Description = h.Description
            }).ToList();
        logger.LogWarning("No history found for Loan ID: {LoanId}", request.Id);
        return [];
    }
}