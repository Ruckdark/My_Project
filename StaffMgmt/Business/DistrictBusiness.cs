using StaffMgmt.Interfaces;
using StaffMgmt.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StaffMgmt.Business
{
    public class DistrictBusiness : IDistrictBusiness
    {
        private readonly IDistrictService _districtService;

        public DistrictBusiness(IDistrictService districtService)
        {
            _districtService = districtService;
        }

        public async Task<IEnumerable<District>> GetDistrictsByProvinceIdAsync(int provinceId)
        {
            // Hiện tại chỉ gọi qua Service
            return await _districtService.GetDistrictsByProvinceIdAsync(provinceId);
        }
    }
}