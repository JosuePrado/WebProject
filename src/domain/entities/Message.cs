namespace WebProject.Domain.Entities;

public class Message
{
    public int MessageID { get; set; }
    public int ChannelID { get; set; }
    public int UserID { get; set; }
    public required string Content { get; set; }
    public DateTime SentAt { get; set; } = DateTime.Now;
}
