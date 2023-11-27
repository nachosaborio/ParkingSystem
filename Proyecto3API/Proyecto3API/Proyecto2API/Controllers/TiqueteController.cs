using Microsoft.AspNetCore.Mvc;
using Proyecto1.Models;
using Proyecto3API;

namespace Proyecto2API.Controllers
{
    [Route("api/Tiquete")]
    [ApiController]
    public class TiqueteController : ControllerBase
    {
        private Contexto _miBD;

        public TiqueteController(Contexto miBD)
        {
            _miBD = miBD;
        }

        [HttpPost("AddTiquete")]
        public ActionResult AddTiquete([FromBody] Tiquete value)
        {
            _miBD.Add(value);
            _miBD.SaveChanges();
            return Ok(value);
        }

        [HttpGet("GetAllTiquetes")]
        public ActionResult<IEnumerable<Tiquete>> GetAllTiquetes()
        {
            return Ok(_miBD.Tiquetes.ToList());
        }

        [HttpGet("GetTiqueteXId/{id}")]
        public ActionResult<Tiquete> GetTiqueteXId(int id) {
            Tiquete tiquete;
            tiquete = _miBD.Tiquetes.Where(x => x.Id == id).FirstOrDefault();
            return Ok(tiquete);
        }

        [HttpPost("UpdateTiquete")]
        public ActionResult UpdateTiquete([FromBody] Tiquete value)
        {
            _miBD.Update(value);
            _miBD.SaveChanges();
            return Ok(value);
        }

        [HttpDelete("DeleteTiquete/{id}")]
        public ActionResult DeleteTiquete(int id)
        {
            Tiquete tiquete;
            tiquete = _miBD.Tiquetes.Where(x => x.Id == id).FirstOrDefault();
            _miBD.Remove(tiquete);
            _miBD.SaveChanges();
            return Ok();
        }
    }
}
