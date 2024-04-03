using HealthAPI.Context;
using HealthAPI.Replicates;

namespace HealthAPI.Controllers.DTO
{
    public class DoctorModel
    {
        public DoctorModel() { }
        public DoctorModel(Doctor context)
        {
            id = context.Id;
            name = context.Name;
            description = context.Description;
            startDateWork = context.StartDateWork;
            patients = context.Patients.Select(it => new PatientModel(it)).ToArray();
        }

        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int startDateWork { get; set; }
        public PatientModel[] patients { get; set; }
    }
}
