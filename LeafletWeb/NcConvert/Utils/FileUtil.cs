using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace NcConvert.Utils
{
    class FileUtil
    {
        public static bool IsFileExist(string filepath)
        {
            return File.Exists(filepath);
        }

        public static void Write(string text)
        {
            File.WriteAllText(@"d:\what.json", text,Encoding.UTF8);
        }

    }
}
