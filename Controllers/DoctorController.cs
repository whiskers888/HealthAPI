using HealthAPI.Context;
using HealthAPI.Controllers.DTO;
using HealthAPI.Replicates;
using Microsoft.AspNetCore.Mvc;

namespace HealthAPI.Controllers
{
    public class DoctorController:BaseController
    {
        public DoctorController(ApplicationContext _appContext):base(_appContext) { }

        [HttpGet("[controller]/[action]")]
        public JsonResult Init()
        {
            var res = GetCommon();
            return Send(true, res);
        }

        [HttpGet("[controller]/[action]")]
        public JsonResult GetAll()
        {
            var res = GetCommon();
            res.doctors = ApplicationContext.DoctorsManager.Doctors.Select(it => new DoctorModel(it));
            return Send(true, res);
        }


        [HttpGet("[controller]/[action]")]
        public JsonResult Get(int id)
        {
            var res = GetCommon();
            res.doctors = new DoctorModel(ApplicationContext.DoctorsManager.Doctors.FirstOrDefault(it => it.Id == id));
            return Send(true, res);
        }

        [HttpPost("[controller]/[action]")]
        public JsonResult Create([FromBody] DoctorModel model)
        {
            Doctor doctor = ApplicationContext.DoctorsManager.Create(model);

            var res = GetCommon();
            res.doctors = new DoctorModel(doctor);
            return Send(true, res);
        }

        [HttpPut("[controller]/[action]")]
        public JsonResult Update([FromBody] DoctorModel model)
        {

            Doctor doctor = ApplicationContext.DoctorsManager.Update(model);

            var res = GetCommon();
            res.doctors = new DoctorModel(doctor);
            return Send(true, res);
        }

        [HttpGet("[controller]/[action]")]
        public JsonResult GetPatients(int doctorId)
        {

            Patient[] patients = ApplicationContext.DoctorsManager.GetPatients(doctorId);

            var res = GetCommon();
            res.patients = patients.Select(it => new PatientModel(it));
            return Send(true, res);
        }
        [HttpPost("[controller]/[action]")]
        public JsonResult AttachPatient(int doctorId, int patientId)
        {

            Patient[] patients = ApplicationContext.DoctorsManager.AttachPatient(doctorId, patientId);

            var res = GetCommon();
            res.patients = patients.Select(it => new PatientModel(it));
            return Send(true, res);
        }
        [HttpPost("[controller]/[action]")]
        public JsonResult DettachPatient(int doctorId, int patientId)
        {

            Patient[] patients = ApplicationContext.DoctorsManager.DettachPatient(doctorId, patientId);

            var res = GetCommon();
            res.patients = patients.Select(it => new PatientModel(it));
            return Send(true, res);
        }

        [HttpDelete("[controller]/[action]")]
        public JsonResult Delete(int id)
        {
            ApplicationContext.DoctorsManager.Delete(id);
            var res = GetCommon();
            res.doctors = ApplicationContext.DoctorsManager.Doctors.Select(it => new DoctorModel(it));
            return Send(true, res);
        }
    }
}
