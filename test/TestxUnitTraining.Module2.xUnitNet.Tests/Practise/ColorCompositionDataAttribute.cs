using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xunit.Sdk;

namespace TestxUnitTraining.Module2.xUnitNet.Tests.Practise
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple =true, Inherited = false)]
    public class ColorCompositionDataAttribute : DataAttribute
    {
        private readonly string _path;

        public ColorCompositionDataAttribute(string path)
        {
            _path = path;
        }

        public override IEnumerable<object[]> GetData(MethodInfo testMethod)
        {
            using var sr = new StreamReader(_path);
            string all = sr.ReadToEnd();
            InputDataJson json = JsonConvert.DeserializeObject<InputDataJson>(all)!;
            foreach (var colorCompo in json.GetColorCompositionData)
            {
                yield return new object[] { colorCompo.R, colorCompo.G, colorCompo.B };
            }

        }


        private class InputDataJson
        {
            public Composition[] GetColorCompositionData { get; set; } = null!;
        }

        public class Composition
        {
            public int R { get; set; }
            public int G { get; set; }
            public int B { get; set; }
        }

    }
}
