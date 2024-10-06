namespace WebProject.Domain;

public class Message
{
    public int MessageId { get; set; }
    public int ChannelId { get; set; }
    public int UserId { get; set; }
    public required string Content { get; set; }
    public DateTime SentAt { get; set; }
}