using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ContactsApp.Models
{
    public class ContactPhoneNumber
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Phone number")]
        [StringLength(11, ErrorMessage = "Phone number cannot be longer than 11 characters.")]
        [DisplayFormat(DataFormatString = "{0:# ###-###-####}")]
        [RegularExpression(@"\d+", ErrorMessage = "Please, without lettars and symbols")]
        public string PhoneNumber { get; set; }

        //[Key]
       // [ForeignKey("Contact")]
        public int ContactId { get; set; }
        public virtual Contact Contact { get; set; }

        public bool IsDeleted { get; set; }
        public override string ToString()
        {
            return String.Format("{0:+# (###) ###-##-##}", PhoneNumber);
        }
    }
}