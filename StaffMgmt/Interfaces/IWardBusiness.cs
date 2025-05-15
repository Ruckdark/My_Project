using StaffMgmt.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StaffMgmt.Interfaces
{
    public interface IWardBusiness
    {
        Task<IEnumerable<Ward>> GetWardsByDistrictIdAsync(int districtId);
    }
}