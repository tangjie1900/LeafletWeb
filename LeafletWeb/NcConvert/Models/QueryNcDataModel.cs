using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NcConvert.Models
{
    class QueryNcDataModel 
    {
        public int StartRow { get; set; }
        public int StartColumn { get; set; }

        public int EndRow { get; set; }
        public int EndColumn { get; set; }

        public int RowCount { get; set; }
        public int ColumnCount { get; set; }

        public float[] Value { get; set; }
        public float[] Dir { get; set; }

        public NcLonLatModel ncLatLonModel { get; set; }

        public int[] Levels { get; set; }
    }
}
