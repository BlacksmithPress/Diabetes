using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Results;
using BlacksmithPress.Diabetes.Persistence.Database;

namespace BlacksmithPress.Diabetes.Cloud.Controllers
{
    [Authorize]
    [BasicAuthentication]
    public class UsersController : ApiController
    {
        public UsersController()
        {
            this.database = new Context("BlacksmithPress.Diabetes");
        }

        public UsersController(Context database)
        {
            this.database = database;
        }

        private Context database;

        // GET: api/Users
        public IQueryable<User> GetUsers()
        {
            return database.Users;
        }

        // GET: api/Users/5
        [ResponseType(typeof(User))]
        public IHttpActionResult GetUser(long id)
        {
            User user = database.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [ResponseType(typeof(User))]
        public IHttpActionResult PatchUser(long id, User user)
        {
            var actual = database.Users.FirstOrDefault(p => p.Id == id);
            if (actual == null)
                return NotFound();

            // Modify any provided properties here
            if (!string.IsNullOrEmpty(user.Name))
                actual.Name = user.Name;

            // End of patching
            database.SaveChanges();

            return Ok(actual);
        }

        // PUT: api/Users/5
        [ResponseType(typeof(User))]
        public IHttpActionResult PutUser(long id, User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != user.Id)
            {
                return BadRequest();
            }

            database.Entry(user).State = EntityState.Modified;

            try
            {
                database.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(user);
        }

        // POST: api/Users
        [ResponseType(typeof(User))]
        public IHttpActionResult PostUser(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            database.Users.Add(user);
            database.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = user.Id }, user);
        }

        // DELETE: api/Users/5
        [ResponseType(typeof(User))]
        public IHttpActionResult DeleteUser(long id)
        {
            User user = database.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            database.Users.Remove(user);
            database.SaveChanges();

            return Ok(user);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                database.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserExists(long id)
        {
            return database.Users.Count(e => e.Id == id) > 0;
        }
    }
}