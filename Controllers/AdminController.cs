using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class AdminController : Controller
    {
        private ApplicationDbContext _context;
        public AdminController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: Admin
        public IEnumerable<Admin> GetAdmins()
        {
            return _context.Admins.ToList();

        }

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AddOrEdit(int id = 0)
        {


            if (id != 0)
            {
                var AdminInDb = _context.Admins.SingleOrDefault(c => c.ID == id);
                return View(AdminInDb);
            }
            else
            {
                Admin admin = new Admin();
                return View(admin);
            }

        }
        [HttpPost]
        public ActionResult AddOrEdit(Admin admin)
        {
            try
            {

                if (admin.ID == 0)
                {
                    _context.Admins.Add(admin);
                    _context.SaveChanges();
                }
                else
                {
                    var AdminInDb = _context.Admins.SingleOrDefault(c => c.ID == admin.ID);
                    if (AdminInDb == null)
                    {
                        return Json(new { success = false, message = "Client is not found in DB " }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        AdminInDb.Name = admin.Name;
                        AdminInDb.password = admin.password;
                        AdminInDb.Active = admin.Active;
                        AdminInDb.email = admin.email;
                        AdminInDb.RoleName = admin.RoleName;
                        _context.SaveChanges();
                    }

                }

                return Json(new { success = true, html = GlobalClass.RenderRazorViewToString(this, "AdminList", GetAdmins()), message = "Submitted Successfully" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult Delete(int id)
        {
            try
            {
                var AdminInDB = _context.Admins.SingleOrDefault(s => s.ID == id);
                _context.Admins.Remove(AdminInDB);
                _context.SaveChanges();

                return Json(new { success = true, html = GlobalClass.RenderRazorViewToString(this, "AdminList", GetAdmins()), message = "Deleted Successfully" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult AdminList()
        {
            var AdminList = _context.Admins.ToList();
            return View(AdminList);
        }
    }
}