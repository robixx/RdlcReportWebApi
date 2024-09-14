using System.ComponentModel.DataAnnotations;

namespace RdlcReportWebApi.Models
{
    public class EmpCode
    {
        [Key]
        public int Id { get; set; }
        public int EmpId { get; set; }
        public string SecreateKey { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}
