using Microsoft.EntityFrameworkCore;
using RdlcReportWebApi.Models;

namespace RdlcReportWebApi.Data
{
    public class DataBaseConnection : DbContext
    {
        public DataBaseConnection(DbContextOptions<DataBaseConnection> options) : base(options)
        {
        }

        public DbSet<Employess> Employess { get; set; }
        public DbSet<EmpCode> EmpCode { get; set; }
    }
}
