using CitationNeeded.Domain.Interfaces;
using System;
using System.Drawing;

namespace CitationNeeded.WebApp.Services
{
    public class StringRandomColorService : IColorService
    {
        public Color GenerateColorByString(string s)
        {
            var random = new Random(s.GetHashCode());
            return Color.FromArgb(random.Next(0, 256), random.Next(0, 256), random.Next(0, 256));
        }
    }
}
