using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class OperationController : Controller
    {
        private ApplicationDbContext _context;

        public OperationController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        public IEnumerable<Operation> GetOperations()
        {
            return _context.Operations.ToList();

        }
        public ActionResult AddOrEdit(int id = 0)
        {
            
            if (id != 0)
            {
                var OperationInDb = _context.Operations.SingleOrDefault(c => c.Id == id);
                return View(OperationInDb);
            }
            else
            {
                Operation operation = new Operation();
                return View(operation);
            }

            
        }
        [HttpPost]
        public ActionResult AddOrEdit(Operation operation)
        {
            try
            {

                if (operation.Id == 0)
                {
                    _context.Operations.Add(operation);
                    _context.SaveChanges();
                }
                else
                {
                    var OperationInDb = _context.Operations.SingleOrDefault(c => c.Id == operation.Id);
                    if (OperationInDb == null)
                    {
                        return Json(new { success = false, message = "Operation is not found in DB " }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        OperationInDb.Notes = operation.Notes;
                        OperationInDb.PriceInTons = operation.PriceInTons;
                        OperationInDb.Subtotal = operation.Subtotal;
                        OperationInDb.Quantity = operation.Quantity;
                        OperationInDb.DeliveryReceiptNumber = operation.DeliveryReceiptNumber;
                        OperationInDb.Tax1 = operation.Tax1;
                        OperationInDb.Tax14 = operation.Tax14;
                        OperationInDb.BillDate = operation.BillDate;
                        OperationInDb.BillNumber = operation.BillNumber;
                        OperationInDb.Creditor = operation.Creditor;
                        OperationInDb.Debtor = operation.Debtor;
                        OperationInDb.DueDate = operation.DueDate;
                        OperationInDb.Type = operation.Type;
                        OperationInDb.Unit = operation.Unit;
   
     
                        _context.SaveChanges();
                    }


                }

                return Json(new { success = true, html = GlobalClass.RenderRazorViewToString(this, "OperationList", GetOperations()), message = "Submitted Successfully" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult OperationList()
        {
            var OperationList = _context.Operations.ToList();
            return View(OperationList);
        }
        public ActionResult Delete(int id)
        {
            try
            {
                var OperationInDB = _context.Operations.SingleOrDefault(s => s.Id == id);
                _context.Operations.Remove(OperationInDB);
                _context.SaveChanges();

                return Json(new { success = true, html = GlobalClass.RenderRazorViewToString(this, "OperationList", GetOperations()), message = "Deleted Successfully" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}