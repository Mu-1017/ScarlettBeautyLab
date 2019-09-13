using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScarlettBeautyLab.Models
{
    [Flags]
    public enum SkinAgeGroups: byte
    {
        LateTeen,
        Mid20s,
        Big30s,
        Mature,
        Aged
    }

    [Flags]
    public enum SkinTypes: byte
    {
        Normal,
        Dry,
        Oily,
        Combination,
        Sensitive
    }
}
