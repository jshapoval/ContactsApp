using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using ContactsApp.Models;

namespace ContactsApp.Data
{
    public class ContactContext : DbContext
    {
        public ContactContext() : base("DefaultConnection")
        { }

        public DbSet<Contact> Contacts { get; set; }
       // public DbSet<ContactPhoneNumber> ContactPhoneNumbers { get; set; }

    }
}