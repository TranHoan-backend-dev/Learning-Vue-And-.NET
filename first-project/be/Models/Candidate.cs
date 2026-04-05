namespace hehe.Models;

public class Candidate(
    string name,
    string phone,
    string email,
    string hiringCampaign,
    string hiringPosition,
    string hiringPost,
    string hiringRound,
    string review,
    DateOnly hiringAt,
    bool status)
{
    private Guid Id { get; set; } = Guid.NewGuid();
    private string Name { get; set; } = name;
    private string Phone { get; set; } = phone;
    private string Email { get; set; } = email;
    private string HiringCampaign { get; set; } = hiringCampaign;
    private string HiringPosition { get; set; } = hiringPosition;
    private string HiringPost { get; set; } = hiringPost;
    private string HiringRound { get; set; } = hiringRound;
    private string Review { get; set; } = review;
    private DateOnly HiringAt { get; set; } = hiringAt;
    private bool Status { get; set; } = status;
}