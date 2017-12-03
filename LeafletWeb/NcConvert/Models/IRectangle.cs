using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NcConvert.Models
{
    public interface IRectangle : IModel
    {
        float minLon { get; set; }
        float minLat { get; set; }
        float maxLon { get; set; }
        float maxLat { get; set; }
    }
}
