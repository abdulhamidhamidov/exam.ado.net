using shoping.models;

namespace shoping.Service;

public interface IUserAddressService
{
    List<UserAddress> getUsersAddress();
    UserAddress GetUserAddressById(int id);
    bool CreateUserAddress(UserAddress userAddress);
    bool UpdateUserAddress(UserAddress userAddress);
    bool DeleteUserAddress(int id);
}