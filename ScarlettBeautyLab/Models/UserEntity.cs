using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ScarlettBeautyLab.Models
{
    public class UserEntity: IdentityUser<Guid>
    {
        public string Nickname { get; set; }

        [Column(TypeName = "nvarchar(24)")]
        public SkinAgeGroups AgeGroup { get; set; }

        [Column(TypeName = "nvarchar(24)")]
        public SkinTypes SkinType { get; set; }
    }
}
