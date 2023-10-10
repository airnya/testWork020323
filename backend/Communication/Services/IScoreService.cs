using backend.Dtos;
using backend.Model;

namespace backend.Communication.Services;

public interface IScoreService
{
    IEnumerable<Score> GetScores(string nameFilter = null);
    void AddScore(string userName, int score);
    void DeleteAllScores(string userName);
}