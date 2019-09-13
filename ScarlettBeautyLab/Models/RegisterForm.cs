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
        [Display(Name = "nickname", Description = "Nickname")]
        public string Nickname { get; set; }

        [Required]
        [Display(Name = "agegroup", Description = "Skin age group")]
        public SkinAgeGroups AgeGroup { get; set; }

        [Required]
        [Display(Name = "skintype", Description = "Skin type")]
        public SkinTypes SkinType { get; set; }
    }
}
