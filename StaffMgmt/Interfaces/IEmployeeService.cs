using StaffMgmt.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StaffMgmt.Interfaces
{
    public interface IEmployeeService
    {
        Task<Employee> AddEmployeeAsync(Employee employee);
        Task<IEnumerable<Employee>> GetAllEmployeesAsync();
        
        Task<bool> CheckCccdExistsAsync(string cccd, int? currentEmployeeId = null); // currentEmployeeId để loại trừ khi Edit
        Task<bool> CheckPhoneNumberExistsAsync(string phoneNumber, int? currentEmployeeId = null);
        
        Task<Employee?> GetEmployeeByIdAsync(int id); // Lấy Employee theo Id (có thể null)
        Task UpdateEmployeeAsync(Employee employee);   // Cập nhật Employee
        Task DeleteEmployeeAsync(int id);            // Xóa Employee theo Id
    }
}