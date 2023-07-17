using Microsoft.EntityFrameworkCore;
using Task_1.Data;
using Task_1.Interfaces;
using Task_1.Models;

namespace Task_1.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Add(Customer user)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Customer user)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Customer>> GetAllUsers()
        {
            return await _context.Users.ToListAsync(); ;
        }

        public async Task<Customer> GetUserById(string userId)
        {
            return await _context.Users.FindAsync(userId);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Customer user)
        {
            _context.Update(user);
            return Save();
        }
    }
}
