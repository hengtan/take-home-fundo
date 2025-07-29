using Fundo.Application.Commands.Loans.Create;
using Fundo.Application.Commands.Loans.RegisterPayment;
using Fundo.Application.DTOs;
using Fundo.Application.Queries.Loan.GetById;
using Fundo.Application.Queries.Loan.List;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fundo.API.Controllers.Loans;

[ApiController]
[Authorize]
[Route("loans")]
public class LoansController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create(CreateLoanCommand command) =>
        Ok(await mediator.Send(command));

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<LoanDetailsDto>> GetById(Guid id) =>
        Ok(await mediator.Send(new GetLoanByIdQuery(id)));

    [HttpGet]
    public async Task<ActionResult<List<LoanListItemDto>>> GetAll() =>
        Ok(await mediator.Send(new GetAllLoansQuery()));

    [HttpPost("{id:guid}/payment")]
    public async Task<IActionResult> RegisterPayment(Guid id, RegisterPaymentCommand command)
    {
        if (id != command.LoanId) return BadRequest("Loan ID mismatch.");
        await mediator.Send(command);
        return NoContent();
    }
}