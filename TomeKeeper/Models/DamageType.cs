using System;
using System.Collections.Generic;
using System.Text;

namespace TomeKeeper.Models
{
    internal sealed class DamageType
    {
        public DamageType() { }
        public string Index { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
    }
}
