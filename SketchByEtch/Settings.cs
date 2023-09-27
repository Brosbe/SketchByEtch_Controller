using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SketchByEtch
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


        public int[] CalculatePosition(int xPos, int yPos)
        {
            if (SwapKnobs)
            {
                int swapper = xPos;
                xPos = yPos;
                yPos = swapper;
            }

            if (ScreenWidth >= ScreenHeight)
            {
                if (UseFullScreen)
                {
                    xPos = (int)(xPos / GetScreenWidthToKnobRatio());
                }
                else
                {
                    xPos = (int)(xPos / GetScreenHeightToKnobRatio());
                    xPos += (ScreenWidth - ScreenHeight) / 2;
                }

                yPos = (int)(yPos / GetScreenHeightToKnobRatio());

                return new[] { xPos, yPos };
            }

            if (UseFullScreen)
            {
                yPos = (int)(yPos / GetScreenHeightToKnobRatio());
            }
            else
            {
                yPos = (int)(yPos / GetScreenWidthToKnobRatio());
                yPos += (ScreenHeight - ScreenWidth) / 2;
            }

            xPos = (int)(xPos / GetScreenWidthToKnobRatio());

            return new[] { xPos, yPos };

        }

        private double GetScreenHeightToKnobRatio()
        {
            return MaxKnobValue / (double)ScreenHeight;
        }

        private double GetScreenWidthToKnobRatio()
        {
            return MaxKnobValue / (double)ScreenWidth;
        }
    }
}
