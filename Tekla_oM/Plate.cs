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
    public class Plate : BHoMObject
    {
        //type Enum 

        public virtual double Thickness { get; set; }

        public virtual Polyline Contour { get; set; }

        public virtual Common.Materials.Material Material { get; set;}

    }

}
