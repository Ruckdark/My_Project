// File: Business/ProvinceBusiness.cs
using Microsoft.EntityFrameworkCore;
using StaffMgmt.Interfaces;
using StaffMgmt.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StaffMgmt.Business
{
    public class ProvinceBusiness : IProvinceBusiness
    {
        private readonly IProvinceService _provinceService; // Inject Service Interface

        public ProvinceBusiness(IProvinceService provinceService)
        {
            _provinceService = provinceService;
        }

        public async Task<IEnumerable<Province>> GetAllProvincesAsync()
        {
            // Hiện tại chỉ gọi qua Service, sau này có thể thêm logic ở đây
            return await _provinceService.GetAllProvincesAsync();
        }

        public async Task<Province?> GetProvinceByIdAsync(int id)
        {
            return await _provinceService.GetProvinceByIdAsync(id);
        }

        public async Task AddProvinceAsync(Province province)
        {
            // TODO: Thêm Validation logic ở đây trước khi gọi Service
            // Ví dụ: Kiểm tra tên tỉnh không được trùng? (Cần thêm phương thức vào Service)
            // if (await _provinceService.ProvinceNameExistsAsync(province.Name)) { ... }

            await _provinceService.AddProvinceAsync(province);
        }

        public async Task UpdateProvinceAsync(Province province)
        {
            // TODO: Thêm Validation logic ở đây
            await _provinceService.UpdateProvinceAsync(province);
        }

        public async Task DeleteProvinceAsync(int id)
        {
            // TODO: Thêm kiểm tra nghiệp vụ ở đây trước khi xóa
            // Ví dụ: Kiểm tra xem tỉnh có đang được sử dụng ở đâu không (ngoài Huyện đã xử lý bằng FK)
            // Cần kiểm tra lỗi từ Service (ví dụ KeyNotFoundException hoặc DbUpdateException do FK constraint)
            try
            {
                await _provinceService.DeleteProvinceAsync(id);
            }
            catch (DbUpdateException dbEx)
            {
                // Xử lý lỗi nếu không xóa được do ràng buộc khóa ngoại (ví dụ: còn Employee tham chiếu)
                // Ghi log và/hoặc throw lỗi thân thiện hơn cho Controller
                // Ví dụ: throw new InvalidOperationException("Không thể xóa tỉnh này vì đang có dữ liệu liên quan.", dbEx);
                throw new InvalidOperationException("Không thể xóa tỉnh này vì đang có dữ liệu liên quan.", dbEx); // Ném lại lỗi rõ ràng hơn
            }
            catch (KeyNotFoundException)
            {
                throw; // Ném lại lỗi không tìm thấy
            }

        }
    }
}