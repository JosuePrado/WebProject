namespace WebProject.Domain.Entities;

public class ChannelMember
{
    public int ChannelID { get; set; }
    public int UserID { get; set; }
    public DateTime JoinedAt { get; set; } = DateTime.Now;
}