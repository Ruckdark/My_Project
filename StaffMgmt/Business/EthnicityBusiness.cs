using StaffMgmt.Interfaces;
using StaffMgmt.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StaffMgmt.Business
{
    public class EthnicityBusiness : IEthnicityBusiness
    {
        private readonly IEthnicityService _ethnicityService;
        public EthnicityBusiness(IEthnicityService ethnicityService) { _ethnicityService = ethnicityService; }

        public async Task<IEnumerable<Ethnicity>> GetAllEthnicitiesAsync()
        {
            // TODO: Thêm logic nghiệp vụ nếu cần
            return await _ethnicityService.GetAllEthnicitiesAsync();
        }
    }
}