using System;
using System.Collections.Generic;
using BH.oM.Structure.Elements;
using BH.oM.Geometry;
using BH.oM.Base;

using BH.Engine.Geometry;
using BH.Engine.Reflection;

namespace BH.Engine.Structure
{
    public static partial class Create
    {
        /***************************************************/
        /**** Public Methods                            ****/
        /***************************************************/

        public static Cut Cut(ICurve curve , BHoMObject obj, double thickness)
        {
            return new Cut { curve = curve, cutObject = obj, thickness = thickness};
        }

        /***************************************************/
    }
}