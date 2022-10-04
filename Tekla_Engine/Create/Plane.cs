using System;
using System.Collections.Generic;
using BH.oM.Structure.Elements;
using BH.oM.Geometry;

using BH.Engine.Geometry;
using BH.Engine.Reflection;

using BH.Engine.Base;

namespace BH.Engine.Structure
{
    public static partial class Create
    {
        /***************************************************/
        /**** Public Methods                            ****/
        /***************************************************/

        public static Plane Plane(Vector xAxis, Vector yAxis, Point origin)
        {
                Vector normal = BH.Engine.Geometry.Query.CrossProduct(xAxis,yAxis).Normalise();
                return new Plane { Origin = origin.ShallowClone(), Normal = normal };
        }

        /***************************************************/


    }
}