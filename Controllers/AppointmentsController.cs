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
    public class AppointmentsController : Controller
    {
        private Dental_Clinic_DB db = new Dental_Clinic_DB();

        // GET: Appointments
        public ActionResult Index(string sortOrder, string searchString)
        {
            //  var appointments = db.Appointments.Include(a => a.Patient);
            //return View(await appointments.ToListAsync());

            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "date_desc";

            var appointments = from s in db.Appointments select s;


            if (!String.IsNullOrEmpty(searchString))
            {
                appointments = appointments.Where(s => s.Patient.Pat_Name.ToUpper().Contains(searchString.ToUpper()));
            }



            switch (sortOrder)
            {
                case "name_desc":
                    appointments = appointments.OrderByDescending(s => s.Patient.Pat_Name);
                    break;
                case "date_desc":
                    appointments = appointments.OrderByDescending(s => s.Exam_Date);
                    break;
                default:
                    appointments = appointments.OrderBy(s => s.Reply_Date);
                    break;



            }
            return View(appointments.ToList());





        }











        // GET: Appointments/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appointment appointment = await db.Appointments.FindAsync(id);
            if (appointment == null)
            {
                return HttpNotFound();
            }
            return View(appointment);
        }




        // GET: Appointments/Create
        public ActionResult Create()
        {
            ViewBag.Pat_Id = new SelectList(db.Patients, "Pat_Id", "Pat_Name");
            return View();
        }

        // POST: Appointments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "App_Id,Pat_Id,Exam_Date,Reply_Date,Reason")] Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                db.Appointments.Add(appointment);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.Pat_Id = new SelectList(db.Patients, "Pat_Id", "Pat_Name", appointment.Pat_Id);
            return View(appointment);
        }









        // GET: Appointments/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appointment appointment = await db.Appointments.FindAsync(id);
            if (appointment == null)
            {
                return HttpNotFound();
            }
            ViewBag.Pat_Id = new SelectList(db.Patients, "Pat_Id", "Pat_Name", appointment.Pat_Id);
            return View(appointment);
        }

        // POST: Appointments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "App_Id,Pat_Id,Exam_Date,Reply_Date,Reason")] Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(appointment).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Pat_Id = new SelectList(db.Patients, "Pat_Id", "Pat_Name", appointment.Pat_Id);
            return View(appointment);
        }









        // GET: Appointments/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appointment appointment = await db.Appointments.FindAsync(id);
            if (appointment == null)
            {
                return HttpNotFound();
            }
            return View(appointment);
        }

        // POST: Appointments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Appointment appointment = await db.Appointments.FindAsync(id);
            db.Appointments.Remove(appointment);
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
