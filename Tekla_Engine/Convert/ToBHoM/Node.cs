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

        public static Node ToBHoM(this tsGeo.Point tsPoint)
        {
            Node node = new Node();
            
            node.Coordinates = new oM.Geometry.CoordinateSystem.Cartesian(new Point() { X = tsPoint.X, Y = tsPoint.Y, Z = tsPoint.Z }, new Vector() { X = 1, Y = 0, Z = 0 }, new Vector() { X = 0, Y = 1, Z = 0 }, new Vector() { X = 0, Y = 0, Z = 1 });
            return node;
        }
    }
}
