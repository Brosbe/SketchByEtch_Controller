using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace etchASketch
{
    static class Extensions
    {
        public static Point ToPoint(this string input)
        {
            string[] xAndY = input.Split(',');
            return new Point(int.Parse(xAndY[0]), int.Parse(xAndY[1]));
        }
    }
}
