using System.Linq;
using CleanArch.Domain.Interfaces;
using CleanArch.Domain.Models;
using CleanArch.Infra.Data.Context;

namespace CleanArch.Infra.Data.Repository
{
    public class UserRepository: IUserRepository
    {
        private UniversityDbContext _context;

        public UserRepository(UniversityDbContext context)
        {
            _context = context;
        }

        public bool IsExistUser(string email, string password)
        {
            return _context.Users.Any(current => current.Email == email && current.Password == password);
        }

        public void AddUser(User user)
        {
            _context.Add(user);
        }

        public bool IsExistUserName(string userName)
        {
            return _context.Users.Any(content => content.UserName == userName);
        }

        public bool IsExistEmail(string email)
        {
            return _context.Users.Any(content => content.Email == email);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
