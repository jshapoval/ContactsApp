using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ContactsApp.Models
{
    public class Contact
    {
        public int Id { get; set; }

        [StringLength(50, ErrorMessage = "Last name cannot be longer than 50 characters.")]
        [Display(Name = "Last name")]
        [DisplayFormat(NullDisplayText = "No information")]
        public string LastName { get; set; }


        [Required]
        [StringLength(50, ErrorMessage = "First name cannot be longer than 50 characters.")]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [StringLength(50, ErrorMessage = "Patronymic cannot be longer than 50 characters.")]
        [DisplayFormat(NullDisplayText = "No information")]
        public string Patronymic { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true,  NullDisplayText = "No information")]
        [Display(Name = "Date of birth")]
        public DateTime DateOfBirth { get; set; }

        [DisplayFormat(NullDisplayText = "No information")]
        public string Company { get; set; }

        [DisplayFormat(NullDisplayText = "No information")]
        public string Position { get; set; }

        [Display(Name = "Contact information")]
        [DisplayFormat(NullDisplayText = "No information")]
        public string ContactInformation { get; set; }

        [Required]
        [Display(Name = "Phone numbers")]
       // public List<ContactPhoneNumber> PhoneNumbers { get; set; }
       public virtual ICollection<ContactPhoneNumber> PhoneNumbers { get; set; }

       [DisplayFormat(NullDisplayText = "No information")]
        public string Email { get; set; }

        [DisplayFormat(NullDisplayText = "No information")]
        public string Skype { get; set; }

        [DisplayFormat(NullDisplayText = "No information")]
        public string Other { get; set; }

    }

}