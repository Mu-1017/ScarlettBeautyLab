using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScarlettBeautyLab.Models
{
    public class OwnerInfo : Resource
    {
        public string Name { get; set; }
        public string Tagline { get; set; }
        public string Email { get; set; }
        public SkinAgeGroups SkinAge { get; set; }
        public SkinTypes SkinType { get; set; }
    }
}
