using StaffMgmt.Interfaces;
using StaffMgmt.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StaffMgmt.Business
{
    public class WardBusiness : IWardBusiness
    {
        private readonly IWardService _wardService;

        public WardBusiness(IWardService wardService)
        {
            _wardService = wardService;
        }

        public async Task<IEnumerable<Ward>> GetWardsByDistrictIdAsync(int districtId)
        {
            // Hiện tại chỉ gọi qua Service
            return await _wardService.GetWardsByDistrictIdAsync(districtId);
        }
    }
}