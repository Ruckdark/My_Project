#region Using Directives
using FluentValidation;
using StaffMgmt.Interfaces;
using StaffMgmt.Models;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.Linq;
#endregion

namespace StaffMgmt.Validators
{
    public class EmployeeFormViewModelValidator : AbstractValidator<EmployeeFormViewModel>
    {
        // Inject các dependency cần thiết
        private readonly IDistrictService _districtService;
        private readonly IWardService _wardService;
        private readonly IEmployeeService _employeeService;
        private readonly ILogger<EmployeeFormViewModelValidator> _logger;

        public EmployeeFormViewModelValidator(
            IDistrictService districtService,
            IWardService wardService,
            IEmployeeService employeeService,
            ILogger<EmployeeFormViewModelValidator> logger)
        {
            _districtService = districtService;
            _wardService = wardService;
            _employeeService = employeeService;
            _logger = logger;

            _logger.LogInformation("EmployeeFormViewModelValidator được khởi tạo.");

            // --- Định nghĩa các quy tắc validation ---
            RuleFor(vm => vm.FullName)
                .NotEmpty().WithMessage("Họ tên là bắt buộc.")
                .MaximumLength(150).WithMessage("Họ tên không được vượt quá 150 ký tự.");

            RuleFor(vm => vm.EthnicityId)
                .NotNull().WithMessage("Vui lòng chọn Dân tộc.");

            RuleFor(vm => vm.OccupationId)
                .NotNull().WithMessage("Vui lòng chọn Nghề nghiệp.");

            RuleFor(vm => vm.IdentityCardNumber)
                .NotEmpty().WithMessage("Số CCCD là bắt buộc.").When(vm => !vm.HasNoIdentityCard)
                .MaximumLength(20).WithMessage("Số CCCD không được vượt quá 20 ký tự.").When(vm => !vm.HasNoIdentityCard)
                // Gọi hàm kiểm tra trùng lặp CCCD (đã implement)
                .MustAsync(BeUniqueIdentityCardNumber).WithMessage("Số CCCD này đã tồn tại.")
                    .When(vm => !vm.HasNoIdentityCard && !string.IsNullOrEmpty(vm.IdentityCardNumber));

            RuleFor(vm => vm.PhoneNumber)
               .NotEmpty().WithMessage("Số điện thoại là bắt buộc.").When(vm => !vm.HasNoPhoneNumber)
               .MaximumLength(15).WithMessage("Số điện thoại không được vượt quá 15 ký tự.").When(vm => !vm.HasNoPhoneNumber)
               .Matches(@"^(\+84|0)\d{9,10}$").WithMessage("Định dạng số điện thoại không hợp lệ.").When(vm => !vm.HasNoPhoneNumber && !string.IsNullOrEmpty(vm.PhoneNumber))
               // Gọi hàm kiểm tra trùng lặp SĐT (đã implement)
               .MustAsync(BeUniquePhoneNumber).WithMessage("Số điện thoại này đã tồn tại.")
                   .When(vm => !vm.HasNoPhoneNumber && !string.IsNullOrEmpty(vm.PhoneNumber));

            RuleFor(vm => vm.ProvinceId).NotNull().WithMessage("Vui lòng chọn Tỉnh/Thành phố.");
            RuleFor(vm => vm.DistrictId).NotNull().WithMessage("Vui lòng chọn Quận/Huyện.");
            RuleFor(vm => vm.WardId).NotNull().WithMessage("Vui lòng chọn Xã/Phường.");

            RuleFor(vm => vm.DistrictId)
               .MustAsync(async (viewModel, districtId, context, cancellationToken) =>
               {
                   if (!viewModel.ProvinceId.HasValue || !districtId.HasValue || districtId.Value <= 0 || viewModel.ProvinceId.Value <= 0) return true;
                   _logger.LogDebug("Validating District {DistrictId} against Province {ProvinceId}", districtId.Value, viewModel.ProvinceId.Value);
                   // --- Gọi hàm kiểm tra logic địa chỉ (đã implement) ---
                   return await IsDistrictInProvince(districtId.Value, viewModel.ProvinceId.Value, cancellationToken);
               })
               .WithMessage("Quận/Huyện không thuộc Tỉnh/Thành phố đã chọn.")
               .When(vm => vm.ProvinceId.HasValue && vm.DistrictId.HasValue);

            RuleFor(vm => vm.WardId)
               .MustAsync(async (viewModel, wardId, context, cancellationToken) =>
               {
                   if (!viewModel.DistrictId.HasValue || !wardId.HasValue || wardId.Value <= 0 || viewModel.DistrictId.Value <= 0) return true;
                   _logger.LogDebug("Validating Ward {WardId} against District {DistrictId}", wardId.Value, viewModel.DistrictId.Value);
                   // --- Gọi hàm kiểm tra logic địa chỉ (đã implement) ---
                   return await IsWardInDistrict(wardId.Value, viewModel.DistrictId.Value, cancellationToken);
               })
               .WithMessage("Xã/Phường không thuộc Quận/Huyện đã chọn.")
               .When(vm => vm.DistrictId.HasValue && vm.WardId.HasValue);

            RuleFor(vm => vm.StreetAddress)
                .MaximumLength(255).WithMessage("Địa chỉ cụ thể không được vượt quá 255 ký tự.");
        }

        // --- Hàm kiểm tra bất đồng bộ (Đã hoàn thiện) ---
        private async Task<bool> IsDistrictInProvince(int districtId, int provinceId, CancellationToken cancellationToken)
        {
            _logger.LogDebug("Calling _districtService.GetDistrictByIdAsync({DistrictId}) for validation", districtId);
            try
            {
                var district = await _districtService.GetDistrictByIdAsync(districtId);
                if (district == null)
                {
                    _logger.LogWarning("Validation failed: District with Id {DistrictId} not found.", districtId);
                    return false; // Huyện không tồn tại -> không hợp lệ
                }
                bool isValid = district.ProvinceId == provinceId;
                _logger.LogDebug("Validation result for District {DistrictId} in Province {ProvinceId}: {IsValid}", districtId, provinceId, isValid);
                return isValid;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi gọi GetDistrictByIdAsync trong validation cho DistrictId: {DistrictId}", districtId);
                return false; // Coi như không hợp lệ nếu có lỗi xảy ra khi kiểm tra
            }
        }

        private async Task<bool> IsWardInDistrict(int wardId, int districtId, CancellationToken cancellationToken)
        {
            _logger.LogDebug("Calling _wardService.GetWardByIdAsync({WardId}) for validation", wardId);
            try
            {
                var ward = await _wardService.GetWardByIdAsync(wardId);
                if (ward == null)
                {
                    _logger.LogWarning("Validation failed: Ward with Id {WardId} not found.", wardId);
                    return false; // Xã không tồn tại -> không hợp lệ
                }
                bool isValid = ward.DistrictId == districtId;
                _logger.LogDebug("Validation result for Ward {WardId} in District {DistrictId}: {IsValid}", wardId, districtId, isValid);
                return isValid;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi gọi GetWardByIdAsync trong validation cho WardId: {WardId}", wardId);
                return false;
            }
        }

        private async Task<bool> BeUniqueIdentityCardNumber(EmployeeFormViewModel viewModel, string identityCardNumber, ValidationContext<EmployeeFormViewModel> context, CancellationToken cancellationToken)
        {
            // RuleFor đã kiểm tra NotEmpty nên không cần kiểm tra lại ở đây
            _logger.LogDebug("Checking uniqueness for IdentityCardNumber: {IdentityCardNumber}, excluding EmployeeId: {EmployeeId}", identityCardNumber, viewModel.Id);
            try
            {
                // Gọi Service để kiểm tra CCCD tồn tại (loại trừ Id hiện tại nếu có)
                bool exists = await _employeeService.CheckCccdExistsAsync(identityCardNumber, viewModel.Id > 0 ? viewModel.Id : null);
                _logger.LogDebug("CheckCccdExistsAsync result: {Exists}", exists);
                // Trả về true nếu KHÔNG tồn tại (tức là unique)
                return !exists;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi gọi CheckCccdExistsAsync trong validation cho CCCD: {IdentityCardNumber}", identityCardNumber);
                return false; // Coi như không hợp lệ nếu có lỗi xảy ra khi kiểm tra
            }
        }

        private async Task<bool> BeUniquePhoneNumber(EmployeeFormViewModel viewModel, string phoneNumber, ValidationContext<EmployeeFormViewModel> context, CancellationToken cancellationToken)
        {
            // RuleFor đã kiểm tra NotEmpty và Matches
            _logger.LogDebug("Checking uniqueness for PhoneNumber: {PhoneNumber}, excluding EmployeeId: {EmployeeId}", phoneNumber, viewModel.Id);
            try
            {
                // Gọi Service để kiểm tra SĐT tồn tại (loại trừ Id hiện tại nếu có)
                bool exists = await _employeeService.CheckPhoneNumberExistsAsync(phoneNumber, viewModel.Id > 0 ? viewModel.Id : null);
                _logger.LogDebug("CheckPhoneNumberExistsAsync result: {Exists}", exists);
                // Trả về true nếu KHÔNG tồn tại
                return !exists;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi gọi CheckPhoneNumberExistsAsync trong validation cho SĐT: {PhoneNumber}", phoneNumber);
                return false;
            }
        }
    }
}
