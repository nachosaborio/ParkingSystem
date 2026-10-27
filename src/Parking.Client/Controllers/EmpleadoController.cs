using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Proyecto1.Models;
using Proyecto2Client.Interfaces;

namespace Proyecto1.Controllers
{
    public class EmpleadoController : Controller
    {
        private readonly IEmpleadoServices _iEmpleadoServices;

        public EmpleadoController(IEmpleadoServices empleadoServices)
        {
            _iEmpleadoServices = empleadoServices;
        }

        // GET: EmpleadoControllet
        public async Task<ActionResult> Index()
        {
            List<Empleado> empleados;
            
            empleados = await _iEmpleadoServices.GetAllEmpleados();
            return View(empleados);
        }

        // GET: EmpleadoControllet/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EmpleadoControllet/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Empleado empleado)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    await _iEmpleadoServices.AddEmpleado(empleado);
                    return RedirectToAction(nameof(Index));
                }
                else { 
                    return View(); 
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: EmpleadoControllet/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            Empleado empleado;
            empleado = await _iEmpleadoServices.GetEmpleadoXId(id);
            return View(empleado);
        }

        // POST: EmpleadoControllet/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Empleado empleado)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _iEmpleadoServices.UpdateEmpleado(empleado);
                    return RedirectToAction(nameof(Index));
                }
                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: EmpleadoControllet/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            Empleado empleado;
            empleado = await _iEmpleadoServices.GetEmpleadoXId(id);
            return View(empleado); 
        }

        // POST: EmpleadoControllet/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Empleado empleado)
        {
            await _iEmpleadoServices.DeleteEmpleado(empleado.Id);
            return RedirectToAction(nameof(Index));
        }
    }
}
