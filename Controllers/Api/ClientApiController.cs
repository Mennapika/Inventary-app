using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication1.Models;

namespace WebApplication1.Controllers.Api
{
    public class ClientApiController : ApiController
    {
        private ApplicationDbContext _context;
        public ClientApiController()
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
        public Client GetClient(int id)
        {
            var Client = _context.Clients.SingleOrDefault(s => s.ID == id);
            if (Client == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return Client;
        }
        [HttpPost]
        public Client CreateClient(Client Client)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            _context.Clients.Add(Client);
            _context.SaveChanges();
            return Client;
        }
        [HttpPut]
        public HttpResponseMessage Edit(int id, [FromBody] Client Client)
        {
            try
            {
                var ClientInDb = _context.Clients.SingleOrDefault(c => c.ID == id);
                if (ClientInDb == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Client with id =" + id.ToString() + "not found to edit");
                }
                else
                {
                    ClientInDb.Name = Client.Name;
                    ClientInDb.Address = Client.Address;
                    ClientInDb.RecordImage = Client.RecordImage;
                    ClientInDb.SNN = Client.SNN;
                    ClientInDb.TaxcardImage = Client.TaxcardImage;

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
        public HttpResponseMessage DeletClient(int id)
        {


            try
            {
                var ClientInDB = _context.Clients.SingleOrDefault(s => s.ID == id);
                if (ClientInDB == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Client with id =" + id.ToString() + "not found to delete");

                }
                _context.Clients.Remove(ClientInDB);
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
