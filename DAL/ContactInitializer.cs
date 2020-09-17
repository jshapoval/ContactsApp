using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ContactsApp.DAL;
using ContactsApp.Models;

namespace ContactsApp.DAL
{
   public class ContactInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<ContactContext>
    {
        protected override void Seed(ContactContext context)
        {
            var contacts = new List<Contact>
            {
                new Contact
                {
                    FirstName = "Anna", LastName = "Ivanova", DateOfBirth = DateTime.Parse("01-09-1997"),
                    PhoneNumbers = new List<ContactPhoneNumber>()
                        {new ContactPhoneNumber() {PhoneNumber = "79001252222"}}
                },
                new Contact
                {
                    FirstName = "Alex", LastName = "Petrov", DateOfBirth = DateTime.Parse("01-07-1994"),
                    PhoneNumbers = new List<ContactPhoneNumber>()
                    {
                        new ContactPhoneNumber() {PhoneNumber = "79002433333"},
                        new ContactPhoneNumber() {PhoneNumber = "79001254444"}
                    }
                },
                new Contact
                {
                    FirstName = "Nina", LastName = "Sidorova", DateOfBirth = DateTime.Parse("01-03-1994"),
                    PhoneNumbers = new List<ContactPhoneNumber>()
                    {
                        new ContactPhoneNumber() {PhoneNumber = "79001256666"},
                        new ContactPhoneNumber() {PhoneNumber = "79001255555"}
                    }
                },
                new Contact
                {
                    FirstName = "Petr", LastName = "Ivanov", DateOfBirth = DateTime.Parse("01-09-1997"),
                    PhoneNumbers = new List<ContactPhoneNumber>()
                        {new ContactPhoneNumber() {PhoneNumber = "79001255654"}}
                },

            };

            contacts.ForEach(s => context.Contacts.Add(s));
            context.SaveChanges();

        }
    }
}