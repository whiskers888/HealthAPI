using HealthAPI.Context;
using HealthAPI.Controllers.DTO;
using HealthAPI.Models;
using HealthAPI.Replicates;
using Microsoft.EntityFrameworkCore;

namespace HealthAPI.Managers
{
    public class DoctorsManager
    {

        protected ApplicationContext ApplicationContext { get; set; }
        protected DBContext DBContext { get; set; }
        public DoctorsManager (ApplicationContext applicationContext) { ApplicationContext = applicationContext;  DBContext = applicationContext.CreateDbContext(); }

        private List<Doctor> _doctors { get; set; } = new List<Doctor> ();

        public Doctor[] Doctors { get => _doctors.ToArray (); }
        public bool Read()
        {
            DBContext.Doctors.Include(it => it.EFPatients).ToList();
            try
            {
                foreach(EFDoctor item in DBContext.Doctors)
                {
                    if(item.IsDeleted != true) _doctors.Add(new Doctor(item));
                }
                return true;
            }
            catch { throw; }
        }

        public Doctor Get(int id) => _doctors.FirstOrDefault(it => it.Id == id);

        public Doctor Create(DoctorModel model)
        {
            try
            {
                EFDoctor doctor = new EFDoctor()
                {
                    Name = model.name,
                    Description = model.description,
                    StartDateWork = model.startDateWork,
                };
                DBContext.Add(doctor);
                DBContext.SaveChanges();

                Doctor replicate = new Doctor(doctor);
                _doctors.Add(replicate);

                return replicate;
            }
            catch { throw; }
        }

        public Doctor Update(DoctorModel model)
        {
            try
            {

                EFDoctor _doctor = DBContext.Doctors.FirstOrDefault(it => it.Id == model.id);


                _doctor.Name = model.name;
                _doctor.Description = model.description;
                _doctor.StartDateWork = model.startDateWork;

                DBContext.Update(_doctor);

                _doctors.Remove(_doctors.FirstOrDefault(it => it.Id == model.id));
                Doctor repl = new Doctor(_doctor);
                _doctors.Add(repl);

                return repl;
            }
            catch { throw; }
        }

        public Patient[] GetPatients(int doctorId)
        {
            return Get(doctorId).Patients.ToArray();
        }

        public Patient[] AttachPatient(int doctorId, int patientId)
        {
            var patient = ApplicationContext.PatientsManager.Get(patientId);

            var _doctor = DBContext.Doctors.FirstOrDefault(it => it.Id == doctorId);
            _doctor.EFPatients.Add(patient.Context);

            DBContext.Update(_doctor);
            DBContext.SaveChanges();

            var doctor = Get(doctorId);
            doctor.Patients.Add(patient);

            return GetPatients(doctorId);
        }

        public Patient[] DettachPatient(int doctorId, int patientId)
        {
            var patient = ApplicationContext.PatientsManager.Get(patientId);

            var _doctor = DBContext.Doctors.FirstOrDefault(it => it.Id == doctorId);
            _doctor.EFPatients.Remove(patient.Context);

            DBContext.Update(_doctor);
            DBContext.SaveChanges();


            var doctor = Get(doctorId);
            doctor.Patients.Remove(patient);

            return GetPatients(doctorId);
        }

        public bool Delete(int id)
        {
            try
            {

                EFDoctor _doctor = DBContext.Doctors.FirstOrDefault(it => it.Id == id);
                _doctor.IsDeleted = true;
                DBContext.Update(_doctor);

                _doctors.Remove(_doctors.FirstOrDefault(it => it.Id == id));
                return true;

            }
            catch { throw; }



        }

    }
}
