using HealthAPI.Context;
using HealthAPI.Controllers.DTO;
using HealthAPI.Replicates;
using Microsoft.AspNetCore.Mvc;

namespace HealthAPI.Controllers
{
    public class PatientController:BaseController
    {
        public PatientController(ApplicationContext _appContext):base(_appContext) { }


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
            res.patient = ApplicationContext.PatientsManager.Patients.Select(it => new PatientModel(it));
            return Send(true, res);
        }


        [HttpGet("[controller]/[action]")]
        public JsonResult Get(int id)
        {
            var res = GetCommon();
            res.patients = new PatientModel(ApplicationContext.PatientsManager.Patients.FirstOrDefault(it => it.Id == id));
            return Send(true, res);
        }

        [HttpPost("[controller]/[action]")]
        public JsonResult Create([FromBody] PatientModel model)
        {
            Patient doctor = ApplicationContext.PatientsManager.Create(model);

            var res = GetCommon();
            res.patients = new PatientModel(doctor);
            return Send(true, res);
        }

        [HttpPut("[controller]/[action]")]
        public JsonResult Update([FromBody] PatientModel model)
        {

            Patient doctor = ApplicationContext.PatientsManager.Update(model);

            var res = GetCommon();
            res.patients = new PatientModel(doctor);
            return Send(true, res);
        }

        [HttpDelete("[controller]/[action]")]
        public JsonResult Delete(int id)
        {
            ApplicationContext.PatientsManager.Delete(id);
            var res = GetCommon();
            res.patients = ApplicationContext.PatientsManager.Patients.Select(it => new PatientModel(it));
            return Send(true, res);
        }
    }
}
