using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Readearth.NetCDF;
using NcConvert.Models;

namespace NcConvert.Utils
{
    class NCFileUtil
    {
        public NCFileUtil(string filepath)
        {
            this.m_datafile = new CDLFile(filepath);
        }

        private float[] lon, lat;

        private float[][] power, dir;

        private int[] time;

        private CDLFile m_datafile;

        /// <summary>
        /// 读取所有数据
        /// </summary>
        public NcDataModel ReadAllData(IList<NcVarModel> models)
        {
            //   var models = NcVarsModel.Load();
            for (int i = 0; i < models.Count; i++)
            {
                NcVarModel model = models[i];
                try
                {
                    ReadNcLonLat(model);
                    m_datafile.Read(model.TimeSteps);

                    lon = m_datafile.m_Variables[model.LonVarName].f_1dim;
                    lat = m_datafile.m_Variables[model.LatVarName].f_1dim;
                    time = m_datafile.m_Variables[model.TimeSteps].i_1dim;

                    for (int j = 0; j < (int)time.Length / 6; j++)
                    {
                        m_datafile.Read(i, model.Value);
                        m_datafile.Read(i, model.Dir);
                        power[j] = m_datafile.m_Variables[model.Value].f_1dim;
                        dir[j] = m_datafile.m_Variables[model.Dir].f_1dim;
                    }

                    return new NcDataModel()
                    {
                        Lon = lon,
                        Lat = lat,
                        TimeSteps = time,
                        Value = power,
                        Dir = dir
                    };
                }
                catch (Exception)
                {

                }
            }
            return null;
        }

        public NcLonLatModel ReadNcLonLat(NcVarModel model)
        {
            m_datafile.Open();
            m_datafile.Read(model.LonVarName);
            m_datafile.Read(model.LatVarName);
            lon = m_datafile.m_Variables[model.LonVarName].f_1dim;
            lat = m_datafile.m_Variables[model.LatVarName].f_1dim;
            return new NcLonLatModel()
            {
                minLon = lon[0],
                minLat = lat[0],
                maxLon = lon[lon.Length - 1],
                maxLat = lat[lat.Length - 1],

                Lon = lon,
                Lat = lat,
            };
        }

        public float[] GetValueInArea(string varName, int startRow, int startColumn, int rowcount, int columnCount)
        {
            return m_datafile.ReadVarFloat1dim(varName, startRow, startColumn, rowcount, columnCount);
        }

        public void Close()
        {
            m_datafile.Close();
        }

    }
}
