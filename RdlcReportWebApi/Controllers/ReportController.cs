using Microsoft.AspNetCore.Mvc;
using Microsoft.Reporting.NETCore;
using RdlcReportWebApi.Common;
using RdlcReportWebApi.Data;
using RdlcReportWebApi.IReportServices;
using RdlcReportWebApi.Models;

namespace RdlcReportWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private IReportService _reportService;
        private readonly DataBaseConnection _connection;
        public ReportController(IReportService reportService, DataBaseConnection connection)
        {
            _reportService = reportService;
            _connection = connection;
        }

        [HttpGet]
        [Route("GetReport")]
        public ActionResult GetReport(string reportType)
        {

            string fileDirPath = Path.Combine(@"F:\ITC\RdlcReportWebApi\ReportWeb\bin\Debug\net6.0");
            string rdlcFilePath = Path.Combine(fileDirPath, "ReportFiles", "Report1.rdlc");

            // Initialize the LocalReport object and load the RDLC file
            LocalReport rpt = new LocalReport();
            rpt.LoadReportDefinition(new FileStream(rdlcFilePath, FileMode.Open, FileAccess.Read));

            // Prepare the data source
            List<UserDto> userList = new List<UserDto>
        {
            new UserDto { FirstName = "Shoriful", LastName = "Islam", Email = "jp@gm.com", Phone = "+976666661111" },
            new UserDto { FirstName = "Jhon", LastName = "Doue", Email = "jp2@gm.com", Phone = "+976666661111" },
            new UserDto { FirstName = "Jummy", LastName = "Daniyel", Email = "jp3@gm.com", Phone = "+976666661111" },
            new UserDto { FirstName = "Jhosep", LastName = "Pande", Email = "jp4@gm.com", Phone = "+976666661111" },
            new UserDto { FirstName = "Matheow", LastName = "Roy", Email = "jp5@gm.com", Phone = "+976666661111" }
        };

            rpt = rpt.SetRDLCReportDatasets(new Dictionary<string, object> { { "dsUsers", userList } });
            rpt.EnableExternalImages = true;

            // Render the report to a byte array
            string deviceInfo = "<DeviceInfo><OutputFormat>" + reportType + "</OutputFormat></DeviceInfo>";
            byte[] bytes = rpt.Render("PDF", deviceInfo);

            // Return the PDF as a file result
            return File(bytes, "application/pdf", getReportName("Report1", reportType));
        }


        [HttpGet]
        [Route("GetProcedureReport")]
        public ActionResult GetProcedureReport(string reportType)
        {

            string fileDirPath = Path.Combine(@"F:\ITC\RdlcReportWebApi\ReportWeb\bin\Debug\net6.0");
            string rdlcFilePath = Path.Combine(fileDirPath, "ReportFiles", "Report2.rdlc");

            // Initialize the LocalReport object and load the RDLC file
            LocalReport rpt = new LocalReport();
            rpt.LoadReportDefinition(new FileStream(rdlcFilePath, FileMode.Open, FileAccess.Read));

            // Prepare the data source
            var userList = (from e in _connection.Employess
                            join ec in _connection.EmpCode
                            on e.EmpId equals ec.EmpId
                            select new EmployeeEmpCodeDto
                            {
                                EmpId = e.EmpId,
                                Name = e.Name,
                                Address = e.Address,
                                Port = e.Port,
                                Mobile = e.Mobile,
                                SecreateKey = ec.SecreateKey,

                            }).ToList();


            rpt = rpt.SetRDLCReportDatasets(new Dictionary<string, object> { { "ds1", userList } });
            rpt.EnableExternalImages = true;
            rpt.SetParameters(new ReportParameter("Header", "RDLC Report Testing"));
            //Rpt1.SetParameters(new ReportParameter("comnam", company != null ? company.comnam : ""));//"/Images/Comp_Logo/3101.jpg"
            //Rpt1.SetParameters(new ReportParameter("comadd", company != null ? company.comadd1 : ""));

            //Rpt1.SetParameters(new ReportParameter("RptTitle", "User's Login Credentials"));
            //Rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(company != null ? company.comnam : "", username, printdate)));

            // Render the report to a byte array
            string deviceInfo = "<DeviceInfo><OutputFormat>" + reportType + "</OutputFormat></DeviceInfo>";
            byte[] bytes = rpt.Render("PDF", deviceInfo);

            // Return the PDF as a file result
            return File(bytes, "application/pdf", getReportName("Report1", reportType));
        }










        private string getReportName(string reportName, string reportType)
        {
            var outputFileName = reportName + ".pdf";

            switch (reportType.ToUpper())
            {
                default:
                case "PDF":
                    outputFileName = reportName + ".pdf";
                    break;
                case "XLS":
                    outputFileName = reportName + ".xls";
                    break;
                case "WORD":
                    outputFileName = reportName + ".doc";
                    break;
            }

            return outputFileName;
        }
    }
}
