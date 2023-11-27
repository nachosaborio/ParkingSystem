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
            Cache.AddEmpleado(value);
            return Ok(value);
        }

        [HttpGet("GetAllEmpleados")]
        public ActionResult<IEnumerable<Empleado>> GetAllEmpleados()
        {
            return Ok(_miBD.Empleados.ToList());
        }

        [HttpGet("GetEmpleadoXId/{id}")]
        public ActionResult<Empleado> GetEmpleadoXId(int id) { 
            return Ok(Cache.GetEmpleadoXId(id));
        }

        [HttpPost("UpdateEmpleado")]
        public ActionResult UpdateEmpleado([FromBody] Empleado value)
        {
            Cache.UpdateEmpleado(value);
            return Ok(value);
        }

        [HttpDelete("DeleteEmpleado/{id}")]
        public ActionResult DeleteEmpleado(int id)
        {
            Cache.DeleteEmpleado(id);
            return Ok();
        }
    }
}
