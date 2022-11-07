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
    public class UsersController : Controller
    {
        private UDBContext db = new UDBContext();


        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel login)
        {
            if (ModelState.IsValid)
            {
                UDBContext db = new UDBContext();
                var user = (from userlist in db.User
                            where userlist.Email == login.UserEmail && userlist.Password == login.Password
                            select new
                            {
                                userlist.FirstName,
                                userlist.Email,
                                userlist.RoleID,
                                userlist.Role
                            }).ToList();
                if (user.FirstOrDefault() != null)
                {
                    Session["UserName"] = user.FirstOrDefault().FirstName;
                    Session["Role"] = user.FirstOrDefault().Role.RoleName;

                    Session["RoleID"] = user.FirstOrDefault().Role.RoleID;
                    if (user.FirstOrDefault().RoleID == 1)
                    {
                        return Redirect("/Users/Index");
                    }
                    if (user.FirstOrDefault().RoleID == 2)
                    {
                        return Redirect("/Doctor/Index");
                    }
                    else {
                        return Redirect("/Paitent/Index");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Invalid login credentials.");
                }
            }
            return View(login);
        }










        // GET: Users
        public async Task<ActionResult> Index()
        {
            var user = db.User.Include(u => u.Role);
            return View(await user.ToListAsync());
        }

        // GET: Users/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = await db.User.FindAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            ViewBag.RoleID = new SelectList(db.Role, "RoleID", "RoleName");
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "UserId,FirstName,LastName,Email,Password,DOB,MobileID,RoleID")] User user)
        {
            if (ModelState.IsValid)
            {
                db.User.Add(user);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.RoleID = new SelectList(db.Role, "RoleID", "RoleName", user.RoleID);
            return View(user);
        }

        // GET: Users/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = await db.User.FindAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.RoleID = new SelectList(db.Role, "RoleID", "RoleName", user.RoleID);
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "UserId,FirstName,LastName,Email,Password,DOB,MobileID,RoleID")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.RoleID = new SelectList(db.Role, "RoleID", "RoleName", user.RoleID);
            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = await db.User.FindAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            User user = await db.User.FindAsync(id);
            db.User.Remove(user);
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
