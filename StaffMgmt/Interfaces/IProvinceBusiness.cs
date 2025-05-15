// File: Interfaces/IProvinceBusiness.cs
using StaffMgmt.Models; // Hoặc DTOs/ViewModels sau này
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StaffMgmt.Interfaces
{
    // Ban đầu, Business Layer có thể có các phương thức tương tự Service
    // Sau này sẽ thêm logic, validation, mapping vào đây
    public interface IProvinceBusiness
    {
        Task<IEnumerable<Province>> GetAllProvincesAsync();
        Task<Province?> GetProvinceByIdAsync(int id);
        Task AddProvinceAsync(Province province); // Nên nhận ViewModel/DTO ở đây sau này
        Task UpdateProvinceAsync(Province province); // Nên nhận ViewModel/DTO ở đây sau này
        Task DeleteProvinceAsync(int id);
        // Có thể thêm các phương thức nghiệp vụ khác ở đây
    }
}