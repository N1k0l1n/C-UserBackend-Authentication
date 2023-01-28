using UserBackend.Data;
using UserBackend.Models;

namespace UserBackend.Repositories
{
    public class Userrepository : IUserRepository
    {
        private readonly UserDbContext _context;
        public Userrepository(UserDbContext context)
        {
            _context = context;
        }
        public User Create(User user)
        {
            _context.Users.Add(user);
            //will return the Id the user it was created
            user.Id = _context.SaveChanges();
            return user;
        }

        public User GetByEmail(string email)
        {
          return _context.Users.FirstOrDefault(u=>u.Email == email); 
        }

        public User GetById(int id)
        {
            return _context.Users.FirstOrDefault(u => u.Id == id);
        }
    }
}
