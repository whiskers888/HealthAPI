namespace HealthAPI.Models
{
    public class EFDoctor:EFBaseModel
    {
/*
        public EFDoctor() { }*/

        public string Name { get; set; }
        public string Description { get; set; }
        public int StartDateWork { get; set; }
        public List<EFPatient> EFPatients { get; set; } = new List<EFPatient>();
    }
}
