using System;
using System.Collections.Generic;
using BH.oM.Structure.Elements;
using BH.oM.Geometry;


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
