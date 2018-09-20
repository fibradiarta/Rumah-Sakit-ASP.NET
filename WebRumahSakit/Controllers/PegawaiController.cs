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
    public class PegawaiController : Controller
    {
        DBRSEntities3 db = new DBRSEntities3(); 
        // GET: Pegawai
        public async Task<ActionResult> Index()
        {
            return View(await db.PEGAWAIs.ToListAsync());
        }

        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PEGAWAI pegawai = await db.PEGAWAIs.FindAsync(id);
            if (pegawai == null)
            {
                return HttpNotFound();
            }
            return View(pegawai);
        }

        public async Task<ActionResult> Create()
        {
            var role = await db.ROLEs.Where(x => x.ROLE_ID != 1).Select(i => new SelectListItem()
            {
                Text = i.NAMA,
                Value = i.ROLE_ID.ToString(),
                Selected = false
            }).ToArrayAsync();
            ViewBag.Role = role;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken] //setelah post harus ada validate, validasi setiap inputan
        public async Task<ActionResult> Create(PEGAWAI pegawai)
        {
            //apakah model statenya sudah benar atau belum, wajib saat post
            if (ModelState.IsValid)
            {
                db.PEGAWAIs.Add(pegawai);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(pegawai);
        }

        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            PEGAWAI pegawai = await db.PEGAWAIs.FindAsync(id);
            
            var role = await db.ROLEs.Where(x => x.ROLE_ID != 1).Select(i => new SelectListItem()
            {
                Text = i.NAMA,
                Value = i.ROLE_ID.ToString(),
                Selected = false
            }).ToArrayAsync();


            if (pegawai == null)
            {
                return HttpNotFound();
            }

           foreach (var item in role)
           {
                if (item.Value == pegawai.ROLE.ROLE_ID.ToString())
                {
                   item.Selected = true;
                   break;
                }
            }

            ViewBag.Role = role;
            var vmRuangan = new PegawaiViewModels(pegawai);
            return View(vmRuangan);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(PegawaiViewModels vmPegawai)
        {
            if (ModelState.IsValid)
            {
                var pegawai = await db.PEGAWAIs.Include("ROLE").Where(x => x.PEGAWAI_ID == vmPegawai.PEGAWAI_ID).SingleOrDefaultAsync();
                
                var role = await db.ROLEs.FindAsync(Convert.ToInt32(vmPegawai.ROLE_ID));

                pegawai.NAMA = vmPegawai.NAMA;
                pegawai.ALAMAT = vmPegawai.ALAMAT;
                pegawai.NO_TELP = pegawai.NO_TELP;
                pegawai.JENIS_KELAMIN = vmPegawai.JENIS_KELAMIN;
                pegawai.STATUS = vmPegawai.STATUS;
                pegawai.EMAIL = vmPegawai.EMAIL;
                pegawai.PASSWORD = vmPegawai.PASSWORD;

                if (role != null)
                {
                    pegawai.ROLE = role;
                    db.Entry(pegawai).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            return View(vmPegawai);
        }

        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PEGAWAI pegawai = await db.PEGAWAIs.FindAsync(id);
            if (pegawai == null)
            {
                return HttpNotFound();
            }
            return View(pegawai);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            PEGAWAI pegawai = await db.PEGAWAIs.FindAsync(id);
            db.PEGAWAIs.Remove(pegawai);
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