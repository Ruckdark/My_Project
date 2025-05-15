// File: Interfaces/IDataSeedingService.cs

using System.Security.Cryptography;
using System.Threading.Tasks; // Cần thiết cho kiểu trả về Task

namespace StaffMgmt.Interfaces
{
    /// <summary>
    /// Interface định nghĩa các chức năng cho việc nhập dữ liệu ban đầu (seeding).
    /// </summary>
    public interface IDataSeedingService // Sửa 'class' thành 'interface'
    {
        /// <summary>
        /// Thực hiện nhập liệu (seed) cho tất cả các đơn vị hành chính (Tỉnh, Huyện, Xã)
        /// từ một file JSON có cấu trúc lồng nhau.
        /// </summary>
        /// <param name="jsonFilePath">Đường dẫn đến file JSON chứa dữ liệu.</param>
        /// <returns>Một Task đại diện cho hoạt động bất đồng bộ.</returns>
        Task SeedAllAdministrativeUnitsAsync(string jsonFilePath);

        // Có thể thêm các phương thức seeding khác ở đây nếu cần
        // ví dụ: Task SeedEthnicitiesAsync(string filePath);
        // ví dụ: Task SeedOccupationsAsync(string filePath);
    }
}
