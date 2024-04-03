using HealthAPI.Replicates;

namespace HealthAPI.Controllers.DTO
{
    public class PatientModel
    {
        public PatientModel() { }
        public PatientModel(Patient context)
        {
            id = context.Id;
            name = context.Name;
            dateBirthday = context.DateBirthday;
            address = context.Address;
        }

        public int id { get; set; }
        public string name { get; set; }
        public int dateBirthday { get; set; }
        public string? address { get; set; }
    }
}
