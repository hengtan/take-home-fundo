using Fundo.Application.DTOs;
using MediatR;

namespace Fundo.Application.Commands.Loans.GetById;

public record GetLoanByIdQuery(Guid Id) : IRequest<LoanDetailsDto>;