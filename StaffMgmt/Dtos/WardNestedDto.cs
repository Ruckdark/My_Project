using System.Text.Json.Serialization;

namespace StaffMgmt.Models.Dtos
{
    public class WardNestedDto
    {
        [JsonPropertyName("Code")]
        public string? Code { get; set; }

        [JsonPropertyName("Name")]
        public string? Name { get; set; }

        [JsonPropertyName("FullName")]
        public string? FullName { get; set; } // Có thể dùng tên này cho chuẩn

        [JsonPropertyName("DistrictCode")]
        public string? DistrictCode { get; set; }
    }
}