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
using BlacksmithPress.Diabetes.Persistence.Database;

namespace BlacksmithPress.Diabetes.Cloud.Controllers
{
    public class PeopleController : ApiController
    {
        public PeopleController()
        {
            this.database = new Context("DefaultConnection");
        }

        public PeopleController(Context database)
        {
            this.database = database;
        }

        private Context database;

        // GET: api/People
        public IQueryable<Person> GetPeople()
        {
            return database.People;
        }

        // GET: api/People/5
        [ResponseType(typeof(Person))]
        public IHttpActionResult GetPerson(long id)
        {
            Person person = database.People.Find(id);
            if (person == null)
            {
                return NotFound();
            }

            return Ok(person);
        }

        [ResponseType(typeof(Person))]
        public IHttpActionResult PatchPerson(long id, Person person)
        {
            var actual = database.People.FirstOrDefault(p => p.Id == id);
            if (actual == null)
                return NotFound();

            // Modify any provided properties here
            if (!string.IsNullOrEmpty(person.Name))
                actual.Name = person.Name;

            // End of patching
            database.SaveChanges();

            return Ok(actual);
        }

        // PUT: api/People/5
        [ResponseType(typeof(Person))]
        public IHttpActionResult PutPerson(long id, Person person)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != person.Id)
            {
                return BadRequest();
            }

            database.Entry(person).State = EntityState.Modified;

            try
            {
                database.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(person);
        }

        // POST: api/People
        [ResponseType(typeof(Person))]
        public IHttpActionResult PostPerson(Person person)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            database.People.Add(person);
            database.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = person.Id }, person);
        }

        // DELETE: api/People/5
        [ResponseType(typeof(Person))]
        public IHttpActionResult DeletePerson(long id)
        {
            Person person = database.People.Find(id);
            if (person == null)
            {
                return NotFound();
            }

            database.People.Remove(person);
            database.SaveChanges();

            return Ok(person);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                database.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PersonExists(long id)
        {
            return database.People.Count(e => e.Id == id) > 0;
        }
    }
}