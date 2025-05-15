// File: ApiControllers/AddressApiController.cs

#region Using Directives
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging; // <<=== Thêm using cho ILogger
using StaffMgmt.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;
#endregion

namespace StaffMgmt.ApiControllers
{
    [Route("api/address")]
    [ApiController]
    public class AddressApiController : ControllerBase
    {
        #region Private Fields & Constructor (Dependency Injection)

        private readonly IDistrictBusiness _districtBusiness;
        private readonly IWardBusiness _wardBusiness;
        private readonly ILogger<AddressApiController> _logger; // <<=== Thêm ILogger

        // Cập nhật Constructor để nhận ILogger
        public AddressApiController(
            IDistrictBusiness districtBusiness,
            IWardBusiness wardBusiness,
            ILogger<AddressApiController> logger) // <<=== Thêm tham số logger
        {
            _districtBusiness = districtBusiness;
            _wardBusiness = wardBusiness;
            _logger = logger; // <<=== Gán giá trị logger
        }

        #endregion

        #region API Endpoints

        // GET: api/address/districts/{provinceId}
        [HttpGet("districts/{provinceId}")]
        public async Task<IActionResult> GetDistrictsByProvinceId(int provinceId)
        {
            _logger.LogInformation("API: Nhận yêu cầu lấy danh sách huyện cho ProvinceId: {ProvinceId}", provinceId);
            if (provinceId <= 0)
            {
                _logger.LogWarning("API: ProvinceId không hợp lệ: {ProvinceId}", provinceId);
                return BadRequest("Province ID không hợp lệ.");
            }

            try
            {
                var districts = await _districtBusiness.GetDistrictsByProvinceIdAsync(provinceId);
                var result = districts.Select(d => new { d.Id, d.Name }).ToList();
                _logger.LogInformation("API: Trả về {Count} huyện cho ProvinceId: {ProvinceId}", result.Count, provinceId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                // --- Sửa ở đây: Thêm Logging ---
                _logger.LogError(ex, "API: Lỗi khi lấy danh sách huyện cho ProvinceId: {ProvinceId}", provinceId);
                // ---------------------------------
                return StatusCode(StatusCodes.Status500InternalServerError, "Đã xảy ra lỗi khi lấy danh sách Quận/Huyện.");
            }
        }

        // GET: api/address/wards/{districtId}
        [HttpGet("wards/{districtId}")]
        public async Task<IActionResult> GetWardsByDistrictId(int districtId)
        {
            _logger.LogInformation("API: Nhận yêu cầu lấy danh sách xã cho DistrictId: {DistrictId}", districtId);
            if (districtId <= 0)
            {
                _logger.LogWarning("API: DistrictId không hợp lệ: {DistrictId}", districtId);
                return BadRequest("District ID không hợp lệ.");
            }

            try
            {
                var wards = await _wardBusiness.GetWardsByDistrictIdAsync(districtId);
                var result = wards.Select(w => new { w.Id, w.Name }).ToList();
                _logger.LogInformation("API: Trả về {Count} xã cho DistrictId: {DistrictId}", result.Count, districtId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                // --- Sửa ở đây: Thêm Logging ---
                _logger.LogError(ex, "API: Lỗi khi lấy danh sách xã cho DistrictId: {DistrictId}", districtId);
                // ---------------------------------
                return StatusCode(StatusCodes.Status500InternalServerError, "Đã xảy ra lỗi khi lấy danh sách Xã/Phường.");
            }
        }

        #endregion
    }
}
