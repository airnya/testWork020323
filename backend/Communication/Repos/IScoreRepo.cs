using backend.Model;

namespace backend.Communication.Repos;

public interface IScoreRepo
{
    IEnumerable<Score> GetScores();
    IEnumerable<Score> GetScoresByFilter(string filter, int count = 100);

    void AddScore(int userId, int maxScore);
    void RemoveAllScore(int userId);
}