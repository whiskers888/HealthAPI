using HealthAPI.Models;
using Microsoft.EntityFrameworkCore;
namespace HealthAPI.Context
{
    public class DBContext : DbContext
    {

        /*Перечисление моделей*/
        public DbSet<EFDoctor> Doctors { get; set; }
        public DbSet<EFPatient> Patients { get; set; }
        public DBContext(string cnnString)
        {
            ConnectionString = cnnString;
            /*Database.EnsureDeleted();*/
            Database.EnsureCreated();
        }

        public string ConnectionString { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionString);
        }
    }
}
