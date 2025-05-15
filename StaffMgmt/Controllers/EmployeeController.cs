// File: Controllers/EmployeeController.cs

#region Using Directives
// Các namespace cần thiết cho Controller, ViewModel, Interface, EF Core, Logging,...
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering; // Cần cho SelectList
using StaffMgmt.Interfaces;           // Namespace chứa các Interfaces
using StaffMgmt.Models;               // Namespace chứa Model (Employee, Province, etc.)
// using StaffMgmt.ViewModels;        // Namespace chứa ViewModel (nếu tách riêng)
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging; // Cần cho ILogger
using FluentValidation;             // Thêm using cho ValidationException
using Microsoft.EntityFrameworkCore; // Thêm using cho DbUpdateConcurrencyException
using Microsoft.Extensions.DependencyInjection; // <<=== Thêm using cho IServiceScopeFactory
#endregion

namespace StaffMgmt.Controllers
{
    /// <summary>
    /// Controller chịu trách nhiệm xử lý các yêu cầu liên quan đến quản lý Nhân viên (Employee).
    /// </summary>
    public class EmployeeController : Controller
    {
        #region Private Fields & Constructor

        // Giữ lại các Business Layer chính cần cho các Action khác
        private readonly IEmployeeBusiness _employeeBusiness;
        private readonly ILogger<EmployeeController> _logger;
        // Thêm IServiceScopeFactory để tạo scope riêng cho việc load dropdown
        private readonly IServiceScopeFactory _scopeFactory;

        // Constructor nhận các dependency
        public EmployeeController(
            IEmployeeBusiness employeeBusiness,
            // Không cần inject các business chỉ dùng trong Populate nữa
            IServiceScopeFactory scopeFactory, // Inject ScopeFactory
            ILogger<EmployeeController> logger)
        {
            _employeeBusiness = employeeBusiness ?? throw new ArgumentNullException(nameof(employeeBusiness));
            // Gán ScopeFactory
            _scopeFactory = scopeFactory ?? throw new ArgumentNullException(nameof(scopeFactory));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        #endregion

        #region Action Methods (CRUD Operations)

        // GET: Employee/Index
        /// <summary>
        /// Hiển thị trang danh sách các nhân viên.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            _logger.LogInformation("Action Index (GET) được gọi.");
            try
            {
                var employeeList = await _employeeBusiness.GetAllEmployeesAsync();
                _logger.LogInformation("Lấy được {Count} nhân viên từ Business Layer.", employeeList.Count());
                return View(employeeList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi lấy danh sách Employee cho trang Index.");
                TempData["ErrorMessage"] = "Không thể tải danh sách nhân viên.";
                return View(new List<Employee>()); // Trả về danh sách rỗng nếu lỗi
            }
        }

        // GET: Employee/Details/{id}
        /// <summary>
        /// Hiển thị trang chi tiết thông tin của một nhân viên.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            _logger.LogInformation("Action Details (GET) được gọi với Id: {EmployeeId}", id);
            if (id == null) { _logger.LogWarning("Details (GET): Id is null."); return NotFound("Yêu cầu không hợp lệ."); }

            try
            {
                var employee = await _employeeBusiness.GetEmployeeByIdAsync(id.Value);
                if (employee == null) { _logger.LogWarning("Details (GET): Không tìm thấy Employee với Id: {EmployeeId}", id.Value); return NotFound($"Không tìm thấy nhân viên với ID {id.Value}."); }

                _logger.LogInformation("Tìm thấy Employee: {FullName}. Chuẩn bị hiển thị View Details.", employee.FullName);
                // TODO: Map sang EmployeeDetailsViewModel nếu cần
                return View(employee); // Truyền Model Employee cho View Details.cshtml
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi lấy chi tiết Employee Id: {EmployeeId}", id.Value);
                TempData["ErrorMessage"] = "Không thể tải thông tin chi tiết nhân viên.";
                return RedirectToAction(nameof(Index));
            }
        }


        // GET: Employee/Create
        /// <summary>
        /// Hiển thị form để tạo mới một nhân viên.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            _logger.LogInformation("Action Create (GET) được gọi.");
            try
            {
                var viewModel = new EmployeeFormViewModel();
                // Gọi helper để chuẩn bị dropdowns (helper sẽ dùng scope riêng)
                await PopulateViewModelDropdownsAsync(viewModel);
                _logger.LogInformation("Chuẩn bị hiển thị View EmployeeForm để tạo mới.");
                return View("EmployeeForm", viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi xảy ra trong Action Create (GET).");
                TempData["ErrorMessage"] = "Đã xảy ra lỗi khi tải form tạo mới.";
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Employee/Create
        /// <summary>
        /// Xử lý dữ liệu được gửi lên từ form tạo mới nhân viên.
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmployeeFormViewModel viewModel)
        {
            _logger.LogInformation("Action Create (POST) được gọi với ViewModel.");
            _logger.LogInformation("Bắt đầu gọi EmployeeBusiness.AddEmployeeAsync...");
            try
            {
                // Gọi Business Layer (đã bao gồm validation bên trong)
                await _employeeBusiness.AddEmployeeAsync(viewModel);
                _logger.LogInformation("EmployeeBusiness.AddEmployeeAsync thực thi thành công.");

                TempData["UserMessage"] = $"Đã thêm nhân viên '{viewModel.FullName}' thành công!";
                TempData["MessageType"] = "success";
                return RedirectToAction(nameof(Index));
            }
            catch (ValidationException valEx) // Bắt lỗi từ FluentValidation
            {
                _logger.LogWarning("Lỗi Validation từ Business Layer/FluentValidation: {Message}", valEx.Message);
                foreach (var error in valEx.Errors) { ModelState.AddModelError(error.PropertyName ?? "", error.ErrorMessage); }
            }
            catch (ApplicationException appEx) // Bắt lỗi nghiệp vụ khác
            {
                _logger.LogError(appEx, "Lỗi nghiệp vụ khi thêm Employee.");
                ModelState.AddModelError("", appEx.Message);
            }
            catch (Exception ex) // Bắt các lỗi không mong muốn khác
            {
                _logger.LogError(ex, "Lỗi không mong muốn khi thêm Employee mới.");
                ModelState.AddModelError("", "Đã xảy ra lỗi hệ thống. Vui lòng thử lại.");
            }

            // Nếu có lỗi xảy ra (ModelState không hợp lệ hoặc Exception)
            _logger.LogInformation("Có lỗi. Chuẩn bị load lại dropdown và hiển thị lại View EmployeeForm.");
            await PopulateViewModelDropdownsAsync(viewModel); // Load lại dropdown
            return View("EmployeeForm", viewModel); // Trả về View với dữ liệu và lỗi
        }

        // GET: Employee/Edit/{id}
        /// <summary>
        /// Hiển thị form để chỉnh sửa thông tin nhân viên hiện có.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            _logger.LogInformation("Action Edit (GET) được gọi với Id: {EmployeeId}", id);
            if (id == null) { _logger.LogWarning("Edit (GET): Id is null."); return NotFound("Yêu cầu không hợp lệ."); }
            try
            {
                var employee = await _employeeBusiness.GetEmployeeByIdAsync(id.Value);
                if (employee == null) { _logger.LogWarning("Edit (GET): Không tìm thấy Employee Id: {EmployeeId}", id.Value); return NotFound($"Không tìm thấy nhân viên với ID {id.Value}."); }

                _logger.LogDebug("Bắt đầu mapping Model sang ViewModel cho Edit...");
                var viewModel = new EmployeeFormViewModel
                {
                    Id = employee.Id,
                    FullName = employee.FullName,
                    DateOfBirth = employee.DateOfBirth,
                    EthnicityId = employee.EthnicityId,
                    OccupationId = employee.OccupationId,
                    IdentityCardNumber = employee.IdentityCardNumber,
                    HasNoIdentityCard = string.IsNullOrEmpty(employee.IdentityCardNumber),
                    PhoneNumber = employee.PhoneNumber,
                    HasNoPhoneNumber = string.IsNullOrEmpty(employee.PhoneNumber),
                    ProvinceId = employee.ProvinceId,
                    DistrictId = employee.DistrictId,
                    WardId = employee.WardId,
                    StreetAddress = employee.StreetAddress
                };
                _logger.LogDebug("Mapping hoàn tất.");

                // Gọi helper để chuẩn bị dropdowns (helper sẽ dùng scope riêng)
                await PopulateViewModelDropdownsAsync(viewModel);
                _logger.LogInformation("Chuẩn bị hiển thị View EmployeeForm để chỉnh sửa.");
                return View("EmployeeForm", viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi tải form Edit cho Employee Id: {EmployeeId}", id.Value);
                TempData["ErrorMessage"] = "Không thể tải thông tin để chỉnh sửa.";
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Employee/Edit/{id}
        /// <summary>
        /// Xử lý dữ liệu được gửi lên từ form chỉnh sửa nhân viên.
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EmployeeFormViewModel viewModel)
        {
            _logger.LogInformation("Action Edit (POST) được gọi với Id: {EmployeeId}", id);
            if (id != viewModel.Id) { _logger.LogWarning("Edit (POST): Id route ({RouteId}) không khớp Id form ({FormId}).", id, viewModel.Id); return BadRequest("Dữ liệu không hợp lệ."); }

            _logger.LogInformation("Bắt đầu gọi EmployeeBusiness.UpdateEmployeeAsync cho Id: {EmployeeId}...", viewModel.Id);
            try
            {
                await _employeeBusiness.UpdateEmployeeAsync(viewModel);
                _logger.LogInformation("EmployeeBusiness.UpdateEmployeeAsync thành công cho Id: {EmployeeId}", viewModel.Id);
                TempData["UserMessage"] = $"Đã cập nhật thông tin nhân viên '{viewModel.FullName}' thành công!";
                TempData["MessageType"] = "success";
                return RedirectToAction(nameof(Index));
            }
            catch (ValidationException valEx) { _logger.LogWarning("Lỗi Validation khi cập nhật: {Message}", valEx.Message); foreach (var error in valEx.Errors) { ModelState.AddModelError(error.PropertyName ?? "", error.ErrorMessage); } }
            catch (KeyNotFoundException) { _logger.LogWarning("Edit (POST): Không tìm thấy Employee Id: {EmployeeId}.", viewModel.Id); ModelState.AddModelError("", "Không tìm thấy nhân viên này."); }
            catch (DbUpdateConcurrencyException) { _logger.LogWarning("Lỗi DbUpdateConcurrencyException khi cập nhật Employee Id: {EmployeeId}.", viewModel.Id); ModelState.AddModelError("", "Dữ liệu đã bị thay đổi."); }
            catch (ApplicationException appEx) { _logger.LogError(appEx, "Lỗi nghiệp vụ khi cập nhật."); ModelState.AddModelError("", appEx.Message); }
            catch (Exception ex) { _logger.LogError(ex, "Lỗi không mong muốn khi cập nhật."); ModelState.AddModelError("", "Đã xảy ra lỗi hệ thống."); }

            _logger.LogInformation("Có lỗi. Chuẩn bị load lại dropdown và hiển thị lại View EmployeeForm (Edit).");
            await PopulateViewModelDropdownsAsync(viewModel); // Load lại dropdown
            return View("EmployeeForm", viewModel);
        }


        // GET: Employee/Delete/{id}
        /// <summary>
        /// Hiển thị trang xác nhận xóa một nhân viên.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            _logger.LogInformation("Action Delete (GET) được gọi với Id: {EmployeeId}", id);
            if (id == null) { return NotFound("Yêu cầu không hợp lệ."); }
            try { var employee = await _employeeBusiness.GetEmployeeByIdAsync(id.Value); if (employee == null) { return NotFound($"Không tìm thấy nhân viên với ID {id.Value}."); } return View(employee); }
            catch (Exception ex) { _logger.LogError(ex, "Lỗi khi tải thông tin Delete cho Employee Id: {EmployeeId}", id.Value); TempData["ErrorMessage"] = "Không thể tải thông tin để xóa."; return RedirectToAction(nameof(Index)); }
        }

        // POST: Employee/Delete/{id}
        /// <summary>
        /// Xử lý yêu cầu xóa nhân viên.
        /// </summary>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            _logger.LogInformation("Action DeleteConfirmed (POST) được gọi với Id: {EmployeeId}", id);
            try { await _employeeBusiness.DeleteEmployeeAsync(id); _logger.LogInformation("EmployeeBusiness.DeleteEmployeeAsync thành công cho Id: {EmployeeId}", id); TempData["UserMessage"] = "Đã xóa nhân viên thành công!"; TempData["MessageType"] = "success"; }
            catch (KeyNotFoundException) { _logger.LogWarning("DeleteConfirmed (POST): Không tìm thấy Employee Id: {EmployeeId}.", id); TempData["UserMessage"] = "Nhân viên này đã được xóa trước đó."; TempData["MessageType"] = "warning"; }
            catch (InvalidOperationException ex) { _logger.LogWarning(ex, "Không thể xóa Employee Id: {EmployeeId} do ràng buộc.", id); TempData["ErrorMessage"] = ex.Message; }
            catch (ApplicationException appEx) { _logger.LogError(appEx, "Lỗi nghiệp vụ khi xóa."); TempData["ErrorMessage"] = appEx.Message; }
            catch (Exception ex) { _logger.LogError(ex, "Lỗi không mong muốn khi xóa."); TempData["ErrorMessage"] = "Đã xảy ra lỗi hệ thống."; }
            return RedirectToAction(nameof(Index));
        }

        #endregion

        #region Helper Methods
        /// <summary>
        /// Chuẩn bị dữ liệu SelectList cho các dropdown trong EmployeeFormViewModel.
        /// Sử dụng scope riêng để tránh lỗi DbContext concurrency.
        /// </summary>
        private async Task PopulateViewModelDropdownsAsync(EmployeeFormViewModel viewModel)
        {
            _logger.LogDebug("Bắt đầu PopulateViewModelDropdownsAsync cho ViewModel (Id: {EmployeeId}).", viewModel?.Id);
            if (viewModel == null) { _logger.LogError("ViewModel bị null trong PopulateViewModelDropdownsAsync."); viewModel = new EmployeeFormViewModel(); }

            viewModel.ProvinceList ??= Enumerable.Empty<SelectListItem>();
            viewModel.EthnicityList ??= Enumerable.Empty<SelectListItem>();
            viewModel.OccupationList ??= Enumerable.Empty<SelectListItem>();
            viewModel.DistrictList ??= Enumerable.Empty<SelectListItem>();
            viewModel.WardList ??= Enumerable.Empty<SelectListItem>();

            _logger.LogDebug("Tạo scope mới để lấy dữ liệu dropdown...");
            using (var scope = _scopeFactory.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;
                var provinceBusiness = serviceProvider.GetRequiredService<IProvinceBusiness>();
                var ethnicityBusiness = serviceProvider.GetRequiredService<IEthnicityBusiness>();
                var occupationBusiness = serviceProvider.GetRequiredService<IOccupationBusiness>();
                var districtBusiness = serviceProvider.GetRequiredService<IDistrictBusiness>();
                var wardBusiness = serviceProvider.GetRequiredService<IWardBusiness>();
                _logger.LogDebug("Đã resolve các business service trong scope mới.");

                try
                {
                    _logger.LogDebug("Đang lấy danh sách Tỉnh (trong scope mới)...");
                    var provinces = await provinceBusiness.GetAllProvincesAsync();
                    if (provinces != null) { viewModel.ProvinceList = new SelectList(provinces, nameof(Province.Id), nameof(Province.Name), viewModel.ProvinceId); } else { _logger.LogWarning("Province list is null."); }
                    _logger.LogDebug("Đã lấy xong Tỉnh.");

                    _logger.LogDebug("Đang lấy danh sách Dân tộc (trong scope mới)...");
                    var ethnicities = await ethnicityBusiness.GetAllEthnicitiesAsync();
                    if (ethnicities != null) { viewModel.EthnicityList = new SelectList(ethnicities, nameof(Ethnicity.Id), nameof(Ethnicity.Name), viewModel.EthnicityId); } else { _logger.LogWarning("Ethnicity list is null."); }
                    _logger.LogDebug("Đã lấy xong Dân tộc.");

                    _logger.LogDebug("Đang lấy danh sách Nghề nghiệp (trong scope mới)...");
                    var occupations = await occupationBusiness.GetAllOccupationsAsync();
                    if (occupations != null) { viewModel.OccupationList = new SelectList(occupations, nameof(Occupation.Id), nameof(Occupation.Name), viewModel.OccupationId); } else { _logger.LogWarning("Occupation list is null."); }
                    _logger.LogDebug("Đã lấy xong Nghề nghiệp.");

                    if (viewModel.ProvinceId.HasValue && viewModel.ProvinceId > 0)
                    {
                        _logger.LogDebug("ProvinceId ({ProvinceId}) có giá trị, đang load DistrictList (trong scope mới)...", viewModel.ProvinceId);
                        var districts = await districtBusiness.GetDistrictsByProvinceIdAsync(viewModel.ProvinceId.Value);
                        if (districts != null)
                        {
                            viewModel.DistrictList = new SelectList(districts, nameof(District.Id), nameof(District.Name), viewModel.DistrictId);
                            _logger.LogDebug("Đã load {Count} Districts.", districts.Count());
                        }
                        else { _logger.LogWarning("District list trả về null."); }

                        if (viewModel.DistrictId.HasValue && viewModel.DistrictId > 0)
                        {
                            _logger.LogDebug("DistrictId ({DistrictId}) có giá trị, đang load WardList (trong scope mới)...", viewModel.DistrictId);
                            var wards = await wardBusiness.GetWardsByDistrictIdAsync(viewModel.DistrictId.Value);
                            if (wards != null)
                            {
                                viewModel.WardList = new SelectList(wards, nameof(Ward.Id), nameof(Ward.Name), viewModel.WardId);
                                _logger.LogDebug("Đã load {Count} Wards.", wards.Count());
                            }
                            else { _logger.LogWarning("Ward list trả về null."); }
                        }
                    }
                    _logger.LogDebug("PopulateViewModelDropdownsAsync hoàn tất trong scope mới.");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Lỗi trong PopulateViewModelDropdownsAsync (bên trong scope).");
                    viewModel.ProvinceList ??= Enumerable.Empty<SelectListItem>();
                    viewModel.EthnicityList ??= Enumerable.Empty<SelectListItem>();
                    viewModel.OccupationList ??= Enumerable.Empty<SelectListItem>();
                    viewModel.DistrictList ??= Enumerable.Empty<SelectListItem>();
                    viewModel.WardList ??= Enumerable.Empty<SelectListItem>();
                }
            }
        }
        #endregion
    }
}
