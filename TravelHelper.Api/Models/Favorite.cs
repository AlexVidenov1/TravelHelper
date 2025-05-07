namespace TravelHelper.Api.Models;
public class Favorite
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string City { get; set; } = "";
    public string? Note { get; set; }
    public DateTime Created { get; set; } = DateTime.UtcNow;
}