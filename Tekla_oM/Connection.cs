using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BH.oM.Base;
using BH.oM.Geometry;
using BH.oM.Structure.Properties;
using BH.oM.Structure.Elements;

namespace BH.oM.Structure.Elements
{
    public class Connection : BHoMObject
    {
        public string LibraryName { get; set; }

        public List<int> ConnectingElementIds { get; set; }

    }

}