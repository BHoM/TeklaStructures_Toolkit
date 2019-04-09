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

        public static Connection Connection(string libraryName, List<int> connectingElementIds)
        {
            return new Connection() { LibraryName = libraryName, ConnectingElementIds = connectingElementIds };
        }

        /***************************************************/


    }
}
