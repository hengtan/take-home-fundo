namespace Fundo.Domain.Entities;

public class History
{
    public Guid Id { get; set; }
    public Guid LoandId { get; set; }
    public string Description { get; set; }
    public DateTime Created { get; set; }

    protected History() {}

    public History(Guid loanId, string description, DateTime created)
    {
        Id = Guid.NewGuid();
        LoandId = loanId;
        Description = description ?? throw new ArgumentNullException(nameof(description), "Description cannot be null.");
        Created = created;
    }
}