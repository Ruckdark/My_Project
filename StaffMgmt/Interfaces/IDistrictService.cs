using StaffMgmt.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StaffMgmt.Interfaces
{
    public interface IDistrictService
    {
        Task<IEnumerable<District>> GetDistrictsByProvinceIdAsync(int provinceId);
        Task<District?> GetDistrictByIdAsync(int id);
    }
}