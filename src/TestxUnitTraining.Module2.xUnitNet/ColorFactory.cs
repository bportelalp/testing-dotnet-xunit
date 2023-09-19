using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestxUnitTraining.Module2.xUnitNet
{
    public static class ColorFactory
    {

        public static Color GetColorByName(string nombre)
        {
            if (nombre is null)
            {
                throw new ArgumentNullException(nameof(nombre));
            }

            return nombre.ToLower() switch
            {
                "red" => Color.Red,
                "blue" => Color.Blue,
                "green" => Color.Green,
                "white" => Color.White,
                "black" => Color.Black,
                _ => default,
            };
        }

        public static Color GetColorComposition(int red, int green, int blue)
        {
            if (red < 0 || red > 255)
            {
                throw new ArgumentOutOfRangeException(nameof(red), "Red is out of range. It must be between 0 and 255");
            }

            if (green < 0 || green > 255)
            {
                throw new ArgumentOutOfRangeException(nameof(green), "Green is out of range. It must be between 0 and 255");
            }

            if (blue < 0 || blue > 255)
            {
                throw new ArgumentOutOfRangeException(nameof(blue), "Blue is out of range. It must be between 0 and 255");
            }

            return Color.FromArgb(red, green, blue);
        }
    }
}
