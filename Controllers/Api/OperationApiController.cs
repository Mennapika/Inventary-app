using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication1.Models;
namespace WebApplication1.Controllers.Api
{
    public class OperationApiController : ApiController
    {
        private ApplicationDbContext _context;
        public OperationApiController()
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
        public Operation GetOperation(int id)
        {
            var Operation = _context.Operations.SingleOrDefault(s => s.Id == id);
            if (Operation == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return Operation;
        }
        [HttpPost]
        public Operation CreateOperation(Operation operation)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            _context.Operations.Add(operation);
            _context.SaveChanges();
            return operation;
        }
        [HttpPut]
        public HttpResponseMessage Edit(int id, [FromBody] Operation operation)
        {
            try
            {
                var OperationInDb = _context.Operations.SingleOrDefault(c => c.Id == id);
                if (OperationInDb == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "operation t with id =" + id.ToString() + "not found to edit");
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
                    return Request.CreateResponse(HttpStatusCode.OK);

                }
            }
            catch (Exception ex)
            {

                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);

            }
        }

        [HttpDelete]
        public HttpResponseMessage DeleteOperations(int id)
        {


            try
            {
                var OperationInDB = _context.Operations.SingleOrDefault(s => s.Id == id);
                if (OperationInDB == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Operation with id =" + id.ToString() + "not found to delete");

                }
                _context.Operations.Remove(OperationInDB);
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
