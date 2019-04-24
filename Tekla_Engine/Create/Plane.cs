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

        public static Plane Plane(Vector xAxis, Vector yAxis, Point origin)
        {
                Vector normal = BH.Engine.Geometry.Query.CrossProduct(xAxis,yAxis).Normalise();
                return new Plane { Origin = origin.Clone(), Normal = normal };
        }

        /***************************************************/


    }
}