using Microsoft.AspNetCore.Mvc;
using Proyecto1.Models;
using Proyecto3API;

namespace Proyecto2API.Controllers
{
    [Route("api/Parqueo")]
    [ApiController]
    public class ParqueoController : ControllerBase
    {
        private Contexto _miBD;

        public ParqueoController(Contexto miBD)
        {
            _miBD = miBD;
        }

        [HttpPost("AddParqueo")]
        public ActionResult AddParqueo([FromBody] Parqueo value)
        {
            _miBD.Add(value);
            _miBD.SaveChanges();
            return Ok(value);
        }

        [HttpGet("GetAllParqueos")]
        public ActionResult<IEnumerable<Parqueo>> GetAllParqueos()
        {
            return Ok(_miBD.Parqueos.ToList());
        }

        [HttpGet("GetParqueoXId/{id}")]
        public ActionResult<Parqueo> GetParqueoXId(int id) {
            Parqueo parqueo;
            parqueo = _miBD.Parqueos.Where(x => x.Id == id).FirstOrDefault();
            return Ok(parqueo);
        }

        [HttpPost("UpdateParqueo")]
        public ActionResult UpdateParqueo([FromBody] Parqueo value)
        {
            _miBD.Update(value);
            _miBD.SaveChanges();
            return Ok(value);
        }

        [HttpDelete("DeleteParqueo/{id}")]
        public ActionResult DeleteParqueo(int id)
        {
            Parqueo parqueo;
            parqueo = _miBD.Parqueos.Where(x => x.Id == id).FirstOrDefault();
            _miBD.Remove(parqueo);
            _miBD.SaveChanges();
            return Ok();
        }
    }
}
