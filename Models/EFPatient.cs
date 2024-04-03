namespace HealthAPI.Models
{
    public class EFPatient:EFBaseModel
    {
/*
        public EFPatient() { }*/
        public string Name { get; set; }
        public int DateBirthday { get; set; }
        public string? Address { get; set; }

        public List<EFDoctor> Doctors { get; set; } = new List<EFDoctor>();

    }
}
