using DATN01.Models;
using System.Text.Json.Serialization;

namespace DATN01.Areas.Admin.Models
{
    public class RegisterModel
    {
        [JsonPropertyName("Employee")]
        public required EmployeeModel Employee { get; set; }
        [JsonPropertyName("Account")]
        public required AccountModel Account {  get; set; }
    }
}
