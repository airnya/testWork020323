using backend.Communication.Repos;
using backend.DataLayer;
using backend.Model;
using Microsoft.EntityFrameworkCore;

namespace backend.Repos;

public class UserRepo: DbContext, IUserRepo 
{
    private readonly ApplicationContext _appContext;
    public UserRepo(ApplicationContext appContext)
    {
        _appContext = appContext;
    }
    
    public User? GetUser(string name)
        => _appContext.Set<User>().FirstOrDefault(u => u.Name == name);
}