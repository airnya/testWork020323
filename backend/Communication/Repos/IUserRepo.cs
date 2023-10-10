using backend.Model;

namespace backend.Communication.Repos;

public interface IUserRepo
{
    User? GetUser(string name);
}