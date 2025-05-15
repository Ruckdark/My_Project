// File: Services/EthnicityService.cs
using Microsoft.EntityFrameworkCore;
using StaffMgmt.Data;
using StaffMgmt.Interfaces;
using StaffMgmt.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StaffMgmt.Services
{
    public class EthnicityService : IEthnicityService
    {
        private readonly StaffMgmtDbContext _context;
        public EthnicityService(StaffMgmtDbContext context) { _context = context; }

        public async Task<IEnumerable<Ethnicity>> GetAllEthnicitiesAsync()
        {
            // Lấy và sắp xếp theo tên
            return await _context.Ethnicities.OrderBy(e => e.Name).ToListAsync();
        }
    }
}