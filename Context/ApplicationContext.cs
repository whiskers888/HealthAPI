using HealthAPI.Managers;
using Microsoft.EntityFrameworkCore;

namespace HealthAPI.Context
{
    public class ApplicationContext
    {

        public ApplicationContext(IConfiguration config)
        {
            Version = "0.1";
            Title = "HealthAPI";
            Configuration = config;
            Initialize();
        }

        public void Initialize()
        {

            /*Инициализация менеджеров*/
            DoctorsManager = new DoctorsManager(this);
            PatientsManager = new PatientsManager(this);

            DoctorsManager.Read();
            PatientsManager.Read();

        }

        public DoctorsManager DoctorsManager { get; set; }
        public PatientsManager PatientsManager { get; set; }
        public string Version { get; set; }
        public string Title { get; set; }
        public IConfiguration Configuration { get; set; }

        public DBContext CreateDbContext() => new DBContext(Configuration.GetConnectionString("DefaultConnection"));

    }
}
