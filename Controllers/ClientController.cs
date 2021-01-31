using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class ClientController : Controller
    {
        private ApplicationDbContext _context;

        public ClientController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        public IEnumerable<Client> GetClients()
        {
            return _context.Clients.ToList();

        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AddOrEdit(int id = 0)
        {


            if (id != 0)
            {
                var ClientInDb = _context.Clients.SingleOrDefault(c => c.ID == id);
                return View(ClientInDb);
            }
            else
            {
                Client client = new Client();
                return View(client);
            }

        }
        [HttpPost]
        public ActionResult AddOrEdit(Client client)
        {
            try
            {

                if (client.ID == 0)
                {
                    _context.Clients.Add(client);
                    _context.SaveChanges();
                }
                else
                {
                    var ClientInDb = _context.Clients.SingleOrDefault(c => c.ID == client.ID);
                    if (ClientInDb == null)
                    {
                        return Json(new { success = false, message = "Client is not found in DB " }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        ClientInDb.Name = client.Name;
                        ClientInDb.Address = client.Address;
                        ClientInDb.RecordImage = client.RecordImage;
                        ClientInDb.SNN = client.SNN;
                        ClientInDb.TaxcardImage = client.TaxcardImage;
                        _context.SaveChanges();
                    }

                }

                return Json(new { success = true, html = GlobalClass.RenderRazorViewToString(this, "ClientList", GetClients()), message = "Submitted Successfully" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult ClientList()
        {
            var ClientList = _context.Clients.ToList();
            return View(ClientList);
        }
        public ActionResult Delete(int id)
        {
            try
            {
                var ClientInDB = _context.Clients.SingleOrDefault(s => s.ID == id);
                _context.Clients.Remove(ClientInDB);
                _context.SaveChanges();

                return Json(new { success = true, html = GlobalClass.RenderRazorViewToString(this, "ClientList", GetClients()), message = "Deleted Successfully" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}