namespace WebProject.Domain;

public class User
{
    public int UserID { get; set; }
    public required string Email { get; set; }
    public required string Username { get; set; }
    public required string Password { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}
