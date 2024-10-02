namespace WebProject.Domain.Entities;

public class Channel
{
    public int ChannelID { get; set; }
    public required string ChannelName { get; set; }
    public bool IsPrivate { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}