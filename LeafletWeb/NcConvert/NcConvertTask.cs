using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NcConvert.Models;
using NcConvert.Utils;
using NcConvert.FastJson;

namespace NcConvert
{
    public class NcConvertTask
    {
        private static IList<NcVarModel> models = NcVarsModel.Load();

        private string ncfilepath;              //nc文件路径

        private void Inital(string ncfilepath)
        {
            this.ncfilepath = ncfilepath;
            //1.初步检测参数是否正确
            if (!FileUtil.IsFileExist(ncfilepath))
                throw new Exception("nc文件路径不存在");
        }

        /// <summary>
        /// 区域查找
        /// </summary>
        /// <param name="queryArea">选择的范围，经纬度之间','隔开，"小经,小纬,大经,大纬"</param>
        /// <param name="ncfilepath">nc文件路径</param>
        /// <param name="zoom">地图缩放等级</param>
        public string ZoneLookUp(string queryArea, string ncfilepath, string zoom)
        {
            Inital(ncfilepath);
            IRectangle zoneQueryRect = CoreUtil.GetRectangle<QueryRectangleF>(queryArea);   //前台所要查找的范围
            //2.读取nc文件
            //先读取经纬度信息，确定读取的下标
            NCFileUtil ncFile = new NCFileUtil(ncfilepath);
            NcLonLatModel ncLatLonModel = ncFile.ReadNcLonLat(models[0]);
            int minLonIndex = 0, maxLonIndex = 0, minLatIndex = 0, maxLatIndex = 0;
            CoreUtil.GetIndex(ncLatLonModel.Lon, zoneQueryRect.minLon, zoneQueryRect.maxLon, ref minLonIndex, ref maxLonIndex);
            CoreUtil.GetIndex(ncLatLonModel.Lat, zoneQueryRect.minLat, zoneQueryRect.maxLat, ref minLatIndex, ref maxLatIndex);
            ncLatLonModel.minLonIndex = minLonIndex; ncLatLonModel.minLatIndex = minLatIndex; ncLatLonModel.maxLonIndex = maxLonIndex; ncLatLonModel.maxLatIndex = maxLatIndex;

            QueryNcDataModel queryNcDataModel = new QueryNcDataModel();
            queryNcDataModel.ncLatLonModel = ncLatLonModel;
            queryNcDataModel.StartRow = minLatIndex;
            queryNcDataModel.StartColumn = minLonIndex;
            queryNcDataModel.EndRow = maxLatIndex;
            queryNcDataModel.EndColumn = maxLonIndex;
            queryNcDataModel.RowCount = maxLatIndex - minLatIndex + 1;
            queryNcDataModel.ColumnCount = maxLonIndex - minLonIndex + 1;
            queryNcDataModel.Dir = ncFile.GetValueInArea(models[0].Dir, minLatIndex, minLonIndex, queryNcDataModel.RowCount, queryNcDataModel.ColumnCount);
            queryNcDataModel.Value = ncFile.GetValueInArea(models[0].Value, minLatIndex, minLonIndex, queryNcDataModel.RowCount, queryNcDataModel.ColumnCount);
            queryNcDataModel.Levels = CoreUtil.GetLEVEL(queryNcDataModel.RowCount, queryNcDataModel.ColumnCount, 6);
          
            GFSWeatherJson gfsjson = new GFSWeatherJson();
            string json = gfsjson.createGeoJsonFromNetcdf(queryNcDataModel, int.Parse(zoom));
            return json;
        }
    }
}
