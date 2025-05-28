using Microsoft.EntityFrameworkCore;
using SchoolPortal.Common.Models;
using SchoolPortal.Common.Models.dboSchema;

namespace CountryService.API.Repository
{
	public class CountryRepository : ICountryRepository
	{
		private readonly SchoolPortalContext _context;

		public CountryRepository(SchoolPortalContext context)
		{
			_context = context;
		}

		public async Task<IEnumerable<CountryMaster?>> GetAllAsync()
		{
			return await _context.Set<CountryMaster>()
				.Where(c => !c.IsDeleted && c.IsActive)
				.ToListAsync();
		}

		public async Task<CountryMaster?> GetByIdAsync(Guid id)
		{
			return await _context.Set<CountryMaster>()
				.Where(c => !c.IsDeleted && c.IsActive)
				.FirstOrDefaultAsync(c => c.Id == id);
		}

		public async Task<bool> CreateAsync(CountryMaster Country)
		{
			await _context.Set<CountryMaster>().AddAsync(Country);
			return await _context.SaveChangesAsync() > 0;
		}

		public async Task<bool> UpdateAsync(Guid id, CountryMaster? Country)
		{
			var CountryEntity = await _context.Set<CountryMaster>().FindAsync(id);
			if (CountryEntity == null) return false;

			_context.Entry(CountryEntity).CurrentValues.SetValues(Country);
			return await _context.SaveChangesAsync() > 0;
		}

		public async Task<bool> DeleteAsync(Guid id)
		{
			var Country = await _context.Set<CountryMaster>().FindAsync(id);
			if (Country == null) return false;

			_context.Set<CountryMaster>().Remove(Country);
			return await _context.SaveChangesAsync() > 0;
		}
	}
}
