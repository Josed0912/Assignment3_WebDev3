using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Assignment3_Torres_JoseDavid.Models
{
    public class ContactModel
    {
                                                          
        public int Id { get; set; } 
        public DateTime CreateDate { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "First name must be at least 2 characters long")]
        [RegularExpression("^[^0-9]+$", ErrorMessage = "First name cannot contain numbers.")]
        [Display(Name = "First Name")]

        public string FirstName { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "Last name must be at least 2 characters long")]
        [RegularExpression("^[^0-9]+$", ErrorMessage = "Last name cannot contain numbers.")]
        [Display(Name = "Last Name")]

        public string LastName { get; set; }

        //Regex source: https://stackoverflow.com/questions/15774555/efficient-regex-for-canadian-postal-code-function

        [Required]
        [DataType(DataType.PostalCode)]
        [RegularExpression(@"/^[ABCEGHJ-NPRSTVXY]\d[ABCEGHJ-NPRSTV-Z][ -]?\d[ABCEGHJ-NPRSTV-Z]\d$/i")]

        public string PostalCode { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@".*@.*\.\w{2,}", ErrorMessage = "Plese enter a valid email address.")]
        public string Email { get; set; }

        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number.")]

        public string Phone { get; set; }

        [Required]

        public string Topic { get; set; }

        [Required]
        [MaxLength(300)]

        public string Comments { get; set; }
       
    }

   
}
