// File: Interfaces/IProvinceService.cs
using StaffMgmt.Models;
using System.Collections.Generic;
using System.Threading.Tasks; // Sử dụng Task cho các phương thức bất đồng bộ

namespace StaffMgmt.Interfaces
{
    public interface IProvinceService
    {
        Task<IEnumerable<Province>> GetAllProvincesAsync(); // Lấy tất cả tỉnh
        Task<Province?> GetProvinceByIdAsync(int id);       // Lấy tỉnh theo ID (có thể null)
        Task AddProvinceAsync(Province province);           // Thêm tỉnh mới
        Task UpdateProvinceAsync(Province province);        // Cập nhật tỉnh
        Task DeleteProvinceAsync(int id);                 // Xóa tỉnh theo ID
        Task<bool> ProvinceExistsAsync(int id);             // Kiểm tra tỉnh tồn tại
    }
}