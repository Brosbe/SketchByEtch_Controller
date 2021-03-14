using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace etchASketch
{
    public class Settings
    {
        public bool SwapKnobs { get; set; }
        public bool InvertX { get; set; }
        public bool InvertY { get; set; }
        public bool UseFullScreen { get; set; }
        public int MaxKnobValue { get; set; }
        public int ScreenWidth { get; set; }
        public int ScreenHeight { get; set; }

        //at the moment this only accounts for a screen in landscape mode. support for portrait mode will come later
        public Point CalculatePosition(string potString)
        {
            var potSplit = potString.Split(',');
            int[] potInts;
            //shorthand if statement. intellisense recommended it to me. it's pretty dope
            //this checks whether the knob logic should be swapped. it allows for the same calculations to be used in both situations.
            potInts = SwapKnobs ? new[] { int.Parse(potSplit[1]), int.Parse(potSplit[0]) } : new[] { int.Parse(potSplit[0]), int.Parse(potSplit[1]) };
            ConvertIntsToTruePositions(potInts);
            var margin = UseFullScreen ? 0 : (ScreenWidth - ScreenHeight) / 2;

            potInts[0] = InvertX ? ScreenWidth - margin - potInts[0] : margin + potInts[0];
            if (InvertY)
            {
                potInts[1] = ScreenHeight - potInts[1];
            }

            return new Point(potInts[0], potInts[1]);

        }

        private double GetScreenHeightToKnobRatio()
        {
            return MaxKnobValue / (double)ScreenHeight;
        }

        private double GetScreenWidthToKnobRatio()
        {
            return MaxKnobValue / (double)ScreenWidth;
        }

        private void ConvertIntsToTruePositions(int[] inputArray)
        {
            //checks what ratio to use of X axis, depending on if you want to use the entire screen or just the center square part of it
            inputArray[0] = (int)(inputArray[0] / (UseFullScreen ? GetScreenWidthToKnobRatio() : GetScreenHeightToKnobRatio()));
            inputArray[1] = (int)(inputArray[1] / GetScreenHeightToKnobRatio());
        }
    }
}
