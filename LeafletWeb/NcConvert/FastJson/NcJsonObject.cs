using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NcConvert.FastJson
{
     class NcJsonObject
    {
        public NcJsonObject()
        {
            meta = new NcJsonMeta();
            meta.date = "2016-09-14T08:00:00.000Z";
        }

        private static long serialVersionUID = -8018539429381292589L;

        public NcJsonHeader header;
        public NcJsonMeta meta;
        public double[] data;
    }
}
