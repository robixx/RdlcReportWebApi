using System.ComponentModel.DataAnnotations;


namespace RdlcReportWebApi.Models
{
    public class Employess
    {
        [Key]
        public int EmpId { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public int Port { get; set; }
        public string? Mobile { get; set; }
    }
}
