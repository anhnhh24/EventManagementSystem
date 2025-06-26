using EventController.Models.Entity;
using System.Collections.Generic;

namespace EventController.Models.DAO.Interfaces
{
    public interface IUserDAO
    {
        public List<User> GetAllUsers();
        public User GetUserById(int id);
        public User GetUserByEmail(string email);
        public void AddUser(User user);
        public void UpdateUser(User user);
        public void DeleteUser(int id);
        public bool UserExists(int id);
        public bool UserExistsByEmail(string email);
        public List<User> SearchUsersByName(string keyword);
        public List<User> GetUsersByRole(int roleId);
        public bool VerifyPassword(User user, string plainPassword);
    }
}
