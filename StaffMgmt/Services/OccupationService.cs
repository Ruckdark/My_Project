using Microsoft.EntityFrameworkCore;
using StaffMgmt.Data;
using StaffMgmt.Interfaces;
using StaffMgmt.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StaffMgmt.Services
{
    public class OccupationService : IOccupationService
    {
        private readonly StaffMgmtDbContext _context;
        public OccupationService(StaffMgmtDbContext context) { _context = context; }

        public async Task<IEnumerable<Occupation>> GetAllOccupationsAsync()
        {
            return await _context.Occupations.OrderBy(o => o.Name).ToListAsync();
        }
    }
}