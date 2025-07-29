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
    [HttpPost]
    public async Task<IActionResult> Create(CreateLoanCommand command)
    {
        var created = await mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = created }, created);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<LoanDetailsDto>> GetById(Guid id)
    {
        var loan = await mediator.Send(new GetLoanByIdQuery(id));

        if (loan is null)
            return NotFound(new { error = $"Loan with ID {id} was not found." });

        return Ok(loan);
    }

    [HttpGet]
    public async Task<ActionResult<List<LoanListItemDto>>> GetAll()
    {
        var loans = await mediator.Send(new GetAllLoansQuery());
        return loans.Count != 0 ? Ok(loans) : NoContent();
    }

    [HttpPost("{id:guid}/payment")]
    public async Task<IActionResult> RegisterPayment(Guid id, RegisterPaymentCommand command)
    {
        if (id != command.LoanId)
            return BadRequest(new { error = "Loan ID mismatch." });

        await mediator.Send(command);
        return NoContent();
    }
}