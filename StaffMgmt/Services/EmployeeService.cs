// File: Services/EmployeeService.cs
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StaffMgmt.Data;
using StaffMgmt.Interfaces;
using StaffMgmt.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StaffMgmt.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly StaffMgmtDbContext _context;
        private readonly ILogger<EmployeeService> _logger;

        public EmployeeService(StaffMgmtDbContext context, ILogger<EmployeeService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Employee> AddEmployeeAsync(Employee employee)
        {
            if (employee == null)
            {
                throw new ArgumentNullException(nameof(employee));
            }

            try
            {
                _context.Employees.Add(employee);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Đã thêm Employee mới với Id: {EmployeeId}", employee.Id);
                return employee; // Trả về employee đã được gán Id
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Lỗi DbUpdateException khi thêm Employee. Inner: {InnerMessage}", ex.InnerException?.Message);
                // Có thể throw exception cụ thể hơn hoặc xử lý lỗi (ví dụ: lỗi trùng khóa UNIQUE)
                throw; // Ném lại lỗi để tầng Business xử lý
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi không mong muốn khi thêm Employee.");
                throw;
            }
        }

        public async Task<bool> CheckCccdExistsAsync(string cccd, int? currentEmployeeId = null)
        {
            // Sửa lỗi CS0029: Dùng Task.FromResult(false) thay vì return false
            if (string.IsNullOrWhiteSpace(cccd)) return await Task.FromResult(false);

            _logger.LogDebug("Kiểm tra CCCD '{Cccd}' tồn tại, loại trừ Id: {CurrentEmployeeId}", cccd, currentEmployeeId);
            var query = _context.Employees.Where(e => e.IdentityCardNumber == cccd);

            if (currentEmployeeId.HasValue)
            {
                query = query.Where(e => e.Id != currentEmployeeId.Value);
            }

            // Lệnh await AnyAsync() đã trả về Task<bool> nên không cần sửa ở đây
            bool exists = await query.AnyAsync();
            _logger.LogDebug("Kết quả kiểm tra CCCD '{Cccd}': {Exists}", cccd, exists);
            // Sửa lỗi CS0029: Không cần return await Task.FromResult(exists) vì await AnyAsync() đã trả về Task<bool>
            return exists;
        }

        public async Task<bool> CheckPhoneNumberExistsAsync(string phoneNumber, int? currentEmployeeId = null)
        {
            // Sửa lỗi CS0029: Dùng Task.FromResult(false) thay vì return false
            if (string.IsNullOrWhiteSpace(phoneNumber)) return await Task.FromResult(false);

            _logger.LogDebug("Kiểm tra SĐT '{PhoneNumber}' tồn tại, loại trừ Id: {CurrentEmployeeId}", phoneNumber, currentEmployeeId);
            var query = _context.Employees.Where(e => e.PhoneNumber == phoneNumber);

            if (currentEmployeeId.HasValue)
            {
                query = query.Where(e => e.Id != currentEmployeeId.Value);
            }

            // Lệnh await AnyAsync() đã trả về Task<bool>
            bool exists = await query.AnyAsync();
            _logger.LogDebug("Kết quả kiểm tra SĐT '{PhoneNumber}': {Exists}", phoneNumber, exists);
            // Sửa lỗi CS0029: Không cần return await Task.FromResult(exists)
            return exists;
        }

        public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
        {
            _logger.LogInformation("Đang lấy danh sách tất cả Employee...");
            try
            {
                // Include các thông tin liên quan nếu cần hiển thị ở trang Index
                return await _context.Employees
                                     // .Include(e => e.Province) // Ví dụ Include Tỉnh
                                     // .Include(e => e.Certificates) // Ví dụ Include Văn bằng
                                     .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi lấy danh sách Employee.");
                return Enumerable.Empty<Employee>(); // Trả về danh sách rỗng nếu lỗi
            }
        }

        /// <summary>
        /// Lấy thông tin Employee theo Id.
        /// Có thể Include các bảng liên quan nếu cần cho trang Details/Edit.
        /// </summary>
        public async Task<Employee?> GetEmployeeByIdAsync(int id)
        {
            _logger.LogInformation("Đang lấy Employee với Id: {EmployeeId}", id);
            try
            {
                // Dùng FindAsync nếu chỉ cần lấy Employee đơn thuần
                // return await _context.Employees.FindAsync(id);

                // Dùng FirstOrDefaultAsync nếu cần Include các bảng liên quan
                return await _context.Employees
                                     .Include(e => e.Province)  // Ví dụ Include Tỉnh
                                     .Include(e => e.District)  // Ví dụ Include Huyện
                                     .Include(e => e.Ward)      // Ví dụ Include Xã
                                     .Include(e => e.Ethnicity) // Ví dụ Include Dân tộc
                                     .Include(e => e.Occupation)// Ví dụ Include Nghề nghiệp
                                     .FirstOrDefaultAsync(e => e.Id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi lấy Employee với Id: {EmployeeId}", id);
                return null; // Trả về null nếu có lỗi
            }
        }

        /// <summary>
        /// Cập nhật thông tin một Employee hiện có.
        /// </summary>
        public async Task UpdateEmployeeAsync(Employee employee)
        {
            if (employee == null) throw new ArgumentNullException(nameof(employee));
            _logger.LogInformation("Chuẩn bị cập nhật Employee Id: {EmployeeId}", employee.Id);

            // Đánh dấu đối tượng là đã bị sửa đổi để EF Core biết cần Update
            _context.Entry(employee).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                _logger.LogInformation("Đã cập nhật thành công Employee Id: {EmployeeId}", employee.Id);
            }
            catch (DbUpdateConcurrencyException ex) // Bắt lỗi concurrency
            {
                _logger.LogError(ex, "Lỗi DbUpdateConcurrencyException khi cập nhật Employee Id: {EmployeeId}", employee.Id);
                // Ném lại lỗi để Business/Controller xử lý (ví dụ: báo người dùng dữ liệu đã bị thay đổi)
                throw;
            }
            catch (DbUpdateException ex) // Bắt lỗi update khác
            {
                _logger.LogError(ex, "Lỗi DbUpdateException khi cập nhật Employee Id: {EmployeeId}. Inner: {InnerMessage}", employee.Id, ex.InnerException?.Message);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi không mong muốn khi cập nhật Employee Id: {EmployeeId}", employee.Id);
                throw;
            }
        }

        /// <summary>
        /// Xóa một Employee theo Id.
        /// </summary>
        public async Task DeleteEmployeeAsync(int id)
        {
            _logger.LogInformation("Chuẩn bị xóa Employee Id: {EmployeeId}", id);
            try
            {
                // Tìm Employee cần xóa
                var employeeToDelete = await _context.Employees.FindAsync(id);

                if (employeeToDelete != null)
                {
                    _context.Employees.Remove(employeeToDelete);
                    await _context.SaveChangesAsync();
                    _logger.LogInformation("Đã xóa thành công Employee Id: {EmployeeId}", id);
                }
                else
                {
                    _logger.LogWarning("Không tìm thấy Employee Id: {EmployeeId} để xóa.", id);
                    // Có thể throw KeyNotFoundException nếu muốn báo lỗi rõ ràng hơn
                    throw new KeyNotFoundException($"Không tìm thấy Employee với Id {id}.");
                }
            }
            catch (DbUpdateException ex) // Bắt lỗi nếu không xóa được do FK constraint (ví dụ: còn Certificate tham chiếu)
            {
                _logger.LogError(ex, "Lỗi DbUpdateException khi xóa Employee Id: {EmployeeId}. Inner: {InnerMessage}", id, ex.InnerException?.Message);
                // Ném lại lỗi để Business/Controller xử lý
                throw new InvalidOperationException($"Không thể xóa nhân viên này do có dữ liệu liên quan (ví dụ: Văn bằng). Vui lòng kiểm tra lại.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi không mong muốn khi xóa Employee Id: {EmployeeId}", id);
                throw;
            }
        }
        
    }

}