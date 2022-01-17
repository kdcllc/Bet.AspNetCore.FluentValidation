namespace Application.TodoCommands;

public class UpdateResult
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public bool IsCompleted { get; set; }

    public int UserId { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime? CompletedAt { get; set; }
}
