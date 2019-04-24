using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BH.oM.Base;
using BH.oM.Geometry;
using BH.oM.Structure.Properties.Section;
using BH.oM.Structure.Properties.Constraint;
using BH.oM.Structure.Properties;

namespace BH.oM.Structure.Elements
{
    public class Bolt : BHoMObject
    {
        public double Diameter { get; set; }

        public string Standard { get; set; }

        public Line Centerline { get; set; }

    }

}
