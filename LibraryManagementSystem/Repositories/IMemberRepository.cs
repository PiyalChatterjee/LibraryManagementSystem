
using LMS.API.Models.Domain;

namespace LMS.API.Repositories
{
    public interface IMemberRepository
    {
        Task<Member> CreateAsync(Member member);
        Task<Member?> GetByIdAsync(Guid id);
        Task<List<Member>> GetAllAsync();
        Task<Member?> UpdateMemberAsync(Guid id, Member member);
        Task<bool> DeleteMemberAsync(Guid id);
    }
}
