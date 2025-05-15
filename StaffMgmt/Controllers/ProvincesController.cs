using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore; // Có thể không cần nữa nếu chỉ dùng Business
using StaffMgmt.Data;             // Có thể không cần nữa
using StaffMgmt.Models;
using StaffMgmt.Interfaces;       // <<=== Thêm using cho Interfaces

namespace StaffMgmt.Controllers
{
    public class ProvincesController : Controller
    {
        // Thay vì inject DbContext, inject IProvinceBusiness
        // private readonly StaffMgmtDbContext _context; // <<=== Xóa hoặc comment dòng này
        private readonly IProvinceBusiness _provinceBusiness; // <<=== Thêm dòng này

        // Sửa Constructor để nhận IProvinceBusiness
        public ProvincesController(IProvinceBusiness provinceBusiness) // <<=== Sửa tham số
        {
            //_context = context; // <<=== Xóa hoặc comment dòng này
            _provinceBusiness = provinceBusiness; // <<=== Thêm dòng này
        }

        // GET: Provinces (Index View)
        public async Task<IActionResult> Index()
        {
            // Lấy dữ liệu từ Business Layer thay vì DbContext
            // return View(await _context.Provinces.ToListAsync()); // <<=== Thay thế dòng này
            return View(await _provinceBusiness.GetAllProvincesAsync()); // <<=== Bằng dòng này
        }

        // GET: Provinces/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Lấy dữ liệu từ Business Layer
            // var province = await _context.Provinces.FirstOrDefaultAsync(m => m.Id == id); // <<=== Thay thế
            var province = await _provinceBusiness.GetProvinceByIdAsync(id.Value); // <<=== Bằng dòng này (id.Value vì id là nullable int)

            if (province == null)
            {
                return NotFound();
            }

            return View(province); // Trả về View với Model Province lấy từ Business
        }

        // GET: Provinces/Create
        public IActionResult Create()
        {
            // Chỉ trả về View để hiển thị form Create
            return View();
        }

        // POST: Provinces/Create
        [HttpPost]
        [ValidateAntiForgeryToken] // Giúp chống tấn công CSRF
        // Sử dụng [Bind] để chỉ định các thuộc tính được phép binding từ form, tăng bảo mật
        // Không bind 'Id' vì nó được tạo tự động
        public async Task<IActionResult> Create([Bind("Name")] Province province)
        {
            // ModelState.IsValid kiểm tra các validation rule định nghĩa trong Model (ví dụ: [Required], [StringLength])
            if (ModelState.IsValid)
            {
                try
                {
                    // Gọi Business Layer để thêm
                    // _context.Add(province); // <<=== Thay thế
                    // await _context.SaveChangesAsync(); // <<=== Thay thế
                    await _provinceBusiness.AddProvinceAsync(province); // <<=== Bằng dòng này

                    return RedirectToAction(nameof(Index)); // Chuyển hướng về trang Index sau khi thêm thành công
                }
                catch (Exception ex) // Bắt lỗi chung (nên bắt lỗi cụ thể hơn nếu biết)
                {
                    // Log lỗi (sẽ tìm hiểu sau)
                    // ModelState.AddModelError("", "Đã xảy ra lỗi khi thêm tỉnh. Vui lòng thử lại.");
                    ModelState.AddModelError("", $"Lỗi khi thêm tỉnh: {ex.Message}"); // Hiển thị lỗi cụ thể hơn (tạm thời)
                }
            }
            // Nếu ModelState không valid hoặc có lỗi, hiển thị lại form Create với dữ liệu đã nhập và lỗi
            return View(province);
        }

        // GET: Provinces/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // var province = await _context.Provinces.FindAsync(id); // <<=== Thay thế
            var province = await _provinceBusiness.GetProvinceByIdAsync(id.Value); // <<=== Bằng dòng này

            if (province == null)
            {
                return NotFound();
            }
            return View(province); // Trả về View Edit với dữ liệu của Tỉnh cần sửa
        }

        // POST: Provinces/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        // Bind cả 'Id' vì cần biết record nào để cập nhật, và 'Name' là cái cần sửa
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Province province)
        {
            if (id != province.Id) // Kiểm tra xem Id từ route có khớp với Id trong form không
            {
                return NotFound();
            }

            if (ModelState.IsValid) // Kiểm tra validation của Model
            {
                try
                {
                    // Gọi Business Layer để cập nhật
                    // _context.Update(province); // <<=== Thay thế
                    // await _context.SaveChangesAsync(); // <<=== Thay thế
                    await _provinceBusiness.UpdateProvinceAsync(province); // <<=== Bằng dòng này
                }
                catch (DbUpdateConcurrencyException) // Bắt lỗi concurrency nếu có
                {
                    // Kiểm tra xem tỉnh có còn tồn tại không
                    var exists = await _provinceBusiness.GetProvinceByIdAsync(province.Id); // Dùng lại business method
                    if (exists == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        ModelState.AddModelError("", "Dữ liệu đã bị thay đổi bởi người khác. Vui lòng tải lại trang và thử lại.");
                        // Có thể làm phức tạp hơn: load lại dữ liệu mới nhất và cho user chọn merge...
                    }
                }
                catch (Exception ex) // Bắt lỗi khác
                {
                    ModelState.AddModelError("", $"Lỗi khi cập nhật tỉnh: {ex.Message}");
                }

                // Nếu không có lỗi concurrency hoặc lỗi khác làm gián đoạn, chuyển về Index
                if (ModelState.ErrorCount == 0)
                    return RedirectToAction(nameof(Index));
            }
            // Nếu ModelState không valid hoặc có lỗi, hiển thị lại form Edit
            return View(province);
        }

        // GET: Provinces/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // var province = await _context.Provinces.FirstOrDefaultAsync(m => m.Id == id); // <<=== Thay thế
            var province = await _provinceBusiness.GetProvinceByIdAsync(id.Value); // <<=== Bằng dòng này

            if (province == null)
            {
                return NotFound();
            }

            return View(province); // Hiển thị view xác nhận xóa
        }

        // POST: Provinces/Delete/5
        [HttpPost, ActionName("Delete")] // Đặt tên Action khác với GET để tránh trùng lặp signature (do cùng tham số id)
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                // Gọi Business Layer để xóa
                // var province = await _context.Provinces.FindAsync(id); // <<=== Thay thế
                // if (province != null) { _context.Provinces.Remove(province); } // <<=== Thay thế
                // await _context.SaveChangesAsync(); // <<=== Thay thế
                await _provinceBusiness.DeleteProvinceAsync(id); // <<=== Bằng dòng này

                return RedirectToAction(nameof(Index)); // Chuyển về Index sau khi xóa thành công
            }
            catch (InvalidOperationException ex) // Bắt lỗi nghiệp vụ từ Business Layer (ví dụ: không xóa được do FK)
            {
                // Lấy lại thông tin tỉnh để hiển thị lại view Delete với thông báo lỗi
                var province = await _provinceBusiness.GetProvinceByIdAsync(id);
                if (province == null) return NotFound(); // Trường hợp hi hữu: tỉnh bị xóa ngay sau khi GET Delete

                ModelState.AddModelError("", ex.Message); // Hiển thị lỗi nghiệp vụ
                return View("Delete", province); // Chỉ định rõ View là "Delete"
            }
            catch (KeyNotFoundException) // Bắt lỗi không tìm thấy từ Business/Service
            {
                // Tỉnh đã bị xóa bởi người khác trước khi kịp nhấn nút xác nhận
                // Lưu thông báo cảnh báo vào TempData thay vì dùng .WithWarning
                TempData["UserMessage"] = "Tỉnh này đã được xóa trước đó.";
                TempData["MessageType"] = "warning"; // Đặt loại là warning

                return RedirectToAction(nameof(Index)); // Chỉ cần Redirect                                                                                                                   // Hoặc đơn giản là RedirectToAction(nameof(Index));
            }
            catch (Exception ex) // Bắt các lỗi khác
            {
                // Log lỗi
                // Lấy lại thông tin tỉnh để hiển thị lại view Delete với thông báo lỗi
                var province = await _provinceBusiness.GetProvinceByIdAsync(id);
                if (province == null) return NotFound();

                ModelState.AddModelError("", $"Đã xảy ra lỗi không mong muốn: {ex.Message}");
                return View("Delete", province);
            }
        }

        /* // Hàm kiểm tra tồn tại này không cần thiết nữa vì đã có trong Service/Business
        private bool ProvinceExists(int id)
        {
          // return _context.Provinces.Any(e => e.Id == id);
          // Nên gọi qua Business/Service nếu cần kiểm tra ở Controller
        }
        */
    }
}