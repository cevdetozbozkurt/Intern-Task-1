using WebApplication1.Models;

namespace WebApplication1.Interfaces
{
    public interface IMemberRepository
    {
        bool Add(Member member);
        bool Delete(Member member);
        Task<IEnumerable<Member>> GetAllMembers();
        Task<Member> GetMemberById(string memberId);
        Task<Member> GetMemberByEmailAndPassword(string email, string password);
        bool Save();
        bool Update(Member member);
    }
}
