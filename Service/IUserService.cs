using shoping.models;

namespace shoping.Service;

public interface IUserService
{
    List<User> GetUsers();
    User GetUserById(int id);
    bool CreateUser(User user);
    bool UpdateUser(User user);
    bool DeleteUser(int id);
}