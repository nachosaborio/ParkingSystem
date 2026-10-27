using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Proyecto1.Models;
using Proyecto2Client.Interfaces;

namespace Proyecto1.Controllers
{
    public class TiqueteController : Controller
    {
        private readonly ITiqueteServices _iTiqueteServices;

        public TiqueteController(ITiqueteServices tiqueteServices)
        {
            _iTiqueteServices = tiqueteServices;
        }

        // GET: TiqueteController
        public async Task<ActionResult> Index()
        {
            List<Tiquete> tiquetes;

            tiquetes = await _iTiqueteServices.GetAllTiquetes();
            return View(tiquetes);
        }

        // GET: TiqueteControllet/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TiqueteControllet/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Tiquete tiquete)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _iTiqueteServices.AddTiquete(tiquete);
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

        // GET: TiqueteControllet/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            Tiquete tiquete;
            tiquete = await _iTiqueteServices.GetTiqueteXId(id);
            return View(tiquete);
        }

        // POST: TiqueteControllet/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Tiquete tiquete)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _iTiqueteServices.UpdateTiquete(tiquete);
                    return RedirectToAction(nameof(Index));
                }
                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: TiqueteControllet/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            Tiquete tiquete;
            tiquete = await _iTiqueteServices.GetTiqueteXId(id);
            return View(tiquete);
        }

        // POST: TiqueteControllet/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Tiquete tiquete)
        {
            await _iTiqueteServices.DeleteTiquete(tiquete.Id);
            return RedirectToAction(nameof(Index));
        }
    }
}
