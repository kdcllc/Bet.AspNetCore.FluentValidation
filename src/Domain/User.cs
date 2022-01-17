namespace Domain;

public class User : DomainEntity
{
    public string Username { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public DateTime CreatedOn { get; set; }

    // navigational properties
    public ICollection<TodoItem>? Todos { get; set; }
}
