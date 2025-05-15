using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace StaffMgmt.Models.Dtos
{
    public class DistrictNestedDto
    {
        [JsonPropertyName("Code")]
        public string? Code { get; set; }

        [JsonPropertyName("Name")]
        public string? Name { get; set; }

        [JsonPropertyName("FullName")]
        public string? FullName { get; set; } // Có thể dùng tên này

        [JsonPropertyName("ProvinceCode")]
        public string? ProvinceCode { get; set; }

        [JsonPropertyName("Ward")] // Tên thuộc tính trong JSON
        public List<WardNestedDto>? Wards { get; set; }
    }
}