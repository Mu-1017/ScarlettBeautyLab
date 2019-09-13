using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace ScarlettBeautyLab.Models
{
    public class ProductEntity
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public List<SkinAgeGroups> ForAgeGroups { get; set; }
        public List<SkinTypes> ForSkinTypes { get; set; }

        [ForeignKey(nameof(ImageEntity))]
        public Guid ImageId { get; set; }

        [ForeignKey(nameof(BrandEntity))]
        public Guid BrandId { get; set; }

    }
}
