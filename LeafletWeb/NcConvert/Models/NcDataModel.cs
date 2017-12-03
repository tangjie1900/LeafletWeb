using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NcConvert.Models
{
    public class NcLonLatModel : IRectangle
    {
        public float minLon { get; set; }
        public float minLat { get; set; }
        public float maxLon { get; set; }
        public float maxLat { get; set; }

        public int minLonIndex { get; set; }
        public int minLatIndex { get; set; }
        public int maxLonIndex { get; set; }
        public int maxLatIndex { get; set; }
      
        public float[] Lon { get; set; }
        public float[] Lat { get; set; }
    }

    public class NcDataModel : NcLonLatModel
    {
        public int[] TimeSteps { get; set; }
        public float[][] Value { get; set; }
        public float[][] Dir { get; set; }
    }
}
