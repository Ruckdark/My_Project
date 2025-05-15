using StaffMgmt.Interfaces;
using StaffMgmt.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StaffMgmt.Business
{
    public class OccupationBusiness : IOccupationBusiness
    {
        private readonly IOccupationService _occupationService;
        public OccupationBusiness(IOccupationService occupationService) { _occupationService = occupationService; }

        public async Task<IEnumerable<Occupation>> GetAllOccupationsAsync()
        {
            // TODO: Thêm logic nghiệp vụ nếu cần
            return await _occupationService.GetAllOccupationsAsync();
        }
    }
}