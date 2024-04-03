using HealthAPI.Models;

namespace HealthAPI.Replicates
{
    public class Doctor
    {
        private EFDoctor Context { get; set; }
        public Doctor(EFDoctor context) { Context = context; }
        public int Id { get => Context.Id; }
        public string Name { get => Context.Name; set => Context.Name = value; }

        public string Description { get => Context.Description; set => Context.Description = value; }
        public int StartDateWork { get => Context.StartDateWork; set => Context.StartDateWork = value; }
        public List<Patient> Patients { 
            get => Context.EFPatients.Select(it => new Patient(it)).ToList();
        }
    }
}
