using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NcConvert.Models;
using NcConvert.Utils;

namespace NcConvert.FastJson
{
    class GFSWeatherJson
    {
        public string createGeoJsonFromNetcdf(QueryNcDataModel queryNcDataModel, int zoom)
        {
            int countZoom = 0;
            for (int i = 0; i < queryNcDataModel.Levels.Length; i++)
            {
                if (queryNcDataModel.Levels[i] == 3)
                    countZoom++;
            }

            NcJsonObject uObject = new NcJsonObject();
            NcJsonHeader uHeader = new NcJsonHeader();
            uHeader.parameterNumber = 2;
            uHeader.parameterNumberName = "U-7";
            uHeader.numberPoints = queryNcDataModel.ncLatLonModel.Lat.Length * queryNcDataModel.ncLatLonModel.Lon.Length;
            uHeader.nx = queryNcDataModel.ncLatLonModel.Lon.Length;
            uHeader.ny = queryNcDataModel.ncLatLonModel.Lat.Length;
            uHeader.lo1 = queryNcDataModel.ncLatLonModel.minLon;
            uHeader.lo2 = queryNcDataModel.ncLatLonModel.maxLon;
            uHeader.la1 = queryNcDataModel.ncLatLonModel.maxLat;
            uHeader.la2 = queryNcDataModel.ncLatLonModel.minLat;
            uHeader.dx = 1;
            uHeader.dy = 1;
            uObject.header = uHeader;
            uObject.data = new double[countZoom];


            NcJsonObject vObject = new NcJsonObject();
            NcJsonHeader vHeader = new NcJsonHeader();
            vHeader.parameterNumber = 3;
            vHeader.parameterNumberName = "V-component_of_wind";
            vHeader.numberPoints = queryNcDataModel.ncLatLonModel.Lat.Length * queryNcDataModel.ncLatLonModel.Lon.Length;
            vHeader.nx = queryNcDataModel.ncLatLonModel.Lon.Length;
            vHeader.ny = queryNcDataModel.ncLatLonModel.Lat.Length;
            vHeader.lo1 = queryNcDataModel.ncLatLonModel.minLon;
            vHeader.lo2 = queryNcDataModel.ncLatLonModel.maxLon;
            vHeader.la1 = queryNcDataModel.ncLatLonModel.maxLat;
            vHeader.la2 = queryNcDataModel.ncLatLonModel.minLat;
            vHeader.dx = 1;
            vHeader.dy = 1;
            vObject.header = vHeader;
            vObject.data = new double[countZoom];

            DateTime start = DateTime.Now;

            int idx = 0;
            float u = 0f, v = 0f;
            for (int j = 0; j < queryNcDataModel.Value.Length; j++)
            {
                if (queryNcDataModel.Levels[j] != zoom)
                {
                    continue;
                }

                float _dir = queryNcDataModel.Dir[j];
                float _power = queryNcDataModel.Value[j];
                if (!float.IsNaN(_power))
                {
                    CalUV(ref u, ref v, (double)_dir, (double)_power);
                    //uHeader  power
                    vObject.data[idx] = saveValidNumber(v, 2);
                    //vHeader  dir
                    uObject.data[idx] = saveValidNumber(u, 2);
                }
                else
                {
                    //uHeader  power
                    vObject.data[idx] = float.NaN;
                    //vHeader  dir
                    uObject.data[idx] = float.NaN;
                }
                idx++;
            }

            List<NcJsonObject> objectFiles = new List<NcJsonObject>();
            objectFiles.Add(vObject);
            objectFiles.Add(uObject);

            string json = JsonUtil.JsonSerialize<NcJsonObject>(objectFiles);
            return json;
        }

        private double rad = Math.PI / 180.0;

        private void CalUV(ref float u, ref float v, double _dir, double s)
        {
            var dd = _dir * rad;
            double d_dd = (double)dd;
            u = (float)(s * Math.Sin(d_dd));
            v = (float)(s * Math.Cos(d_dd));
        }


        private double saveValidNumber(double value, int validCount)
        {
            String df = ".";
            for (int i = 0; i < validCount; i++)
            {
                df = df + "0";
            }
            return (Math.Round(value, 2));
        }
    }
}
