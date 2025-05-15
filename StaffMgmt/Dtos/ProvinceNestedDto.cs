using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace StaffMgmt.Models.Dtos
{
    public class ProvinceNestedDto
    {
        [JsonPropertyName("Code")]
        public string? Code { get; set; }

        [JsonPropertyName("Name")]
        public string? Name { get; set; }

        [JsonPropertyName("FullName")]
        public string? FullName { get; set; } // Có thể dùng tên này

        [JsonPropertyName("District")] // Tên thuộc tính trong JSON
        public List<DistrictNestedDto>? Districts { get; set; }
    }
}