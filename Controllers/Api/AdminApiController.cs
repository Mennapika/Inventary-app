using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication1.Models;

namespace WebApplication1.Controllers.Api
{
    public class AdminApiController : ApiController
    {
        private ApplicationDbContext _context;
        public AdminApiController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        public IEnumerable<Admin> GetAdmins()
        {
            return _context.Admins.ToList();

        }
        public Admin GetAdmin(int id)
        {
            var Admin = _context.Admins.SingleOrDefault(s => s.ID == id);
            if (Admin == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return Admin;
        }
        [HttpPost]
        public Admin CreateAdmin(Admin admin)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            _context.Admins.Add(admin);
            _context.SaveChanges();
            return admin;
        }
        [HttpPut]
        public HttpResponseMessage Edit(int id, [FromBody] Admin admin)
        {
            try
            {
                var AdminInDb = _context.Admins.SingleOrDefault(c => c.ID == id);
                if (AdminInDb == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Admin with id =" + id.ToString() + "not found to edit");
                }
                else
                {
                    AdminInDb.Name = admin.Name;
                    AdminInDb.password = admin.password;
                    AdminInDb.Active = admin.Active;
                    AdminInDb.email = admin.email;
                    AdminInDb.RoleName = admin.RoleName;
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
        public HttpResponseMessage DeleteAdmin(int id)
        {


            try
            {
                var AdminInDB = _context.Admins.SingleOrDefault(s => s.ID == id);
                if (AdminInDB == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Operation with id =" + id.ToString() + "not found to delete");

                }
                _context.Admins.Remove(AdminInDB);
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
