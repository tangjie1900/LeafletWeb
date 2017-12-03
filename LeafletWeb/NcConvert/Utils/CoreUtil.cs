using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NcConvert.Models;

namespace NcConvert.Utils
{
    class CoreUtil
    {
        public static T GetRectangle<T>(string area) where T : IRectangle, new()
        {
            string[] lonlats = area.Split(',');
            try
            {
                float minlon = float.Parse(lonlats[0]);
                float minLat = float.Parse(lonlats[1]);
                float maxLon = float.Parse(lonlats[2]);
                float maxLat = float.Parse(lonlats[3]);
                return new T() { minLon = minlon, minLat = minLat, maxLon = maxLon, maxLat = maxLat };
            }
            catch (Exception)
            {
                throw new Exception("选择的范围经纬度信息格式不正确");
            }
        }

        public static void GetIndex(float[] f, float minValue, float maxValue, ref int minIndex, ref int maxIndex)
        {
            minIndex = 0;
            maxIndex = 0;

            for (int i = 1; i < f.Length - 1; i++)
            {
                if (f[i - 1] <= minValue && f[i] > minValue)
                    minIndex = i - 1;
                if (f[i - 1] < maxValue && f[i] > maxValue)
                    maxIndex = i;
            }

            if (minValue <= f[0])
                minIndex = 0;
            if (maxValue >= f[f.Length - 1])
                maxIndex = f.Length - 1;

            minIndex = minIndex >= 1 ? minIndex - 1 : 0;
            maxIndex = maxIndex <= f.Length - 2 ? maxIndex + 1 : f.Length - 1;
        }

        public static int[] GetLEVEL(int row, int col, int levelcount)
        {
            int[,] level = new int[row, col];
            int[] ls = new int[row * col];
            int count = 0;
            for (int i = 1; i < levelcount; i++)
            {
                int interval = (int)Math.Pow(2, i);
                for (int ii = 0; ii < row; ii += interval)
                    for (int jj = 0; jj < col; jj += interval)
                        ls[count++] = i;
            }
            return ls;
        }
    }
}
