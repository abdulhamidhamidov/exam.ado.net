using shoping.models;

namespace shoping.Service;

public interface IUserPaymentService
{
    List<UserPayment> GetUserPayments();
    UserPayment GetUserPaymentById(int id);
    bool CreateUserPayment(UserPayment userPayment);
    bool UpdateUserPayment(UserPayment userPayment);
    bool DeleteUserPayment(int id);
}