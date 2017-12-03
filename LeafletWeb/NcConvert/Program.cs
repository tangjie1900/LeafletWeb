using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NcConvert
{
    public class Program
    {
        private NcConvertTask ncConvertTask;

        public string Excute(string area, string ncfilepath)
        {
            if (ncConvertTask == null)
            {
                ncConvertTask = new NcConvertTask();
            }

            try
            {
                //"60,-50,280,50", @"D:\CURRENT_2017081002.nc"
                string json = ncConvertTask.ZoneLookUp(area, @"D:\WIND_2017081002.nc", "3");
                return json;
            }
            catch (Exception)
            {
                return "exception";
            }
        }
    }
}
