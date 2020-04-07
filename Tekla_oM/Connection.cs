using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BH.oM.Base;
using BH.oM.Geometry;
using BH.oM.Structure.Elements;

namespace BH.oM.Structure.Elements
{
    public class Connection : BHoMObject
    {
        public virtual List<int> ConnectingElementIds { get; set; } = null;

        public virtual List<Panel> Plates { get; set; } = null;

        public virtual List<Bolt> Bolts { get; set; } = null;

        public virtual List<Weld> Welds { get; set; } = null;

        public virtual List<Cut> Cuts { get; set; } = null;

    }

}
