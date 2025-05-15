// File: Business/EmployeeBusiness.cs

using FluentValidation;
using Microsoft.Extensions.Logging;
using StaffMgmt.Interfaces;
using StaffMgmt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StaffMgmt.Business
{
    public class EmployeeBusiness : IEmployeeBusiness
    {
        private readonly IEmployeeService _employeeService;
        private readonly IValidator<EmployeeFormViewModel> _validator;
        private readonly ILogger<EmployeeBusiness> _logger;

        public EmployeeBusiness(
            IEmployeeService employeeService,
            IValidator<EmployeeFormViewModel> validator,
            ILogger<EmployeeBusiness> logger)
        {
            _employeeService = employeeService;
            _validator = validator;
            _logger = logger;
        }

        // --- Sửa lại chữ ký phương thức này ---
        public async Task AddEmployeeAsync(EmployeeFormViewModel viewModel) // <<=== Sửa ở đây
        {
            _logger.LogInformation("Bắt đầu xử lý nghiệp vụ thêm Employee từ ViewModel: {FullName}", viewModel.FullName);

            _logger.LogDebug("Bắt đầu validate ViewModel...");
            await _validator.ValidateAndThrowAsync(viewModel); // Validate ViewModel
            _logger.LogDebug("ViewModel hợp lệ.");

            _logger.LogDebug("Bắt đầu mapping ViewModel sang Model Employee...");
            var employee = new Employee // Mapping diễn ra ở đây
            {
                FullName = viewModel.FullName,
                DateOfBirth = viewModel.DateOfBirth,
                EthnicityId = viewModel.EthnicityId,
                OccupationId = viewModel.OccupationId,
                IdentityCardNumber = viewModel.HasNoIdentityCard ? null : viewModel.IdentityCardNumber?.Trim(),
                PhoneNumber = viewModel.HasNoPhoneNumber ? null : viewModel.PhoneNumber?.Trim(),
                ProvinceId = viewModel.ProvinceId,
                DistrictId = viewModel.DistrictId,
                WardId = viewModel.WardId,
                StreetAddress = viewModel.StreetAddress?.Trim()
            };
            _logger.LogDebug("Mapping hoàn tất.");

            try
            {
                // Gọi Service để lưu Model Employee
                await _employeeService.AddEmployeeAsync(employee); // <<=== Service vẫn nhận Model Employee
                _logger.LogInformation("Đã gọi EmployeeService để thêm Employee thành công.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi gọi EmployeeService.AddEmployeeAsync.");
                throw new ApplicationException("Đã xảy ra lỗi phía server khi lưu dữ liệu.", ex);
            }
        }

        public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
        {
            // ... (Giữ nguyên) ...
            _logger.LogInformation("Bắt đầu lấy danh sách Employee từ EmployeeService.");
            try { return await _employeeService.GetAllEmployeesAsync(); }
            catch (Exception ex) { _logger.LogError(ex, "Lỗi khi gọi EmployeeService.GetAllEmployeesAsync."); throw; }
        }

        /// <summary>
        /// Lấy thông tin Employee theo Id.
        /// </summary>
        public async Task<Employee?> GetEmployeeByIdAsync(int id)
        {
            _logger.LogInformation("Bắt đầu lấy Employee Id: {EmployeeId} từ EmployeeService.", id);
            try
            {
                return await _employeeService.GetEmployeeByIdAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi gọi EmployeeService.GetEmployeeByIdAsync cho Id: {EmployeeId}", id);
                throw; // Ném lại lỗi để Controller xử lý
            }
        }

        /// <summary>
        /// Cập nhật thông tin Employee.
        /// </summary>
        public async Task UpdateEmployeeAsync(EmployeeFormViewModel viewModel)
        {
            _logger.LogInformation("Bắt đầu xử lý nghiệp vụ cập nhật Employee Id: {EmployeeId}", viewModel.Id);

            // --- Thực hiện Validation bằng FluentValidation ---
            _logger.LogDebug("Bắt đầu validate ViewModel cho việc cập nhật...");
            // ValidateAndThrowAsync sẽ ném ValidationException nếu có lỗi
            await _validator.ValidateAndThrowAsync(viewModel);
            _logger.LogDebug("ViewModel hợp lệ cho việc cập nhật.");

            // --- Lấy Employee hiện tại từ DB ---
            // Cần lấy bản ghi hiện tại để cập nhật, không tạo mới
            var existingEmployee = await _employeeService.GetEmployeeByIdAsync(viewModel.Id);
            if (existingEmployee == null)
            {
                _logger.LogWarning("Không tìm thấy Employee Id: {EmployeeId} để cập nhật.", viewModel.Id);
                // Ném lỗi cụ thể để Controller bắt
                throw new KeyNotFoundException($"Không tìm thấy Employee với Id {viewModel.Id}.");
            }

            // --- Mapping từ ViewModel sang Model hiện tại ---
            _logger.LogDebug("Bắt đầu mapping ViewModel sang Model Employee hiện tại...");
            // Map thủ công hoặc dùng AutoMapper
            existingEmployee.FullName = viewModel.FullName;
            existingEmployee.DateOfBirth = viewModel.DateOfBirth;
            existingEmployee.EthnicityId = viewModel.EthnicityId;
            existingEmployee.OccupationId = viewModel.OccupationId;
            existingEmployee.IdentityCardNumber = viewModel.HasNoIdentityCard ? null : viewModel.IdentityCardNumber?.Trim();
            existingEmployee.PhoneNumber = viewModel.HasNoPhoneNumber ? null : viewModel.PhoneNumber?.Trim();
            existingEmployee.ProvinceId = viewModel.ProvinceId;
            existingEmployee.DistrictId = viewModel.DistrictId;
            existingEmployee.WardId = viewModel.WardId;
            existingEmployee.StreetAddress = viewModel.StreetAddress?.Trim();
            // KHÔNG map Id
            // TODO: Mapping các trường khác nếu có
            _logger.LogDebug("Mapping hoàn tất.");

            try
            {
                // Gọi Service để cập nhật Employee đã được map
                await _employeeService.UpdateEmployeeAsync(existingEmployee);
                _logger.LogInformation("Đã gọi EmployeeService.UpdateEmployeeAsync thành công cho Employee Id: {EmployeeId}", viewModel.Id);
            }
            catch (Exception ex) // Bắt lỗi từ Service Layer
            {
                _logger.LogError(ex, "Lỗi khi gọi EmployeeService.UpdateEmployeeAsync cho Id: {EmployeeId}", viewModel.Id);
                throw new ApplicationException("Đã xảy ra lỗi phía server khi cập nhật dữ liệu.", ex);
            }
        }

        /// <summary>
        /// Xóa Employee theo Id.
        /// </summary>
        public async Task DeleteEmployeeAsync(int id)
        {
            _logger.LogInformation("Bắt đầu xử lý nghiệp vụ xóa Employee Id: {EmployeeId}", id);
            // --- TODO: Thêm kiểm tra nghiệp vụ trước khi xóa (nếu cần) ---
            // Ví dụ: Kiểm tra xem Employee có đang tham gia dự án nào không...

            try
            {
                // Gọi Service để thực hiện xóa
                await _employeeService.DeleteEmployeeAsync(id);
                _logger.LogInformation("Đã gọi EmployeeService.DeleteEmployeeAsync thành công cho Id: {EmployeeId}", id);
            }
            catch (KeyNotFoundException) // Bắt lỗi không tìm thấy từ Service
            {
                _logger.LogWarning("Không tìm thấy Employee Id: {EmployeeId} khi gọi Delete.", id);
                throw; // Ném lại để Controller xử lý
            }
            catch (InvalidOperationException) // Bắt lỗi không xóa được do FK từ Service
            {
                _logger.LogWarning("Không thể xóa Employee Id: {EmployeeId} do ràng buộc dữ liệu.", id);
                throw; // Ném lại để Controller xử lý
            }
            catch (Exception ex) // Bắt các lỗi khác
            {
                _logger.LogError(ex, "Lỗi khi gọi EmployeeService.DeleteEmployeeAsync cho Id: {EmployeeId}", id);
                throw new ApplicationException("Đã xảy ra lỗi phía server khi xóa dữ liệu.", ex);
            }
        }
    }
}
