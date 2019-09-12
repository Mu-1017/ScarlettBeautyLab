using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ScarlettBeautyLab.Models
{
    public class RegisterForm
    {
        [Required]
        [Display(Name = "email", Description ="Email Address")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(8)]
        [MaxLength(100)]
        [Display(Name = "password", Description = "Password")]
        public string Password { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(100)]
        [Display(Name = "firstname", Description = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(100)]
        [Display(Name = "lastname", Description = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "birthday", Description = "Birthday")]
        public DateTimeOffset Birthday { get; set; }
    }
}
