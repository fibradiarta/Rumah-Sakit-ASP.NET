using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebRumahSakit.Models;

namespace WebRumahSakit.Controllers
{
    public class PerawatController : Controller
    {
        private DBRSEntities3 db = new DBRSEntities3();
        
        public async Task<ActionResult> Index()
        {
            return View(await db.PERAWATs.ToListAsync());
        }

        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PERAWAT perawat = await db.PERAWATs.FindAsync(id);
            if (perawat == null)
            {
                return HttpNotFound();
            }
            return View(perawat);
        }


        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken] //setelah post harus ada validate, validasi setiap inputan
        public async Task<ActionResult> Create(PERAWAT perawat)
        {
            //apakah model statenya sudah benar atau belum, wajib saat post
            if (ModelState.IsValid)
            {
                db.PERAWATs.Add(perawat);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(perawat);
        }

        
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PERAWAT perawat = await db.PERAWATs.FindAsync(id);
            if (perawat == null)
            {
                return HttpNotFound();
            }
            return View(perawat);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(PERAWAT perawat)
        {
            if (ModelState.IsValid)
            {
                db.Entry(perawat).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(perawat);
        }

        
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PERAWAT perawat = await db.PERAWATs.FindAsync(id);
            if (perawat == null)
            {
                return HttpNotFound();
            }
            return View(perawat);
        }

       
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            PERAWAT perawat = await db.PERAWATs.FindAsync(id);
            db.PERAWATs.Remove(perawat);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
