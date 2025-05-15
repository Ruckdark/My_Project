// File: Program.cs

// Thêm các using directive cần thiết ở đầu file
using Microsoft.EntityFrameworkCore;
using StaffMgmt.Data;
using StaffMgmt.Interfaces; // using cho Interfaces
using StaffMgmt.Services;   // using cho Services
using StaffMgmt.Business;   // using cho Business
// Thêm using cho Logger nếu chưa có
using Microsoft.Extensions.Logging;
using System.IO; // Cần cho Path.Combine
using System;   // Cần cho AppDomain

using FluentValidation;
using StaffMgmt.Validators;
using StaffMgmt.Models;

Console.OutputEncoding = System.Text.Encoding.UTF8;

var builder = WebApplication.CreateBuilder(args);

#region Configure Services
// --- 1. Lấy chuỗi kết nối ---
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
if (string.IsNullOrEmpty(connectionString))
{
    // Ném lỗi hoặc xử lý nếu không tìm thấy chuỗi kết nối
    throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
}


// --- 2. Đăng ký DbContext (Chỉ một lần và đã sửa) ---
// Đăng ký DbContext với cấu hình SQL Server và vô hiệu hóa batching
builder.Services.AddDbContext<StaffMgmtDbContext>(options =>
    options.UseSqlServer(connectionString, sqlServerOptionsAction: sqlOptions =>
    {
        // Thiết lập MaxBatchSize = 1 để vô hiệu hóa batching
        // Buộc EF Core gửi từng lệnh INSERT riêng lẻ (quan trọng cho seeding)
        sqlOptions.MaxBatchSize(1);
    }));


// --- 3. Đăng ký các lớp Service và Business với DI Container ---
// Đăng ký các dịch vụ với vòng đời Scoped (một instance cho mỗi HTTP request)
builder.Services.AddScoped<IProvinceService, ProvinceService>();
builder.Services.AddScoped<IProvinceBusiness, ProvinceBusiness>();
builder.Services.AddScoped<IDistrictService, DistrictService>();
builder.Services.AddScoped<IDistrictBusiness, DistrictBusiness>();
builder.Services.AddScoped<IWardService, WardService>();
builder.Services.AddScoped<IWardBusiness, WardBusiness>();
builder.Services.AddScoped<IDataSeedingService, DataSeedingService>();
builder.Services.AddScoped<IEthnicityService, EthnicityService>();
builder.Services.AddScoped<IEthnicityBusiness, EthnicityBusiness>();
builder.Services.AddScoped<IOccupationService, OccupationService>();
builder.Services.AddScoped<IOccupationBusiness, OccupationBusiness>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IEmployeeBusiness, EmployeeBusiness>();
builder.Services.AddScoped<IValidator<EmployeeFormViewModel>, EmployeeFormViewModelValidator>();

// TODO: Thêm các đăng ký cho Employee, Certificate... sau này


// --- 4. Đăng ký các dịch vụ MVC ---
// AddControllersWithViews() đăng ký các dịch vụ cần thiết cho MVC controllers và views.
builder.Services.AddControllersWithViews();

#endregion Configure Services


var app = builder.Build(); // Xây dựng ứng dụng từ các cấu hình services


#region Configure HTTP Request Pipeline
// Cấu hình pipeline xử lý các HTTP request đến

// --- Cấu hình cho môi trường Development ---
if (app.Environment.IsDevelopment())
{
    // Sử dụng trang lỗi chi tiết cho developer
    app.UseDeveloperExceptionPage(); // Hoặc UseExceptionHandler tùy theo phiên bản .NET
}
// --- Cấu hình cho môi trường Production ---
else
{
    // Sử dụng trang lỗi chung cho người dùng cuối
    app.UseExceptionHandler("/Home/Error"); // Chuyển hướng đến action Error của HomeController khi có lỗi không bắt được

    // Sử dụng HSTS (HTTP Strict Transport Security) để bắt buộc HTTPS
    app.UseHsts();
}

// --- Các Middleware quan trọng khác ---
app.UseHttpsRedirection();
app.UseStaticFiles(); // Cho phép phục vụ các file tĩnh
app.UseRouting(); // Bật cơ chế định tuyến
app.UseAuthorization(); // Bật cơ chế phân quyền


// --- Định nghĩa các Endpoint (Routes) ---
// Định nghĩa route mặc định cho MVC
app.MapControllerRoute(
    name: "default", // Tên của route
    pattern: "{controller=Employee}/{action=Create}/{id?}"); // Route mặc định: EmployeeController, action Create

#endregion Configure HTTP Request Pipeline


#region Data Seeding (Chạy một lần khi cần)
// --- LOG NGAY TRƯỚC KHI VÀO KHỐI SEEDING ---
var initialLogger = app.Services.GetRequiredService<ILogger<Program>>();
initialLogger.LogWarning(">>> CHUẨN BỊ VÀO KHỐI DATA SEEDING <<<");

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var logger = services.GetRequiredService<ILogger<Program>>();
    var dbContext = services.GetRequiredService<StaffMgmtDbContext>();
    var seeder = services.GetRequiredService<IDataSeedingService>();

    logger.LogInformation(">>> ĐÃ VÀO USING SCOPE - Bắt đầu kiểm tra trạng thái dữ liệu...");

    try
    {
        bool hasDistricts = false;
        bool hasWards = false;

        // --- LOG TRƯỚC VÀ SAU KHI KIỂM TRA Districts ---
        logger.LogInformation("Chuẩn bị gọi dbContext.Districts.AnyAsync()...");
        var districtCheckWatch = System.Diagnostics.Stopwatch.StartNew();
        try
        {
            hasDistricts = await dbContext.Districts.AnyAsync();
            districtCheckWatch.Stop();
            logger.LogInformation("Gọi dbContext.Districts.AnyAsync() thành công ({HasDistricts}) sau {ElapsedMilliseconds} ms.", hasDistricts, districtCheckWatch.ElapsedMilliseconds);
        }
        catch (Exception ex)
        {
            districtCheckWatch.Stop();
            logger.LogError(ex, "Lỗi khi gọi dbContext.Districts.AnyAsync() sau {ElapsedMilliseconds} ms.", districtCheckWatch.ElapsedMilliseconds);
            // Có thể throw lại lỗi hoặc bỏ qua seeding tùy logic
            throw; // Ném lại lỗi để dừng nếu không query được DB
        }

        // --- LOG TRƯỚC VÀ SAU KHI KIỂM TRA Wards ---
        logger.LogInformation("Chuẩn bị gọi dbContext.Wards.AnyAsync()...");
        var wardCheckWatch = System.Diagnostics.Stopwatch.StartNew();
        try
        {
            hasWards = await dbContext.Wards.AnyAsync();
            wardCheckWatch.Stop();
            logger.LogInformation("Gọi dbContext.Wards.AnyAsync() thành công ({HasWards}) sau {ElapsedMilliseconds} ms.", hasWards, wardCheckWatch.ElapsedMilliseconds);
        }
        catch (Exception ex)
        {
            wardCheckWatch.Stop();
            logger.LogError(ex, "Lỗi khi gọi dbContext.Wards.AnyAsync() sau {ElapsedMilliseconds} ms.", wardCheckWatch.ElapsedMilliseconds);
            throw; // Ném lại lỗi
        }


        if (!hasDistricts || !hasWards)
        {
            logger.LogWarning("Phát hiện thiếu dữ liệu Quận/Huyện hoặc Xã/Phường. Chuẩn bị gọi seeder...");

            string jsonFileName = "simplified_json_generated_data_vn_units.json";
            string baseDirectory = AppContext.BaseDirectory;
            string jsonFilePath = Path.Combine(baseDirectory, "DataSeed", jsonFileName);

            logger.LogInformation("Đường dẫn file JSON: {FilePath}", jsonFilePath);

            // --- LOG NGAY TRƯỚC KHI GỌI SEEDER ---
            logger.LogInformation("Chuẩn bị gọi seeder.SeedAllAdministrativeUnitsAsync()...");
            await seeder.SeedAllAdministrativeUnitsAsync(jsonFilePath); // Gọi service seeding
            logger.LogInformation("Gọi seeder.SeedAllAdministrativeUnitsAsync() đã hoàn tất (hoặc đã thoát).");
        }
        else
        {
            logger.LogInformation("Database đã có đủ dữ liệu Quận/Huyện và Xã/Phường. Bỏ qua bước seeding.");
        }
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "Đã xảy ra lỗi nghiêm trọng trong khối using scope của Data Seeding.");
        // throw; // Cân nhắc ném lại lỗi
    }
    finally
    {
        logger.LogInformation(">>> KẾT THÚC KHỐI USING SCOPE DATA SEEDING <<<");
    }
}
#endregion Data Seeding (Chạy một lần khi cần)

initialLogger.LogInformation(">>> CHUẨN BỊ GỌI app.Run() <<<");
// Dòng cuối cùng: Chạy ứng dụng web
app.Run();
