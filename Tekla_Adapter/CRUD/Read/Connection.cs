using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BH.oM.Structure.Elements;
using BH.Engine.Adapters.Tekla;

using Tekla.Structures;
using tsModel = Tekla.Structures.Model;
using Tekla.Structures.Filtering;
using tsGeo = Tekla.Structures.Geometry3d;

namespace BH.Adapter.Tekla
{
    public partial class TeklaAdapter
    {

        private List<Connection> ReadConnections(List<string> ids = null)
        {

            // create functionality to read Connections

            List<Connection> bhConnectionList = new List<Connection>();

            // do something ! ! ! ! ! 


            return bhConnectionList;
        }

    }
}
