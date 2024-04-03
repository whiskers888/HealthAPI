using HealthAPI.Models;

namespace HealthAPI.Replicates
{
    public class Patient
    {

        public EFPatient Context { get; set; }
        public Patient(EFPatient context) { Context = context; }

        public int Id { get => Context.Id; }
        public string Name { get => Context.Name; set => Context.Name = value; }
        public int DateBirthday { get => Context.DateBirthday; set => Context.DateBirthday = value; }
        public string? Address { get => Context.Address; set => Context.Address = value; }

        public List<Doctor> Doctors { get => Context.Doctors.Select(it => new Doctor(it)).ToList(); }
    }
}
