using Microsoft.AspNetCore.Mvc;
using Proyecto1.Models;
using Proyecto3API;

namespace Proyecto2API.Controllers
{
    [Route("api/Empleado")]
    [ApiController]
    public class EmpleadoController : ControllerBase
    {
        private Contexto _miBD;

        public EmpleadoController(Contexto miBD)
        {
            _miBD = miBD;
        }

        [HttpPost("AddEmpleado")]
        public ActionResult AddEmpleado([FromBody] Empleado value)
        {
            _miBD.Add(value);
            _miBD.SaveChanges();
            return Ok(value);
        }

        [HttpGet("GetAllEmpleados")]
        public ActionResult<IEnumerable<Empleado>> GetAllEmpleados()
        {
            return Ok(_miBD.Empleados.ToList());
        }

        [HttpGet("GetEmpleadoXId/{id}")]
        public ActionResult<Empleado> GetEmpleadoXId(int id) {
            Empleado empleado;
            empleado = _miBD.Empleados.Where(x => x.Id == id).FirstOrDefault();
            return Ok(empleado);
        }

        [HttpPost("UpdateEmpleado")]
        public ActionResult UpdateEmpleado([FromBody] Empleado value)
        {
            _miBD.Update(value);
            _miBD.SaveChanges();
            return Ok(value);
        }

        [HttpDelete("DeleteEmpleado/{id}")]
        public ActionResult DeleteEmpleado(int id)
        {
            Empleado empleado;
            empleado = _miBD.Empleados.Where(x => x.Id == id).FirstOrDefault();
            _miBD.Remove(empleado);
            _miBD.SaveChanges();
            return Ok();
        }
    }
}
