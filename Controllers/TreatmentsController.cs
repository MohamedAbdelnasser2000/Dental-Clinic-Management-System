using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Dental_Clinic_Management_System.Models;

namespace Dental_Clinic_Management_System.Controllers
{
    public class TreatmentsController : Controller
    {
        private Dental_Clinic_DB db = new Dental_Clinic_DB();

        // GET: Treatments
        public ActionResult Index(string sortOrder, string searchString)
        {
            //  return View(await db.Treatments.ToListAsync());
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            var treatments = from s in db.Treatments select s;


            if (!String.IsNullOrEmpty(searchString))
            {
                treatments = treatments.Where(s => s.Treat_Name.ToUpper().Contains(searchString.ToUpper()));
            }



            switch (sortOrder)
            {
                case "name_desc":
                    treatments = treatments.OrderByDescending(s => s.Treat_Name);
                    break;

                default:
                    treatments = treatments.OrderBy(s => s.Treat_Name);
                    break;


            }
            return View(treatments.ToList());
        }





        // GET: Treatments/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Treatment treatment = await db.Treatments.FindAsync(id);
            if (treatment == null)
            {
                return HttpNotFound();
            }
            return View(treatment);
        }

        // GET: Treatments/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Treatments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Treat_Id,Treat_Name,Cost,Description,Quantity")] Treatment treatment)
        {
            if (ModelState.IsValid)
            {
                db.Treatments.Add(treatment);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(treatment);
        }

        // GET: Treatments/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Treatment treatment = await db.Treatments.FindAsync(id);
            if (treatment == null)
            {
                return HttpNotFound();
            }
            return View(treatment);
        }

        // POST: Treatments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Treat_Id,Treat_Name,Cost,Description,Quantity")] Treatment treatment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(treatment).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(treatment);
        }

        // GET: Treatments/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Treatment treatment = await db.Treatments.FindAsync(id);
            if (treatment == null)
            {
                return HttpNotFound();
            }
            return View(treatment);
        }

        // POST: Treatments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Treatment treatment = await db.Treatments.FindAsync(id);
            db.Treatments.Remove(treatment);
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
