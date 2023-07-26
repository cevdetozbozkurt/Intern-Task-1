using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Interfaces;
using WebApplication1.Models;

namespace WebApplication1.Repository
{
	public class UserRepository : IUserRepository
	{

		private readonly ApplicationDbContext _context;
		public UserRepository(ApplicationDbContext context)
        {
			_context = context;
		}

        public bool Add(Member user)
		{
			throw new NotImplementedException();
		}

		public bool Delete(Member user)
		{
			_context.Remove(user);
			return Save();
		}

		public async Task<IEnumerable<Member>> GetAllUsers()
		{
			return await _context.Users.ToListAsync();
		}

		public async Task<Member> GetUserById(string userId)
		{
			return await _context.Users.FindAsync(userId);
		}

		public bool Save()
		{
			var saved = _context.SaveChanges();
			return saved > 0 ? true : false;
		}

		public bool Update(Member user)
		{
			_context.Update(user);
			return Save();
		}
	}
}
