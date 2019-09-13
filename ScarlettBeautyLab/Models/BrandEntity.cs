using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ScarlettBeautyLab.Models
{
    public class BrandEntity
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Story { get; set; }
        [ForeignKey(nameof(ImageEntity))]
        public Guid ImageId { get; set; }
    }
}
