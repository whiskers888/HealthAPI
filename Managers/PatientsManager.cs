using HealthAPI.Context;
using HealthAPI.Controllers.DTO;
using HealthAPI.Models;
using HealthAPI.Replicates;

namespace HealthAPI.Managers
{
    public class PatientsManager
    {

        protected ApplicationContext ApplicationContext { get; set; }
        protected DBContext DBContext { get; set; }
        public PatientsManager(ApplicationContext applicationContext) { ApplicationContext = applicationContext; DBContext = applicationContext.CreateDbContext(); }

        private List<Patient> _patients { get; set; } = new List<Patient>();

        public Patient[] Patients { get => _patients.ToArray(); }
        public bool Read()
        {
            try
            {
                foreach (EFPatient item in DBContext.Patients)
                {
                    if (item.IsDeleted != true) _patients.Add(new Patient(item));
                }
                return true;
            }
            catch { throw; }
        }

        public Patient Get(int id) => _patients.FirstOrDefault(it => it.Id == id);

        public Patient Create(PatientModel model)
        {
            try
            {
                EFPatient patient = new EFPatient()
                {
                    Name = model.name,
                    DateBirthday = model.dateBirthday,
                    Address = model.address,
                };
                DBContext.Add(patient);
                DBContext.SaveChanges();

                Patient replicate = new Patient(patient);
                _patients.Add(replicate);

                return replicate;
            }
            catch { throw; }
        }

        public Patient Update(PatientModel model)
        {
            try
            {

                EFPatient _patient = DBContext.Patients.FirstOrDefault(it => it.Id == model.id);


                _patient.Name = model.name;
                _patient.Address = model.address;
                _patient.DateBirthday = model.dateBirthday;

                DBContext.Update(_patient);

                _patients.Remove(_patients.FirstOrDefault(it => it.Id == model.id));
                Patient repl = new Patient(_patient);
                _patients.Add(repl);

                return repl;
            }
            catch { throw; }
        }

        public bool Delete(int id)
        {
            try
            {

                EFPatient _patient = DBContext.Patients.FirstOrDefault(it => it.Id == id);
                _patient.IsDeleted = true;
                DBContext.Update(_patient);

                _patients.Remove(_patients.FirstOrDefault(it => it.Id == id));
                return true;

            }
            catch { throw; }
        }
    }
}
