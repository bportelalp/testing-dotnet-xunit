using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestxUnitTraining.Module2.xUnitNet
{
    public static class DirectoryUtilities
    {
        public static bool FilesExist(string[] files, string directory)
        {
            var dirInfo = new DirectoryInfo(directory);
            if (!dirInfo.Exists)
                return false;

            var dirFiles = dirInfo.GetFiles().Select(f => f.Name);
            return !files.Except(dirFiles).Any();
        }
    }
}
