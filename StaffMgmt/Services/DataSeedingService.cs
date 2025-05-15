#region Using Directives
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StaffMgmt.Data;
using StaffMgmt.Models;
using StaffMgmt.Models.Dtos; // Sử dụng DTO mới
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using StaffMgmt.Interfaces;
#endregion

namespace StaffMgmt.Services
{
    

    #region Implementation
    public class DataSeedingService : IDataSeedingService
    {
        private readonly StaffMgmtDbContext _context;
        private readonly ILogger<DataSeedingService> _logger;
        private const int WardBatchSize = 200;

        // --- Thêm JsonSerializerOptions để tái sử dụng ---
        private static readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true // Cho phép không phân biệt hoa thường khi parse
        };

        public DataSeedingService(StaffMgmtDbContext context, ILogger<DataSeedingService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task SeedAllAdministrativeUnitsAsync(string jsonFilePath)
        {
            _logger.LogInformation("--- BẮT ĐẦU SEEDING ĐƠN VỊ HÀNH CHÍNH (STREAMING) ---");
            var stopwatch = Stopwatch.StartNew();

            _logger.LogInformation("Kiểm tra sự tồn tại của file: {FilePath}", jsonFilePath);
            if (!File.Exists(jsonFilePath)) { _logger.LogError("File không tồn tại: {FilePath}", jsonFilePath); return; }

            try
            {
                // --- Bước 1: Lấy dữ liệu hiện có từ DB (vẫn cần làm trước) ---
                _logger.LogInformation("Bước 1: Đang lấy dữ liệu Tỉnh/Huyện/Xã hiện có từ DB...");
                var stepWatch = Stopwatch.StartNew();
                var existingProvinces = await _context.Provinces.Where(p => p.Code != null).ToDictionaryAsync(p => p.Code!, p => p);
                var existingDistricts = await _context.Districts.Where(d => d.Code != null).ToDictionaryAsync(d => d.Code!, d => d);
                var existingWardCodes = await _context.Wards.Where(w => w.Code != null).Select(w => w.Code!).ToHashSetAsync();
                stepWatch.Stop();
                _logger.LogInformation("Bước 1: Lấy dữ liệu hiện có hoàn tất sau {ElapsedMilliseconds} ms (Provinces: {PCount}, Districts: {DCount}, Ward Codes: {WCount}).",
                    stepWatch.ElapsedMilliseconds, existingProvinces.Count, existingDistricts.Count, existingWardCodes.Count);


                // --- Bước 2: Mở Stream và Xử lý JSON cuốn chiếu ---
                _logger.LogInformation("Bước 2: Mở file JSON stream và bắt đầu xử lý theo từng Tỉnh...");
                stepWatch.Restart();

                // Tạo các list để chứa dữ liệu mới chờ lưu
                var districtsToAdd = new List<District>();
                var wardsBatch = new List<Ward>(); // Batch chung cho xã

                // Biến đếm tổng kết
                int totalProvincesProcessed = 0;
                int totalDistrictsAdded = 0;
                int totalWardsAdded = 0;
                int skippedProvinces = 0;
                int skippedDistricts = 0;
                int skippedWards = 0;

                // Mở file stream để đọc
                await using FileStream openStream = File.OpenRead(jsonFilePath);

                // Sử dụng DeserializeAsyncEnumerable để đọc từng đối tượng Province từ stream
                await foreach (var provinceDto in JsonSerializer.DeserializeAsyncEnumerable<ProvinceNestedDto>(openStream, _jsonOptions))
                {
                    // Kiểm tra DTO tỉnh đọc được từ stream
                    if (provinceDto == null || string.IsNullOrEmpty(provinceDto.Code) || string.IsNullOrEmpty(provinceDto.FullName))
                    {
                        skippedProvinces++; continue;
                    }

                    // Tìm tỉnh trong DB
                    if (!existingProvinces.TryGetValue(provinceDto.Code, out var currentProvince))
                    {
                        _logger.LogWarning("Bỏ qua Tỉnh Code={Code} không có trong DB.", provinceDto.Code);
                        skippedProvinces++; continue;
                    }
                    totalProvincesProcessed++;
                    _logger.LogDebug("Đang xử lý Tỉnh: {ProvinceName} (Code: {ProvinceCode})", currentProvince.Name, currentProvince.Code);

                    var districtsToAddInProvince = new List<District>(); // Huyện mới của tỉnh này
                    var districtsInProvinceMap = new Dictionary<string, District>(); // Map huyện của tỉnh này

                    // Chuẩn bị thêm Huyện cho tỉnh hiện tại
                    if (provinceDto.Districts != null)
                    {
                        foreach (var districtDto in provinceDto.Districts)
                        {
                            if (string.IsNullOrEmpty(districtDto.Code) || string.IsNullOrEmpty(districtDto.FullName) || districtDto.ProvinceCode != provinceDto.Code) { skippedDistricts++; continue; }

                            District? currentDistrict;
                            if (!existingDistricts.TryGetValue(districtDto.Code, out currentDistrict))
                            {
                                currentDistrict = new District { Code = districtDto.Code, Name = districtDto.FullName, ProvinceId = currentProvince.Id };
                                districtsToAddInProvince.Add(currentDistrict);
                                districtsInProvinceMap.Add(districtDto.Code, currentDistrict);
                            }
                            else
                            {
                                districtsInProvinceMap.Add(districtDto.Code, currentDistrict);
                            }
                        }
                    }

                    // Lưu các Huyện MỚI của tỉnh này vào DB NGAY LẬP TỨC
                    if (districtsToAddInProvince.Any())
                    {
                        _logger.LogDebug("-> Chuẩn bị thêm {Count} Huyện mới cho Tỉnh {ProvinceName}.", districtsToAddInProvince.Count, currentProvince.Name);
                        await _context.Districts.AddRangeAsync(districtsToAddInProvince);
                        try
                        {
                            int savedDistricts = await _context.SaveChangesAsync(); // Lưu huyện của tỉnh này
                            _logger.LogDebug("-> Đã lưu thành công {SavedCount} Huyện mới cho Tỉnh {ProvinceName}.", savedDistricts, currentProvince.Name);
                            totalDistrictsAdded += savedDistricts;
                            // Cập nhật ID và thêm vào dictionary chung existingDistricts
                            foreach (var addedDistrict in districtsToAddInProvince)
                            {
                                if (addedDistrict.Code != null)
                                {
                                    districtsInProvinceMap[addedDistrict.Code].Id = addedDistrict.Id; // Cập nhật ID vào map tạm
                                    existingDistricts.Add(addedDistrict.Code, addedDistrict); // Thêm vào map chung
                                }
                            }
                        }
                        catch (DbUpdateException dbEx) { _logger.LogError(dbEx, "Lỗi khi lưu Huyện cho Tỉnh {ProvinceName}. Inner: {InnerMessage}", currentProvince.Name, dbEx.InnerException?.Message); continue; } // Bỏ qua xử lý xã nếu lỗi lưu huyện
                    }

                    // Xử lý các Xã của tỉnh này (dựa vào districtsInProvinceMap đã có ID)
                    if (provinceDto.Districts != null)
                    {
                        foreach (var districtDto in provinceDto.Districts)
                        {
                            if (string.IsNullOrEmpty(districtDto.Code) || districtDto.Wards == null) continue;

                            // Lấy huyện từ map tạm của tỉnh (đã có ID chính xác)
                            if (districtsInProvinceMap.TryGetValue(districtDto.Code, out var parentDistrict) && parentDistrict.Id > 0)
                            {
                                foreach (var wardDto in districtDto.Wards)
                                {
                                    if (string.IsNullOrEmpty(wardDto.Code) || string.IsNullOrEmpty(wardDto.FullName) || wardDto.DistrictCode != districtDto.Code) { skippedWards++; continue; }

                                    if (wardDto.Code != null && !existingWardCodes.Contains(wardDto.Code))
                                    {
                                        var newWard = new Ward { Code = wardDto.Code, Name = wardDto.FullName, DistrictId = parentDistrict.Id };
                                        wardsBatch.Add(newWard);
                                        existingWardCodes.Add(newWard.Code);

                                        // Lưu batch xã khi đầy
                                        if (wardsBatch.Count >= WardBatchSize)
                                        {
                                            var batchTimer = Stopwatch.StartNew();
                                            _logger.LogDebug("-> Chuẩn bị lưu batch {BatchNum} gồm {Count} Xã/Phường...", (totalWardsAdded / WardBatchSize) + 1, wardsBatch.Count);
                                            await _context.Wards.AddRangeAsync(wardsBatch);
                                            try
                                            {
                                                int savedInBatch = await _context.SaveChangesAsync();
                                                batchTimer.Stop();
                                                _logger.LogDebug("-> Đã lưu thành công batch, {SavedCount} Xã/Phường được thêm sau {ElapsedMilliseconds} ms.", savedInBatch, batchTimer.ElapsedMilliseconds);
                                                totalWardsAdded += savedInBatch;
                                            }
                                            catch (DbUpdateException dbEx) { _logger.LogError(dbEx, "Lỗi khi lưu batch Xã/Phường. Inner: {InnerMessage}", dbEx.InnerException?.Message); }
                                            wardsBatch.Clear();
                                        }
                                    }
                                    else { skippedWards++; }
                                } // end foreach ward
                            }
                            else
                            {
                                _logger.LogWarning("Không tìm thấy Huyện cha (Code={DistrictCode}) trong map tạm hoặc chưa có ID khi xử lý xã.", districtDto.Code);
                                if (districtDto.Wards != null) skippedWards += districtDto.Wards.Count;
                            }
                        } // end foreach district
                    }
                    _logger.LogDebug("Hoàn tất xử lý Tỉnh: {ProvinceName}", currentProvince.Name);
                } // end foreach province (stream)
                stepWatch.Stop();
                _logger.LogInformation("Bước 2&3&4: Xử lý JSON stream và nhập liệu cuốn chiếu hoàn tất sau {ElapsedMilliseconds} ms.", stepWatch.ElapsedMilliseconds);


                // Lưu batch xã cuối cùng (nếu còn)
                if (wardsBatch.Any())
                {
                    _logger.LogInformation("Chuẩn bị lưu batch cuối cùng gồm {Count} Xã/Phường...", wardsBatch.Count);
                    // ... (code lưu batch cuối giữ nguyên) ...
                    await _context.Wards.AddRangeAsync(wardsBatch);
                    try
                    {
                        var batchTimer = Stopwatch.StartNew();
                        int savedInBatch = await _context.SaveChangesAsync();
                        batchTimer.Stop();
                        _logger.LogInformation("Đã lưu thành công batch cuối cùng, {SavedCount} Xã/Phường được thêm sau {ElapsedMilliseconds} ms.", savedInBatch, batchTimer.ElapsedMilliseconds);
                        totalWardsAdded += savedInBatch;
                    }
                    catch (DbUpdateException dbEx) { _logger.LogError(dbEx, "Lỗi khi lưu batch Xã/Phường cuối cùng. Inner: {InnerMessage}", dbEx.InnerException?.Message); }
                    wardsBatch.Clear();
                }

                stopwatch.Stop();
                _logger.LogInformation("--- TỔNG KẾT SEEDING ---");
                _logger.LogInformation("Tổng thời gian thực hiện: {TotalElapsedSeconds} giây.", stopwatch.Elapsed.TotalSeconds);
                _logger.LogInformation("Đã xử lý: {TotalProvincesProcessed} Tỉnh.", totalProvincesProcessed);
                _logger.LogInformation("Đã thêm mới: {TotalDistrictsAdded} Quận/Huyện.", totalDistrictsAdded);
                _logger.LogInformation("Đã thêm mới: {TotalWardsAdded} Xã/Phường.", totalWardsAdded);
                _logger.LogInformation("Đã bỏ qua: {TotalSkippedProvinces} Tỉnh, {TotalSkippedDistricts} Huyện, {TotalSkippedWards} Xã.", skippedProvinces, skippedDistricts, skippedWards);

            }
            catch (JsonException jsonEx) { _logger.LogError(jsonEx, "Lỗi parse file JSON: {FilePath}.", jsonFilePath); }
            catch (DbUpdateException dbEx) { _logger.LogError(dbEx, "Lỗi DbUpdateException. Inner: {InnerMessage}", dbEx.InnerException?.Message); }
            catch (Exception ex) { _logger.LogError(ex, "Lỗi không mong muốn khi nhập dữ liệu."); }
            finally { _logger.LogInformation("--- HOÀN TẤT SEEDING ĐƠN VỊ HÀNH CHÍNH ---"); }
        }
    }
    #endregion
}
