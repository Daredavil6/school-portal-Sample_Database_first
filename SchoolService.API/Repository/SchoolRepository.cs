using SchoolPortal.Common.Models.dboSchema;
using SchoolPortal.Common.Models;
using Microsoft.EntityFrameworkCore;

namespace SchoolService.API.Repository
{
    public class SchoolRepository : ISchoolRepository
    {
        private readonly SchoolPortalContext _context;

        public SchoolRepository(SchoolPortalContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SchoolMaster?>> GetAllAsync()
        {
            return await _context.SchoolMasters.ToListAsync();
        }

        public async Task<SchoolMaster?> GetByIdAsync(Guid id)
        {
            return await _context.SchoolMasters.FindAsync(id);
        }

        public async Task<bool> CreateAsync(SchoolMaster school)
        {
            await _context.SchoolMasters.AddAsync(school);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(Guid id, SchoolMaster school)
        {
            var existingSchool = await _context.SchoolMasters.FindAsync(id);
            if (existingSchool == null) return false;

            _context.Entry(existingSchool).CurrentValues.SetValues(school);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var school = await _context.SchoolMasters.FindAsync(id);
            if (school == null) return false;

            _context.SchoolMasters.Remove(school);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
