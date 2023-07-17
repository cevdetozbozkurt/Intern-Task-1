using Task_1.Models;

namespace Task_1.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<Customer>> GetAllUsers();
        Task<Customer> GetUserById(string userId);
        bool Add(Customer user);
        bool Update(Customer user);
        bool Delete(Customer user);
        bool Save();
    }
}
