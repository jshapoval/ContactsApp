using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContactsApp.Models
{
    public class Contact
    {
        public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Patronymic { get; set; }
        public DateTime Birthday { get; set; }
        public string Company { get; set; }
        public string Position { get; set; }
        public string ContactInformation { get; set; }
        public List<ContactPhoneNumber> PhoneNumbers { get; set; }
        public string Email { get; set; }
        public string Skype { get; set; }
        public string Other { get; set; }

    }

}