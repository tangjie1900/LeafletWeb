using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace NcConvert.Utils
{
    public class JsonUtil
    {
        public static string JsonSerialize<T>(List<T> models)
        {
            try
            {
                return JsonConvert.SerializeObject(models);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
