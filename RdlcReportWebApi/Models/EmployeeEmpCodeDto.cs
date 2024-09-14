namespace RdlcReportWebApi.Models
{
    public class EmployeeEmpCodeDto
    {
        public int EmpId { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public int Port { get; set; }
        public string? Mobile { get; set; }
        public string SecreateKey { get; set; } = string.Empty;

    }
}
