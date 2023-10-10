using backend.Model;

namespace backend.Dtos;

public class UserScoreDto
{
    public string Name { get; set; }
    public int? Highscore { get; set; }
    
    public UserScoreDto(User user, Score score)
    {
        Name = user?.Name;
        Highscore = score?.MaxScore;
    }

    public static UserScoreDto Convert(Score score)
        => new UserScoreDto(score.User, score);
}

