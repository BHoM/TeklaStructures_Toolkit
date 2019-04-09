using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BH.oM.Structure.Elements;
using BH.oM.Geometry;

using tsGeo = Tekla.Structures.Geometry3d;

namespace BH.Engine.Tekla
{
    public static partial class Convert
    {
        /***************************************************/
        /**** Public Methods                            ****/
        /***************************************************/

        //Add methods for converting to BHoM from the specific software types. 
        //Only do this if possible to do without any com-calls or similar to the adapter
        //Example:
        //public static Node ToBHoM(this TeklaNode node)
        //{
        //    //Insert code for convertion
        //}

        /***************************************************/

        public static tsGeo.Point ToTekla(this Point point)
        {
            tsGeo.Point tsPoint = new tsGeo.Point() { X = point.X, Y = point.Y, Z = point.Z };
            return tsPoint;
        }
    }
}
