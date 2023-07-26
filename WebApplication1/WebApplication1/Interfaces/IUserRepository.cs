using WebApplication1.Models;

namespace WebApplication1.Interfaces
{
	public interface IUserRepository
	{
		Task<IEnumerable<Member>> GetAllUsers();
		Task<Member> GetUserById(string userId);
		bool Add(Member user);
		bool Update(Member user);
		bool Delete(Member user);
		bool Save();
	}
}
