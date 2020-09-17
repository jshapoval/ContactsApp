using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using ContactsApp.Models;

namespace ContactsApp.DAL
{
    public class ContactContext : DbContext
    {
        public ContactContext() : base("LocalMsSqlString") { }
        public ContactContext(string connectionString) : base(connectionString) { }
        public DbSet<Contact> Contacts { get; set; } 
        public DbSet<ContactPhoneNumber> ContactPhoneNumbers { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
           // modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<ContactPhoneNumber>()
                .HasRequired<Contact>(s => s.Contact)
                .WithMany(g => g.PhoneNumbers);
        }
    }
}