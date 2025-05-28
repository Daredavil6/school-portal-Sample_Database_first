using SchoolPortal.Common.Models.dboSchema;
using SchoolPortal.Common.Models;
using Microsoft.EntityFrameworkCore;

namespace StateService.API.Repository
{
    public class StateRepository : IStateRepository
    {
        private readonly SchoolPortalContext _context;

        public StateRepository(SchoolPortalContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<StateMaster?>> GetAllAsync()
        {
            return await _context.Set<StateMaster>()
                .Include(s => s.Country)
                .Where(s => !s.IsDeleted && s.IsActive && s.Country != null && !s.Country.IsDeleted && s.Country.IsActive)
                .ToListAsync();
        }

        public async Task<StateMaster?> GetByIdAsync(Guid id)
        {
            return await _context.Set<StateMaster>()
                .Include(s => s.Country)
                .Where(s => !s.IsDeleted && s.IsActive && s.Country != null && !s.Country.IsDeleted && s.Country.IsActive)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<bool> CreateAsync(StateMaster state)
        {
            await _context.Set<StateMaster>().AddAsync(state);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(Guid id, StateMaster? state)
        {
            var stateEntity = await _context.Set<StateMaster>().FindAsync(id);
            if (stateEntity == null) return false;

            _context.Entry(stateEntity).CurrentValues.SetValues(state);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var State = await _context.Set<StateMaster>().FindAsync(id);
            if (State == null) return false;

            _context.Set<StateMaster>().Remove(State);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
