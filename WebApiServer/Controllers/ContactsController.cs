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
using WebApiServer.Models;

namespace WebApiServer.Controllers
{
    public class ContactsController : ApiController
    {
        private ContactServiceContext db = new ContactServiceContext();

        // GET: api/Contacts
        public IQueryable<Contact> GetContacts()
        {
            return db.Contacts;
        }
        
        public IQueryable<Contact> GetContactsByName(string name, bool contains)
        {
            if (contains)
                return db.Contacts.Where(x => x.Name.Contains(name));
            else
                return db.Contacts.Where(x => x.Name == name);
        }

        public IQueryable<Contact> GetContactsByEgn(string egn, bool contains)
        {
            if (contains)
                return db.Contacts.Where(x => x.Egn.Contains(egn));
            else
                return db.Contacts.Where(x => x.Egn == egn);
        }

        public IQueryable<Contact> GetContactsByAddress(string adr, bool contains)
        {
            if (contains)
                return db.Contacts.Where(x => x.Address.Contains(adr));
            else
                return db.Contacts.Where(x => x.Address == adr);
        }

        public IQueryable<Contact> GetContactsByTelephone(string tel, bool contains)
        {
            if (contains)
                return db.Contacts.Where(x => x.Telephone.Contains(tel));
            else
                return db.Contacts.Where(x => x.Telephone == tel);
        }
        
        public IQueryable<Contact> GetContactsById(int id, bool contains)
        {
            if(contains)
                return db.Contacts.Where(x => x.Id.ToString().Contains(id.ToString()));
            else
                return db.Contacts.Where(x => x.Id == id);
        }

        // PUT: api/Contacts/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutContact(int id, Contact contact)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != contact.Id)
            {
                return BadRequest();
            }

            db.Entry(contact).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContactExists(id))
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

        // POST: api/Contacts
        [ResponseType(typeof(Contact))]
        public IHttpActionResult PostContact(Contact contact)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Contacts.Add(contact);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = contact.Id }, contact);
        }

        // DELETE: api/Contacts/5
        [ResponseType(typeof(Contact))]
        public IHttpActionResult DeleteContact(int id)
        {
            Contact contact = db.Contacts.Find(id);
            if (contact == null)
            {
                return NotFound();
            }

            db.Contacts.Remove(contact);
            db.SaveChanges();

            return Ok(contact);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ContactExists(int id)
        {
            return db.Contacts.Count(e => e.Id == id) > 0;
        }
    }
}