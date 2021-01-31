using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
namespace WebApplication1.Controllers
{
    public class SupplierController : Controller
    {
        private ApplicationDbContext _context;
        public ActionResult Index()
        {
            return View();
        }
        public SupplierController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        public IEnumerable<Supplier> GetSuppliers()
        {
            return _context.Suppliers.ToList();

        }
        public ActionResult AddOrEdit(int id = 0)
        {
           

            if (id != 0)
            {
                var SupplierInDb = _context.Suppliers.SingleOrDefault(c => c.ID == id);
                return View(SupplierInDb);
            }
            else {
                Supplier supplier = new Supplier();
                return View(supplier);
            }
                
        }
        [HttpPost]
        public ActionResult AddOrEdit(Supplier supplier)
        {
            try
            {

                if (supplier.ID == 0)
                    {
                      _context.Suppliers.Add(supplier);
                        _context.SaveChanges();
                    }
                    else
                    {
                    var SupplierInDb = _context.Suppliers.SingleOrDefault(c => c.ID == supplier.ID);
                    if (SupplierInDb == null)
                    {
                        return Json(new { success = false, message = "Supplier is not found in DB "}, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        SupplierInDb.Name = supplier.Name;
                        SupplierInDb.Address = supplier.Address;
                        SupplierInDb.RecordImage = supplier.RecordImage;
                        SupplierInDb.SNN = supplier.SNN;
                        SupplierInDb.TaxcardImage = supplier.TaxcardImage;
                        _context.SaveChanges();
                    }

                }
               
                return Json(new { success = true, html = GlobalClass.RenderRazorViewToString(this, "SupplierList", GetSuppliers()), message = "Submitted Successfully" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult SupplierList()
        {
            var SupplierList = _context.Suppliers.ToList();
            return View(SupplierList);
        }

        public ActionResult Delete(int id)
        {
            try
            {
                 var SupplierInDB = _context.Suppliers.SingleOrDefault(s => s.ID == id);
                _context.Suppliers.Remove(SupplierInDB);
                _context.SaveChanges();

                return Json(new { success = true, html = GlobalClass.RenderRazorViewToString(this, "SupplierList", GetSuppliers()), message = "Deleted Successfully" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}