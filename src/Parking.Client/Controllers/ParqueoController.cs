using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Proyecto1.Models;
using Proyecto2Client.Interfaces;

namespace Proyecto1.Controllers
{
    public class ParqueoController : Controller
    {
        private readonly IParqueoServices _iParqueoServices;

        public ParqueoController(IParqueoServices parqueoServices)
        {
            _iParqueoServices = parqueoServices;
        }

        // GET: ParqueoController
        public async Task<ActionResult> Index()
        {
            List<Parqueo> parqueos;

            parqueos = await _iParqueoServices.GetAllParqueos();
            return View(parqueos);
        }

        // GET: ParqueoControllet/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ParqueoControllet/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Parqueo parqueo)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _iParqueoServices.AddParqueo(parqueo);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: ParqueoControllet/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            Parqueo parqueo;
            parqueo = await _iParqueoServices.GetParqueoXId(id);
            return View(parqueo);
        }

        // POST: ParqueoControllet/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Parqueo parqueo)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _iParqueoServices.UpdateParqueo(parqueo);
                    return RedirectToAction(nameof(Index));
                }
                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: ParqueoControllet/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            Parqueo parqueo;
            parqueo = await _iParqueoServices.GetParqueoXId(id);
            return View(parqueo);
        }

        // POST: ParqueoControllet/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Parqueo parqueo)
        {
            await _iParqueoServices.DeleteParqueo(parqueo.Id);
            return RedirectToAction(nameof(Index));
        }
    }
}
