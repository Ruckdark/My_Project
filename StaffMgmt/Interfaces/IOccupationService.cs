using StaffMgmt.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StaffMgmt.Interfaces
{
    public interface IOccupationService
    {
        Task<IEnumerable<Occupation>> GetAllOccupationsAsync();
        // Thêm các phương thức CRUD khác nếu cần
    }
}