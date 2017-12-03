using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NcConvert.Models
{
    public class QueryRectangleF : IRectangle
    {
        public float minLon { get; set; }
        public float minLat { get; set; }
        public float maxLon { get; set; }
        public float maxLat { get; set; }

    }
}
