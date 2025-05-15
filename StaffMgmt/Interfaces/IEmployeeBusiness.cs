// File: Interfaces/IEmployeeBusiness.cs
using StaffMgmt.Models; // Hoặc ViewModels
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StaffMgmt.Interfaces
{
    public interface IEmployeeBusiness
    {
        // Sửa lại tham số thành ViewModel
        Task AddEmployeeAsync(EmployeeFormViewModel viewModel); // <<=== Sửa ở đây
        Task<IEnumerable<Employee>> GetAllEmployeesAsync(); // Giữ nguyên hoặc đổi sang ViewModel sau
        Task<Employee?> GetEmployeeByIdAsync(int id); // Trả về Model (hoặc ViewModel chi tiết sau này)
        Task UpdateEmployeeAsync(EmployeeFormViewModel viewModel); // Nhận ViewModel để validate và update
        Task DeleteEmployeeAsync(int id);
    }
}