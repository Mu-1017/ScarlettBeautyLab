using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ScarlettBeautyLab.Models
{
    public class User: Resource
    {
        public string Email { get; set; }
        public string Nickname { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public SkinAgeGroups AgeGroup { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public SkinTypes SkinType { get; set; }
    }
}
