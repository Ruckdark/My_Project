// File: Services/ProvinceService.cs
using Microsoft.EntityFrameworkCore; // Cần cho Include, ToListAsync, etc.
using StaffMgmt.Data;             // Namespace của DbContext
using StaffMgmt.Interfaces;       // Namespace của Interfaces
using StaffMgmt.Models;           // Namespace của Models
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StaffMgmt.Services
{
    public class ProvinceService : IProvinceService
    {
        private readonly StaffMgmtDbContext _context; // Inject DbContext

        public ProvinceService(StaffMgmtDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Province>> GetAllProvincesAsync()
        {
            // ToListAsync() đến từ Microsoft.EntityFrameworkCore
            return await _context.Provinces.ToListAsync();
        }

        public async Task<Province?> GetProvinceByIdAsync(int id)
        {
            // FindAsync tối ưu cho việc tìm theo khóa chính
            return await _context.Provinces.FindAsync(id);
        }

        public async Task AddProvinceAsync(Province province)
        {
            if (province == null)
            {
                // Có thể throw exception cụ thể hơn
                throw new ArgumentNullException(nameof(province));
            }
            _context.Provinces.Add(province);
            await _context.SaveChangesAsync(); // Lưu thay đổi vào DB
        }

        public async Task UpdateProvinceAsync(Province province)
        {
            if (province == null)
            {
                throw new ArgumentNullException(nameof(province));
            }
            // Đánh dấu là đã bị sửa đổi
            _context.Entry(province).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) // Xử lý lỗi nếu record đã bị sửa đổi bởi người khác
            {
                if (!await ProvinceExistsAsync(province.Id))
                {
                    // Hoặc throw lỗi tùy chỉnh
                    throw new KeyNotFoundException($"Province with Id {province.Id} not found.");
                }
                else
                {
                    throw; // Ném lại lỗi concurrency để tầng trên xử lý
                }
            }
        }

        public async Task DeleteProvinceAsync(int id)
        {
            var province = await _context.Provinces.FindAsync(id);
            if (province != null)
            {
                _context.Provinces.Remove(province);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException($"Province with Id {id} not found for deletion.");
            }
            // Lưu ý: Nếu có Huyện tham chiếu đến Tỉnh này và FK là NO ACTION/RESTRICT,
            // SaveChangesAsync sẽ báo lỗi ở đây. Cần xử lý ở tầng Business hoặc Controller.
        }

        public async Task<bool> ProvinceExistsAsync(int id)
        {
            // AnyAsync hiệu quả hơn CountAsync > 0
            return await _context.Provinces.AnyAsync(e => e.Id == id);
        }
    }
}