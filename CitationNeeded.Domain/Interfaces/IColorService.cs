using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace CitationNeeded.Domain.Interfaces
{
    public interface IColorService
    {
        Color GenerateColorByString(string s);
    }
}
