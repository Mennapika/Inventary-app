using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication1.Models;

namespace WebApplication1.Controllers.Api
{
    public class SupplierApiController : ApiController
    {
        private ApplicationDbContext _context;
        public SupplierApiController()
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
        public Supplier GetSupplier(int id)
        {
            var Supplier = _context.Suppliers.SingleOrDefault(s => s.ID == id);
            if (Supplier == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return Supplier;
        }
        [HttpPost]
        public Supplier CreateSupplier(Supplier Supplier)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            _context.Suppliers.Add(Supplier);
            _context.SaveChanges();
            return Supplier;
        }
        [HttpPut]
        public HttpResponseMessage Edit(int id, [FromBody] Supplier Supplier)
        {
            try
            {
                var SupplierInDb = _context.Suppliers.SingleOrDefault(c => c.ID == id);
                if (SupplierInDb == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Supplier with id =" + id.ToString() + "not found to edit");
                }
                else
                {
                    SupplierInDb.Name = Supplier.Name;
                    SupplierInDb.Address = Supplier.Address;
                    SupplierInDb.RecordImage = Supplier.RecordImage;
                    SupplierInDb.SNN = Supplier.SNN;
                    SupplierInDb.TaxcardImage = Supplier.TaxcardImage;

                    _context.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK);

                }
            }
            catch (Exception ex)
            {

                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);

            }
        }

        [HttpDelete]
        public HttpResponseMessage DeletSupplier(int id)
        {


            try
            {
                var SupplierInDB = _context.Suppliers.SingleOrDefault(s => s.ID == id);
                if (SupplierInDB == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Supplier with id =" + id.ToString() + "not found to delete");

                }
                _context.Suppliers.Remove(SupplierInDB);
                _context.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {

                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);

            }
        }
    }
}
