using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScarlettBeautyLab.Models
{
    public class ImageEntity
    {
        public Guid Id { get; set; }
        public byte[] Image { get; set; }
        public string ImageContentType { get; set; }
    }
}
