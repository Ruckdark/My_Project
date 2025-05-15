// File: Interfaces/IEthnicityService.cs
using StaffMgmt.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StaffMgmt.Interfaces
{
    public interface IEthnicityService
    {
        Task<IEnumerable<Ethnicity>> GetAllEthnicitiesAsync();
        // Thêm các phương thức CRUD khác nếu cần
    }
}