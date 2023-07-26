using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Interfaces;
using WebApplication1.Models;

namespace WebApplication1.Repository
{
    public class MemberRepository : IMemberRepository
    {
        private readonly ApplicationDbContext _context;
        public MemberRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Add(Member member)
        {
            _context.Members.Add(member);
            return Save();
        }

        public bool Delete(Member member)
        {
            _context.Members.Remove(member);
            return Save();
        }

        public async Task<IEnumerable<Member>> GetAllMembers()
        {
            return await _context.Members.ToListAsync();
        }

        public async Task<Member> GetMemberById(string memberId)
        {
            return await _context.Members.FindAsync(memberId);
        }

        public async Task<Member> GetMemberByEmailAndPassword(string email, string password)
        {
            return await _context.Members.FirstOrDefaultAsync(m => m.Email == email && m.Password == password);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Member member)
        {
            _context.Members.Update(member);
            return Save();
        }
    }
}
