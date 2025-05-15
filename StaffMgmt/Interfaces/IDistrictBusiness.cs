using StaffMgmt.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StaffMgmt.Interfaces
{
    public interface IDistrictBusiness
    {
        Task<IEnumerable<District>> GetDistrictsByProvinceIdAsync(int provinceId);
    }
}