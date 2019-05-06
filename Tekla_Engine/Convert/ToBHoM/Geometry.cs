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

        public static Point ToBHoM(this tsGeo.Point tsPoint)
        {
            Point point = new Point() { X = tsPoint.X, Y = tsPoint.Y, Z = tsPoint.Z };
            return point;
        }

        public static Vector ToBHoM(this tsGeo.Vector tsVector)
        {
            Vector vect = new Vector() { X = tsVector.X, Y = tsVector.Y, Z = tsVector.Z };
            return vect;
        }

        public static Plane ToBHoMPlane(this tsGeo.GeometricPlane tsPlane)
        {
            Plane plane = new Plane() { Origin = BH.Engine.Tekla.Convert.ToBHoM(tsPlane.Origin), Normal = BH.Engine.Tekla.Convert.ToBHoM(tsPlane.Normal) };

            return plane;
        }


    }
}
