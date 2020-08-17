using System;
using System.Collections.Generic;
using BH.oM.Physical.Elements;
using BH.oM.Structure.Elements;
using BH.oM.Geometry;

using BH.Engine.Geometry;
using BH.Engine.Reflection;

namespace BH.Engine.Structure
{
    public static partial class Create
    {
        /***************************************************/
        /**** Public Methods                            ****/
        /***************************************************/

        public static Weld Weld(ICurve curve, IFramingElement objectWeldedTo, IFramingElement objectWelded)
        {
            return new Weld { weldPath = curve, objWelded = objectWelded, objWeldedTo = objectWeldedTo };
        }

        /***************************************************/


    }
}