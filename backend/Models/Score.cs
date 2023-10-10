using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Model;

public class Score
{
    public int Id { get; set; }
    public int MaxScore { get; set; }
    public int UserId { get; set; }
    [ForeignKey(nameof(UserId))]
    public User? User { get; set; }
}