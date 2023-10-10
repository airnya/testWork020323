using backend.Communication.Repos;
using backend.Communication.Services;
using backend.Repos;
using backend.Services;
using Unity;

namespace backend.Configuration;

public class Bootstrapper
{
    public void ConfigureServices(IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<IScoreService, ScoreService>();
        serviceCollection.AddTransient<IScoreRepo, ScoreRepo>();
        serviceCollection.AddTransient<IUserRepo, UserRepo>();
    }
}