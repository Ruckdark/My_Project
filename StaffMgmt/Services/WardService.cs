using Microsoft.EntityFrameworkCore;
using StaffMgmt.Data;
using StaffMgmt.Interfaces;
using StaffMgmt.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StaffMgmt.Services
{
    public class WardService : IWardService
    {
        private readonly StaffMgmtDbContext _context;

        public WardService(StaffMgmtDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Ward>> GetWardsByDistrictIdAsync(int districtId)
        {
            return await _context.Wards
                                 .Where(w => w.DistrictId == districtId)
                                 .OrderBy(w => w.Name)
                                 // .Select(w => new Ward { Id = w.Id, Name = w.Name })
                                 .ToListAsync();
        }
        public async Task<Ward?> GetWardByIdAsync(int id)
        {
            return await _context.Wards.FindAsync(id);
            // return await _context.Wards.FirstOrDefaultAsync(w => w.Id == id);
        }
    }
}