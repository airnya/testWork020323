using backend.Communication.Repos;
using backend.DataLayer;
using backend.Model;
using Microsoft.EntityFrameworkCore;

namespace backend.Repos;

public class ScoreRepo : IScoreRepo
{
    private readonly ApplicationContext _appContext;
    public ScoreRepo(ApplicationContext appContext)
    {
        _appContext = appContext;
    }
    
    public IEnumerable<Score> GetScores()
        => _appContext.Set<Score>()
            .Include(s => s.User)
            .OrderByDescending(s => s.MaxScore) ;

    public IEnumerable<Score> GetScoresByFilter(string filter, int count = 100)
        => string.IsNullOrEmpty(filter) 
            ? GetScores().Take(count)
            : _appContext.Set<Score>()
                .Include(s => s.User)
                .Where(s => EF.Functions.Like(s.User.Name, $"%{filter}%"))
                .OrderByDescending(s => s.MaxScore).Take(count);

    public void AddScore(int userId, int maxScore)
    {
        var score = new Score() { UserId = userId, MaxScore = maxScore };
        var db = new ApplicationContext();
        
        db.Scores.Add(score);
        db.SaveChanges();
    }

    public void RemoveAllScore(int userId)
    {
        using (var db = new ApplicationContext())
        {
            var userScores = db.Set<Score>().Where(s => s.UserId == userId).ToArray();
            if (userScores == null) return;
            
            db.Scores.RemoveRange(userScores);
            db.SaveChanges();
        }
    }
}