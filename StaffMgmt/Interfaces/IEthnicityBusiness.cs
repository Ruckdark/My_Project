using StaffMgmt.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StaffMgmt.Interfaces
{
    public interface IEthnicityBusiness
    {
        Task<IEnumerable<Ethnicity>> GetAllEthnicitiesAsync();
    }
}