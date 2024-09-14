namespace RdlcReportWebApi.IReportServices
{
    public interface IReportService
    {
        byte[] GenerateReportAsync(string Report_Name, string reportType);
    }
}
