namespace Fundo.Application.DTOs;

public class LoanListItemDto
{
    public Guid Id { get; init; }
    public string ApplicantName { get; init; } = string.Empty;
    public decimal CurrentBalance { get; init; }
    public string Status { get; init; } = string.Empty;
}