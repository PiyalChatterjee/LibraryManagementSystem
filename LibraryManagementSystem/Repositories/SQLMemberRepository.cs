using LMS.API.Data;
using LMS.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace LMS.API.Repositories
{
    public class SQLMemberRepository : IMemberRepository
    {
        private readonly LMSDbContext dbContext;

        public SQLMemberRepository(LMSDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Member> CreateAsync(Member member)
        {
            await dbContext.Members.AddAsync(member);
            await dbContext.SaveChangesAsync();
            return member;
        }

        public async Task<bool> DeleteMemberAsync(Guid id)
        {
            // Using Find method because it is optimized for primary key lookups and
            // it can return the entity from the context cache if it was previously loaded.
            var memberEntity = await dbContext.Members.FindAsync(id);
            // If the member is not found, return false
            if (memberEntity == null)
            {
                return false;
            }
            dbContext.Members.Remove(memberEntity);
            await dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<Member>> GetAllAsync()
        {
            return await dbContext.Members
                .Include(m => m.User)
                .ToListAsync();
        }

        public async Task<Member?> GetByIdAsync(Guid id)
        {
            var member = await dbContext.Members
                .Include(m => m.User)
                .FirstOrDefaultAsync(x => x.MemberId == id);
            if (member == null)
            {
                return null;
            }
            return member;

        }

        public async Task<Member?> UpdateMemberAsync(Guid id, Member member)
        {
            var memberEntity = await dbContext.Members
                .Include(m => m.User)
                .FirstOrDefaultAsync(x => x.MemberId == id);
            if (memberEntity == null)
            {
                return null;
            }
            memberEntity.MembershipExpiryDate = member.MembershipExpiryDate;
            memberEntity.MembershipStartDate = member.MembershipStartDate;
            memberEntity.Address = member.Address;
            memberEntity.Phone = member.Phone;
            memberEntity.Status = member.Status;

            await dbContext.SaveChangesAsync();
            return memberEntity;
        }
    }
}
