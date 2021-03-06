﻿using System;
using System.Drawing;

namespace CitationNeeded.Extensions
{
    public static class ColorExtensions
    {
        public static string ToHex(this Color color) 
            => $"#{color.R:X2}{color.G:X2}{color.B:X2}";
    }
}
