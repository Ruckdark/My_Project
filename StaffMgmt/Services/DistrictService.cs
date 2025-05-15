using Microsoft.EntityFrameworkCore;
using StaffMgmt.Data;
using StaffMgmt.Interfaces;
using StaffMgmt.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StaffMgmt.Services
{
    public class DistrictService : IDistrictService
    {
        private readonly StaffMgmtDbContext _context;

        public DistrictService(StaffMgmtDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<District>> GetDistrictsByProvinceIdAsync(int provinceId)
        {
            // Lấy các huyện có ProvinceId khớp và chỉ chọn Id, Name để tối ưu
            return await _context.Districts
                                 .Where(d => d.ProvinceId == provinceId)
                                 .OrderBy(d => d.Name) // Sắp xếp theo tên cho dễ nhìn
                                                       // .Select(d => new District { Id = d.Id, Name = d.Name }) // Chỉ lấy Id và Name nếu muốn tối ưu hơn nữa
                                 .ToListAsync();
        }
        
        public async Task<District?> GetDistrictByIdAsync(int id)
        {
            // FindAsync tối ưu cho tìm theo khóa chính
            return await _context.Districts.FindAsync(id);
            // Hoặc dùng FirstOrDefaultAsync nếu cần Include bảng khác sau này:
            // return await _context.Districts.FirstOrDefaultAsync(d => d.Id == id);
        }

    }
}