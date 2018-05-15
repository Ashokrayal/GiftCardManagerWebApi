using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using GiftManagerWebApi;
using System.Web.Http.Cors;

namespace GiftManagerWebApi.Controllers
{
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    public class UserPermissionsController : ApiController
    {
        private GiftManagerEntities db = new GiftManagerEntities();

        // GET: api/UserPermissions
        public IQueryable<UserPermission> GetUserPermissions()
        {
            return db.UserPermissions;
        }

        // GET: api/UserPermissions/5
        [ResponseType(typeof(UserPermission))]
        public IHttpActionResult GetUserPermission(int id)
        {
            UserPermission userPermission = db.UserPermissions.Find(id);
            if (userPermission == null)
            {
                return NotFound();
            }

            return Ok(userPermission);
        }

        // PUT: api/UserPermissions/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUserPermission(int id, UserPermission userPermission)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != userPermission.Id)
            {
                return BadRequest();
            }

            db.Entry(userPermission).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserPermissionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/UserPermissions
        [ResponseType(typeof(UserPermission))]
        public IHttpActionResult PostUserPermission(UserPermission userPermission)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.UserPermissions.Add(userPermission);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = userPermission.Id }, userPermission);
        }

        // DELETE: api/UserPermissions/5
        [ResponseType(typeof(UserPermission))]
        public IHttpActionResult DeleteUserPermission(int id)
        {
            UserPermission userPermission = db.UserPermissions.Find(id);
            if (userPermission == null)
            {
                return NotFound();
            }

            db.UserPermissions.Remove(userPermission);
            db.SaveChanges();

            return Ok(userPermission);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserPermissionExists(int id)
        {
            return db.UserPermissions.Count(e => e.Id == id) > 0;
        }
    }
}