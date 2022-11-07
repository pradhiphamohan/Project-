using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication5;

namespace WebApplication5.Controllers
{
    public class DoctorController : Controller
    {
        private UDBContext db = new UDBContext();

        // GET: Doctor
        public async Task<ActionResult> Index()
        {
            var appointment = db.Appointment;
            return View(await appointment.ToListAsync());
        }

        // GET: Doctor/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appointment appointment = await db.Appointment.FindAsync(id);
            if (appointment == null)
            {
                return HttpNotFound();
            }
            return View(appointment);
        }

        // GET: Doctor/Create
        public ActionResult Create()
        {
            //ViewBag.RoleID = new SelectList(db.Role, "RoleID", "RoleName");
            return View();
        }

        // POST: Doctor/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id,FirstName,DateandTime,Approved")] Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                db.Appointment.Add(appointment);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            //ViewBag.RoleID = new SelectList(db.Role, "RoleID", "RoleName", appointment.RoleID);
            return View(appointment);
        }

        // GET: Doctor/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appointment appointment = await db.Appointment.FindAsync(id);
            if (appointment == null)
            {
                return HttpNotFound();
            }
            //ViewBag.RoleID = new SelectList(db.Role, "RoleID", "RoleName", appointment.RoleID);
            return View(appointment);
        }

        // POST: Doctor/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,FirstName,DateandTime,Approved")] Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(appointment).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            //ViewBag.RoleID = new SelectList(db.Role, "RoleID", "RoleName", appointment.RoleID);
            return View(appointment);
        }

        // GET: Doctor/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appointment appointment = await db.Appointment.FindAsync(id);
            if (appointment == null)
            {
                return HttpNotFound();
            }
            return View(appointment);
        }

        // POST: Doctor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Appointment appointment = await db.Appointment.FindAsync(id);
            db.Appointment.Remove(appointment);
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
