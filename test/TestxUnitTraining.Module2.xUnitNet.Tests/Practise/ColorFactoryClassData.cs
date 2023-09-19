using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestxUnitTraining.Module2.xUnitNet.Tests.Practise
{
    public class ColorFactoryClassData : TheoryData<Color, string>
    {
        public ColorFactoryClassData()
        {
            Add(Color.Red, "red");
            Add(Color.Blue, "blue");
            Add(Color.Green, "green");
            Add(Color.White, "white");
            Add(Color.Black, "black");
        }
    }
}
