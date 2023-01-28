using UserBackend.Models;

namespace UserBackend.Repositories
{
    public interface IUserRepository
    {
        User Create (User user);
        User GetByEmail(string email);

        User GetById(int id);
    }
}
