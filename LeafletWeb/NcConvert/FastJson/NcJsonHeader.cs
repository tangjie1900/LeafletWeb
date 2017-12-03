using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NcConvert.FastJson
{
    public class NcJsonHeader
    {
        private static long serialVersionUID = 60396446737407099L;

        public int discipline = 0;

        public string disciplineName = "Meteorological products";

        public int gribEdition;

        public int gribLength;

        public int center = 7;

        public string centerName = "US National Weather Service - NCEP(WMC)";

        public int subcenter = 0;

        public string refTime = "2016-09-14T08:00:00.000Z";

        public int significanceOfRT = 1;

        public string significanceOfRTName = "Start of forecast";

        public int productStatus = 0;

        public string productStatusName = "Operational products";

        public int productType = 1;

        public string productTypeName = "Forecast products";

        public string productDefinitionTemplate = "productDefinitionTemplate";

        public string productDefinitionTemplateName = "Analysis/forecast at horizontal level/layer at a point in time";

        public int parameterCategory = 2;

        public string parameterCategoryName = "Momentum";

        public int parameterNumber;

        public string parameterNumberName;

        public string parameterUnit = "m.s-1";

        public int genProcessType = 2;

        public string genProcessTypeName = "Forecast";

        public int forecastTime = 0;

        public int surface1Type = 103;

        public string surface1TypeName = "Specified height level above ground";

        public int surface1Value = 10;

        public int surface2Type = 255;

        public string surface2TypeName = "Missing";

        public int surface2Value = 0;

        public int gridDefinitionTemplate = 0;

        public string gridDefinitionTemplateName = "Latitude_Longitude";

        public int numberPoints;

        public int shape = 0;

        public string shapeName = "Earth spherical with radius of 6,371,229.0 m";

        public string gridUnits = "degrees";
        public string winds = "true";
        public int scanMode = 0;
        public int nx;
        public int ny;
        public int basicAngle = 0;
        public int subDivisions = 0;
        public double lo1;
        public double la1;
        public double lo2;
        public double la2;
        public double dx;
        public double dy;
    }

}
