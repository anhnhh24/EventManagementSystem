using EventController.Models.Data.DBcontext;
using EventController.Models.Entity;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace EventController.Models.DAO.Implements
{
    public class UserDAO
    {
        private readonly DBContext _context;

        public UserDAO(DBContext context)
        {
            _context = context;
        }

        public List<User> GetAllUsers()
        {
            return _context.Users
                           .Include(u => u.Role)
                           .OrderByDescending(u => u.DateJoined)
                           .ToList();
        }

        public User GetUserById(int id)
        {
            return _context.Users
                           .Include(u => u.Role)
                           .FirstOrDefault(u => u.UserID == id);
        }

        public User GetUserByEmail(string email)
        {
            return _context.Users
                           .Include(u => u.Role)
                           .FirstOrDefault(u => u.Email == email);
        }

        public void AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public void UpdateUser(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
        }

        public void DeleteUser(int id)
        {
            var user = _context.Users.Find(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
        }

        public bool UserExists(int id) => _context.Users.Any(u => u.UserID == id);
        public bool UserExistsByEmail(string e) => _context.Users.Any(u => u.Email == e);

        public List<User> SearchUsersByName(string keyword)
        {
            return _context.Users
                           .Where(u => u.FullName.Contains(keyword))
                           .ToList();
        }

        public List<User> GetUsersByRole(int roleId)
        {
            return _context.Users
                           .Where(u => u.RoleID == roleId)
                           .ToList();
        }

        public bool VerifyPassword(User user, string plainPassword)
        {
            var existingUser = _context.Users.FirstOrDefault(u => u.Email == user.Email);
            if (user == null) return false;
            return PasswordHelper.VerifyPassword(user.Password, plainPassword);
        }
    }
}
