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
    public class PrescriptionsController : Controller
    {
        private Dental_Clinic_DB db = new Dental_Clinic_DB();








        // GET: Prescriptions
        public ActionResult Index(string sortOrder , string searchString)
        {
            // var prescriptions = db.Prescriptions.Include(p => p.Patient);
            //  return View(await prescriptions.ToListAsync());

            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";

            var prescriptions = from s in db.Prescriptions select s;


            if (!String.IsNullOrEmpty(searchString))
            {
                prescriptions = prescriptions.Where(s => s.Patient.Pat_Name.ToUpper().Contains(searchString.ToUpper()));
            }



            switch (sortOrder)
            {
                case "name_desc":
                    prescriptions = prescriptions.OrderByDescending(s => s.Patient.Pat_Name);
                    break;

                default:
                    prescriptions = prescriptions.OrderBy(s => s.Patient.Pat_Name);
                    break;


            }
            return View(prescriptions.ToList());
        }



















        // GET: Prescriptions/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prescription prescription = await db.Prescriptions.FindAsync(id);
            if (prescription == null)
            {
                return HttpNotFound();
            }
            return View(prescription);
        }








        // GET: Prescriptions/Create
        public ActionResult Create()
        {
            ViewBag.Pat_Id = new SelectList(db.Patients, "Pat_Id", "Pat_Name");
            return View();
        }

        // POST: Prescriptions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Pre_Id,Pat_Id,X_Ray,Disease,Medication,Prescription_List,Current_Health,Cost,Pay,Rest")] Prescription prescription)
        {
            if (ModelState.IsValid)
            {
                db.Prescriptions.Add(prescription);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.Pat_Id = new SelectList(db.Patients, "Pat_Id", "Pat_Name", prescription.Pat_Id);
            return View(prescription);
        }









        // GET: Prescriptions/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prescription prescription = await db.Prescriptions.FindAsync(id);
            if (prescription == null)
            {
                return HttpNotFound();
            }
            ViewBag.Pat_Id = new SelectList(db.Patients, "Pat_Id", "Pat_Name", prescription.Pat_Id);
            return View(prescription);
        }

        // POST: Prescriptions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Pre_Id,Pat_Id,X_Ray,Disease,Medication,Prescription_List,Current_Health,Cost,Pay,Rest")] Prescription prescription)
        {
            if (ModelState.IsValid)
            {
                db.Entry(prescription).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Pat_Id = new SelectList(db.Patients, "Pat_Id", "Pat_Name", prescription.Pat_Id);
            return View(prescription);
        }

        // GET: Prescriptions/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prescription prescription = await db.Prescriptions.FindAsync(id);
            if (prescription == null)
            {
                return HttpNotFound();
            }
            return View(prescription);
        }

        // POST: Prescriptions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Prescription prescription = await db.Prescriptions.FindAsync(id);
            db.Prescriptions.Remove(prescription);
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
