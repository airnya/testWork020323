using System.Data;
using backend.Communication.Repos;
using backend.Communication.Services;
using backend.Dtos;
using backend.Model;

namespace backend.Services;

public class ScoreService : IScoreService
{
    private readonly IScoreRepo _scoreRepo;
    private readonly IUserRepo _userRepo;

    public ScoreService(IScoreRepo scoreRepo, IUserRepo userRepo)
    {
        _scoreRepo = scoreRepo;
        _userRepo = userRepo;
    }

    public void AddScore(string userName, int score)
    {
        var user = GetUser(userName);
        if (user == null) throw new DataException("user not found");
        
        _scoreRepo.AddScore(user.Id, score);
    }
    
    public void DeleteAllScores(string userName)
    {
        var user = GetUser(userName);
        if (user == null) throw new DataException("user not found");
        
        _scoreRepo.RemoveAllScore(user.Id);
    }
    
    public IEnumerable<Score> GetScores(string nameFilter = null)
        => _scoreRepo.GetScoresByFilter(nameFilter);

    private User? GetUser(string name)
        => _userRepo.GetUser(name);
}