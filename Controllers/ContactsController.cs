﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.SqlServer;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ContactsApp.DAL;
using ContactsApp.Data;
using ContactsApp.Models;
using WebGrease.Css.Ast.Selectors;

namespace ContactsApp.Controllers
{
    public class ContactsController : Controller
    {
        private ContactContext db = new ContactContext();

        public ActionResult Index()
        {
          
            return View(db.Contacts.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = db.Contacts.Find(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }


        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,LastName,FirstName,Patronymic,DateOfBirth,Company,Position,ContactInformation,PhoneNumbers,Email,Skype,Other")] Contact contact)
        {
            if (ModelState.IsValid)
            {
                db.Contacts.Add(contact);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(contact);
        }

       
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var contact = db.Contacts.Find(id);

            if (contact == null)
            {
                return HttpNotFound();
            }

            contact.PhoneNumbers = contact.PhoneNumbers.Where(x => !x.IsDeleted).ToList();

            var contactForEdit = new ContactEditModel()
            {
                Id = contact.Id,
                FirstName = contact.FirstName,
                LastName = contact.LastName,
                Patronymic = contact.Patronymic,
                DateOfBirth = contact.DateOfBirth?.ToString("dd.MM.yyyy"),
                Company = contact.Company,
                Position = contact.Position,
                ContactInformation = contact.ContactInformation,
                Email = contact.Email,
                Other = contact.Other,
                Skype = contact.Skype,
                PhoneNumbers = contact.PhoneNumbers
            };
            return View(contactForEdit);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ContactEditModel model)
        {
            var contact = await db.Contacts.FirstOrDefaultAsync(x => x.Id == model.Id);

            if (contact is null)
            {
                return HttpNotFound("Contact not found");
            }

            if (ModelState.IsValid)
            {
                contact.FirstName = model.FirstName;
                contact.LastName = model.LastName;
                contact.Patronymic = model.Patronymic;
                //contact.DateOfBirth = DateTime.ParseExact(model.DateOfBirth, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                contact.DateOfBirth = DateTime.Parse(model.DateOfBirth);
                contact.Company = model.Company;
                contact.Position = model.Position;
                contact.ContactInformation = model.ContactInformation;
                contact.Other = model.Other;
                contact.Skype = model.Skype;
                contact.Email = model.Email;

                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(contact);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
           
            var contact = db.Contacts.Find(id);

            if (contact == null)
            {
                return HttpNotFound();
            }

            return View(contact);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Contact contact = db.Contacts.Find(id);
            db.Contacts.Remove(contact);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        [HttpPost, ActionName("DeleteNumber")]
        public async Task<ActionResult> DeleteNumber(int id)
        {
            var number = await db.ContactPhoneNumbers.FindAsync(id);

            if (number is null)
            {
                return HttpNotFound();
            }

            number.IsDeleted = true;

            await db.SaveChangesAsync();

            return new HttpStatusCodeResult(200);
        }

        [HttpPost]
        public async Task<ActionResult> CreateNumber(string numberValue, int contactId)
        {
            var newNumber = new ContactPhoneNumber();
            newNumber.PhoneNumber = numberValue;
            newNumber.ContactId = contactId;
            db.ContactPhoneNumbers.Add(newNumber);
            await db.SaveChangesAsync();

            return new HttpStatusCodeResult(200);
        }

        [HttpGet]
        public ActionResult Searching(ContactSearchFieldTypes field, string text)
        {

            List<Contact> contacts;

            switch (field)
            {
                case ContactSearchFieldTypes.FirstName:
                    contacts = db.Contacts.Include(c => c.PhoneNumbers).Where(x => x.FirstName.Contains(text)).ToList();
                    break;
                case ContactSearchFieldTypes.LastName:
                    contacts = db.Contacts.Include(c => c.PhoneNumbers).Where(x => x.LastName.Contains(text)).ToList();
                    break;
                case ContactSearchFieldTypes.Patronymic:
                    contacts = db.Contacts.Include(c => c.PhoneNumbers).Where(x => x.Patronymic.Contains(text)).ToList();
                    break;
                case ContactSearchFieldTypes.DateOfBirth:
                   contacts = db.Contacts.Include(c => c.PhoneNumbers).Where(x =>
                       x.DateOfBirth.HasValue && x.DateOfBirth.Value.Year.ToString().Contains(text)).ToList();
                    break;
                case ContactSearchFieldTypes.Company:
                    contacts = db.Contacts.Include(c => c.PhoneNumbers).Where(x => x.Company.Contains(text)).ToList();
                    break;
                case ContactSearchFieldTypes.Position:
                    contacts = db.Contacts.Include(c => c.PhoneNumbers).Where(x => x.Position.Contains(text)).ToList();
                    break;
                case ContactSearchFieldTypes.ContactInformation:
                    contacts = db.Contacts.Include(c => c.PhoneNumbers).Where(x => x.ContactInformation.Contains(text)).ToList();
                    break;
                case ContactSearchFieldTypes.PhoneNumbers:
                    contacts = db.ContactPhoneNumbers.Include(c => c.Contact)
                        .Where(x => x.PhoneNumber.Contains(text))
                        .Select(x => x.Contact).ToList();
                    break;
                case ContactSearchFieldTypes.Email:
                    contacts = db.Contacts.Include(c => c.PhoneNumbers).Where(x => x.Email.Contains(text)).ToList();
                    break;
              case ContactSearchFieldTypes.Skype:
                    contacts = db.Contacts.Include(c => c.PhoneNumbers).Where(x => x.Skype.Contains(text)).ToList();
                    break;
                case ContactSearchFieldTypes.Other:
                    contacts = db.Contacts.Include(c => c.PhoneNumbers).Where(x => x.Other.Contains(text)).ToList();
                    break;
                default:
                    contacts = new List<Contact>();
                    break;
            }

            ViewBag.Searching = true;

            return View("Index", contacts);
        }
        public ActionResult Partial()
        {
            return PartialView();
        }
    }
}
