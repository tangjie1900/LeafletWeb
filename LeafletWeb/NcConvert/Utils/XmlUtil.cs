using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;

namespace NcConvert.Utils
{
    public static class XmlUtil
    {
        public static void Serialize<T>(T o, string filePath)
        {
            try
            {
                XmlSerializer formatter = new XmlSerializer(typeof(T));
                StreamWriter sw = new StreamWriter(filePath, false);
                formatter.Serialize(sw, o);
                sw.Flush();
                sw.Close();
            }
            catch (Exception) { }
        }

        public static T DeSerialize<T>(string filePath)
        {
            try
            {
                XmlSerializer formatter = new XmlSerializer(typeof(T));
                StreamReader sr = new StreamReader(filePath);
                T o = (T)formatter.Deserialize(sr);
                sr.Close();
                return o;
            }
            catch (Exception)
            {
            }
            return default(T);
        }
    }
}
