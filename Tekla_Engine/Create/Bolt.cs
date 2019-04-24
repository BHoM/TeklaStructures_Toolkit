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

        public static Bolt Bolt(double diamter, Line centerline )
        {

            return new Bolt() { Diameter = diamter, Centerline = centerline };
        }

        /***************************************************/


    }
}
