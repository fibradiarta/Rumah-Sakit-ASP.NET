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
    public class DokterController : Controller
    {
        DBRSEntities3 db = new DBRSEntities3();
        // GET: Dokter
        public async Task<ActionResult> Index()
        {
            return View(await db.DOKTERs.ToListAsync());
        }

        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DOKTER dokter = await db.DOKTERs.FindAsync(id);
            if (dokter == null)
            {
                return HttpNotFound();
            }
            return View(dokter);
        }

        public async Task<ActionResult> Create()
        {
            var poli = await db.POLIs.OrderBy(x => x.NAMA).Select(i => new SelectListItem()
            {
                Text = i.NAMA,
                Value = i.POLI_ID.ToString(),
                Selected =false
            }).ToArrayAsync();
            ViewBag.Poli = poli;

            //var poliumum = await db.POLIs.Where(x => x.TIPE_POLI_ID == 10).Select(i => new SelectListItem()
            //{
            //    Text = i.NAMA,
            //    Value = i.POLI_ID.ToString(),
            //    Selected = false
            //}).ToArrayAsync();
            //ViewBag.Poliumum = poliumum;

            //var tipepoli = await db.TIPE_POLI.OrderBy(x => x.NAMA).Select(i => new SelectListItem()
            //{
            //    Text = i.NAMA,
            //    Value = i.TIPE_POLI_ID.ToString(),
            //    Selected =false
            //}).ToArrayAsync();
            //ViewBag.TipePoli = tipepoli;

            var specialis = await db.SPECIALIS.OrderBy(x => x.NAMA).Select(i => new SelectListItem()
            {
                Text = i.NAMA,
                Value = i.SPECIALIS_ID.ToString(),
                Selected = false
            }).ToArrayAsync();
            ViewBag.Specialis = specialis;

            return View();
        }

       

        [HttpPost]
        [ValidateAntiForgeryToken] //setelah post harus ada validate, validasi setiap inputan
        public async Task<ActionResult> Create(DOKTER dokter)
        {
            //apakah model statenya sudah benar atau belum, wajib saat post
            if (ModelState.IsValid)
            {
                db.DOKTERs.Add(dokter);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(dokter);
        }

        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            DOKTER dokter = await db.DOKTERs.FindAsync(id);

            var poli = await db.POLIs.OrderBy(x => x.NAMA).Select(i => new SelectListItem()
            {
                Text = i.NAMA,
                Value = i.POLI_ID.ToString(),
                Selected = false
            }).ToArrayAsync();
            ViewBag.Poli = poli;


            var specialis = await db.SPECIALIS.OrderBy(x => x.NAMA).Select(i => new SelectListItem()
            {
                Text = i.NAMA,
                Value = i.SPECIALIS_ID.ToString(),
                Selected = false
            }).ToArrayAsync();
            

            if (dokter == null)
            {
                return HttpNotFound();
            }

            if (dokter.SPECIALIS_ID != null)
            {
                foreach (var item in poli)
                {
                    if (item.Value == dokter.POLI.POLI_ID.ToString())
                    {
                        item.Selected = true;
                        break;
                    }
                }

                foreach (var item in specialis)
                {
                    if (item.Value == dokter.SPECIALI.SPECIALIS_ID.ToString())
                    {
                        item.Selected = true;
                        break;
                    }
                }
            }
            else
            {
                foreach (var item in poli)
                {
                    if (item.Value == dokter.POLI.POLI_ID.ToString())
                    {
                        item.Selected = true;
                        break;
                    }
                }
            }
            

            ViewBag.Poli = poli;
            ViewBag.Specialis = specialis;
            var vmRuangan = new DokterViewModels(dokter);
            return View(vmRuangan);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(DokterViewModels vmDokter)
        {
            if (ModelState.IsValid)
            {
                var dokter = await db.DOKTERs.Include("SPECIALI").Include("POLI").Where(x => x.DOKTER_ID == vmDokter.DOKTER_ID).SingleOrDefaultAsync();
                var poli = await db.POLIs.FindAsync(Convert.ToInt32(vmDokter.POLI_ID));
                var specialis = await db.SPECIALIS.FindAsync(Convert.ToInt32(vmDokter.SPECIALIS_ID));

                dokter.NAMA = vmDokter.NAMA;
                dokter.ALAMAT = vmDokter.ALAMAT;
                dokter.NO_TELP = vmDokter.NO_TELP;
                dokter.JENIS_KELAMIN = vmDokter.JENIS_KELAMIN;
                dokter.STATUS = vmDokter.STATUS;

                if (poli != null && specialis != null)
                {
                    dokter.POLI = poli;
                    dokter.SPECIALI = specialis;
                    db.Entry(dokter).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                else if(poli != null && specialis == null)
                {
                    dokter.POLI = poli;
                    db.Entry(dokter).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }

            }
            return View(vmDokter);
        }

        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DOKTER dokter = await db.DOKTERs.FindAsync(id);
            if (dokter == null)
            {
                return HttpNotFound();
            }
            return View(dokter);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            DOKTER dokter = await db.DOKTERs.FindAsync(id);
            db.DOKTERs.Remove(dokter);
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