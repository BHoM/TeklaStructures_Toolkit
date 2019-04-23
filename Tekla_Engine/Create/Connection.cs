using System;
using System.Collections.Generic;
using BH.oM.Structure.Elements;
using BH.oM.Geometry;
using BH.oM.Structure.Properties.Section;
using BH.oM.Structure.Properties.Constraint;
using BH.Engine.Geometry;
using BH.Engine.Reflection;



namespace BH.Engine.Structure
{
    public static partial class Create
    {
        /***************************************************/
        /**** Public Methods                            ****/
        /***************************************************/

        public static Connection Connection(string name, List<int> connectingElementIds, List<PanelPlanar> plates)
        {

            return new Connection() { Name = name, ConnectingElementIds = connectingElementIds,  Plates = plates };
        }

        /***************************************************/


    }
}
