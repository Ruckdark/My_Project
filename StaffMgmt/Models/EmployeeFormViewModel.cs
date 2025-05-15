// File: Models/EmployeeFormViewModel.cs (hoặc ViewModels/EmployeeFormViewModel.cs)

#region Using Directives
using Microsoft.AspNetCore.Mvc.Rendering; // Cần cho SelectList, SelectListItem
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations; // Cần cho Data Annotations
#endregion

namespace StaffMgmt.Models // Hoặc StaffMgmt.ViewModels
{
    public class EmployeeFormViewModel
    {
        // --- Thông tin cơ bản của Employee ---

        // Id cần thiết cho việc Edit, không cần cho Create (nhưng vẫn nên có)
        public int Id { get; set; }

        [Required(ErrorMessage = "Họ tên là bắt buộc.")]
        [StringLength(150, ErrorMessage = "Họ tên không được vượt quá 150 ký tự.")]
        [Display(Name = "1. Họ tên")] // Tên hiển thị trên Label
        public string FullName { get; set; } = string.Empty;

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "2. Ngày sinh")]
        public DateTime? DateOfBirth { get; set; }

        // Trường Tuổi sẽ không nhập trực tiếp, bỏ qua trong ViewModel
        // Có thể thêm thuộc tính chỉ đọc để hiển thị nếu cần:
        // [Display(Name = "3. Tuổi")]
        // public int? Age => DateOfBirth.HasValue ? CalculateAge(DateOfBirth.Value) : null;
        // private int CalculateAge(DateTime dob) { ... } // Hàm tính tuổi

        [Display(Name = "4. Dân tộc")]
        public int? EthnicityId { get; set; } // Lưu Id của dân tộc được chọn

        [Display(Name = "5. Nghề nghiệp")]
        public int? OccupationId { get; set; } // Lưu Id của nghề nghiệp được chọn

        [StringLength(20, ErrorMessage = "Số CCCD không được vượt quá 20 ký tự.")]
        // TODO: Thêm validation phức tạp hơn (ví dụ: regex, kiểm tra trùng lặp ở Business)
        [Display(Name = "6. Căn cước công dân")]
        public string? IdentityCardNumber { get; set; }

        [Display(Name = "Không có CCCD")]
        public bool HasNoIdentityCard { get; set; } // Checkbox đi kèm CCCD

        [StringLength(15, ErrorMessage = "Số điện thoại không được vượt quá 15 ký tự.")]
        [Phone(ErrorMessage = "Định dạng số điện thoại không hợp lệ.")]
        // TODO: Thêm validation phức tạp hơn (ví dụ: regex Việt Nam, kiểm tra trùng lặp)
        [Display(Name = "7. Số điện thoại")]
        public string? PhoneNumber { get; set; }

        [Display(Name = "Không có SĐT")]
        public bool HasNoPhoneNumber { get; set; } // Checkbox đi kèm SĐT

        // --- Thông tin Địa chỉ ---
        [Display(Name = "Tỉnh / Thành phố")]
        [Required(ErrorMessage = "Vui lòng chọn Tỉnh/Thành phố.")] // Bắt buộc chọn tỉnh
        public int? ProvinceId { get; set; }

        [Display(Name = "Quận / Huyện")]
        [Required(ErrorMessage = "Vui lòng chọn Quận/Huyện.")] // Bắt buộc chọn huyện
        public int? DistrictId { get; set; }

        [Display(Name = "Xã / Phường")]
        [Required(ErrorMessage = "Vui lòng chọn Xã/Phường.")] // Bắt buộc chọn xã
        public int? WardId { get; set; }

        [StringLength(255, ErrorMessage = "Địa chỉ cụ thể không được vượt quá 255 ký tự.")]
        [Display(Name = "Cụ thể")]
        public string? StreetAddress { get; set; }

        // --- Dữ liệu cho Dropdown Lists ---
        // Các thuộc tính này sẽ được Controller chuẩn bị và truyền cho View
        // Không cần các attribute validation ở đây

        public IEnumerable<SelectListItem> EthnicityList { get; set; } = Enumerable.Empty<SelectListItem>();
        public IEnumerable<SelectListItem> OccupationList { get; set; } = Enumerable.Empty<SelectListItem>();
        public IEnumerable<SelectListItem> ProvinceList { get; set; } = Enumerable.Empty<SelectListItem>();
        // DistrictList và WardList sẽ được load bằng AJAX, không cần truyền từ Controller ban đầu
        // Nhưng có thể cần khi load lại form Edit hoặc sau validation lỗi
        public IEnumerable<SelectListItem> DistrictList { get; set; } = Enumerable.Empty<SelectListItem>();
        public IEnumerable<SelectListItem> WardList { get; set; } = Enumerable.Empty<SelectListItem>();

        // --- (Tùy chọn) Thêm các thuộc tính khác nếu cần ---
        // Ví dụ: Danh sách văn bằng nếu form này xử lý cả văn bằng
    }
}
