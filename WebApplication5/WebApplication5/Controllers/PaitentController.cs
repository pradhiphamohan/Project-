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
    public class PaitentController : Controller
    {
        private UDBContext db = new UDBContext();

        // GET: Paitent
        public async Task<ActionResult> Index()
        {
            var appointment = db.Appointment;
            return View(await appointment.ToListAsync());
        }

        // GET: Paitent/Details/5
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

        // GET: Paitent/Create
        public ActionResult Create()
        {
            ViewBag.RoleID = new SelectList(db.Role, "RoleID", "RoleName");
            return View();
        }

        // POST: Paitent/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id,FirstName,DateandTime,Approved,RoleID")] Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                var user = (from userlist in db.Appointment
                            where userlist.DateandTime == appointment.DateandTime 
                            //where userlist.Email == login.UserEmail && userlist.Password == login.Password
                            select new
                            {
                                userlist.FirstName                           
                            }).ToList();
                if(user.Count > 0){
                    ModelState.AddModelError(nameof(appointment.DateandTime), "Overlapping Date and Time");
                    return View(appointment);
                }
                appointment.Approved = 0;
                appointment.FirstName = Session["UserName"].ToString();
                //appointment.RoleID = (int)Session["RoleID"];
                db.Appointment.Add(appointment);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            //ViewBag.RoleID = new SelectList(db.Role, "RoleID", "RoleName", appointment.RoleID);
            return View(appointment);
        }

        // GET: Paitent/Edit/5
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

        // POST: Paitent/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,FirstName,DateandTime,Approved,RoleID")] Appointment appointment)
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

        // GET: Paitent/Delete/5
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

        // POST: Paitent/Delete/5
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
