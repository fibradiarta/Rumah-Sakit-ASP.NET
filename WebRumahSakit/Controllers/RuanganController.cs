using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebRumahSakit.Models;
using WebRumahSakit.ViewModels;

namespace WebRumahSakit.Controllers
{
    public class RuanganController : Controller
    {
        private DBRSEntities3 db = new DBRSEntities3();
        // GET: Ruangan
        
        public async Task<ActionResult> Index()
        {
            return View(await db.RUANGANs.ToListAsync());
        }

        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RUANGAN ruangan = await db.RUANGANs.FindAsync(id);
            if (ruangan == null)
            {
                return HttpNotFound();
            }
            return View(ruangan);
        }

        public async Task<ActionResult> Create()
        {
            var jenisRuangan = await db.JENIS_RUANGAN.OrderBy(x => x.NAMA).Select(i => new SelectListItem()
            {
                Text = i.NAMA,
                Value = i.JENIS_RUANGAN_ID.ToString(),
                Selected = false
            }).ToArrayAsync();
            ViewBag.jRuangan = jenisRuangan;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken] //setelah post harus ada validate, validasi setiap inputan
        public async Task<ActionResult> Create(RUANGAN ruangan)
        {
            //apakah model statenya sudah benar atau belum, wajib saat post
            if (ModelState.IsValid)
            {
                db.RUANGANs.Add(ruangan);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(ruangan);
        }

        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RUANGAN ruangan = await db.RUANGANs.FindAsync(id);
            var jenisRuangan = await db.JENIS_RUANGAN.OrderBy(x => x.NAMA).Select(i => new SelectListItem()
            {
                Text = i.NAMA,
                Value = i.JENIS_RUANGAN_ID.ToString(),
                Selected = false
            }).ToArrayAsync();

            if (ruangan == null)
            {
                return HttpNotFound();
            }

            foreach (var item in jenisRuangan)
            {
                if (item.Value == ruangan.JENIS_RUANGAN.JENIS_RUANGAN_ID.ToString())
                {
                    item.Selected = true;
                    break;
                }
            }

            ViewBag.jRuangan = jenisRuangan;
            var vmRuangan = new RuanganViewModels(ruangan);
            return View(vmRuangan);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(RuanganViewModels vmRuangan)
        {
            if (ModelState.IsValid)
            {
                var ruangan = await db.RUANGANs.Include("JENIS_RUANGAN").Where(x => x.RUANGAN_ID == vmRuangan.RUANGAN_ID).SingleOrDefaultAsync();
                var jenisRuangan = await db.JENIS_RUANGAN.FindAsync(Convert.ToInt32(vmRuangan.JENIS_RUANGAN_ID));

                ruangan.NAMA = vmRuangan.NAMA;
                ruangan.STATUS = vmRuangan.STATUS;

                if (jenisRuangan != null)
                {
                    ruangan.JENIS_RUANGAN = jenisRuangan;
                    db.Entry(ruangan).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                
            }
            return View(vmRuangan);
        }

        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RUANGAN ruangan = await db.RUANGANs.FindAsync(id);
            if (ruangan == null)
            {
                return HttpNotFound();
            }
            return View(ruangan);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            RUANGAN ruangan = await db.RUANGANs.FindAsync(id);
            db.RUANGANs.Remove(ruangan);
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