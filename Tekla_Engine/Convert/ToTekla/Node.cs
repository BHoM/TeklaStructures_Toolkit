using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BH.oM.Structure.Elements;

using tsGeo = Tekla.Structures.Geometry3d;


namespace BH.Engine.Tekla
{
    public static partial class Convert
    {
        /***************************************************/
        /**** Public Methods                            ****/
        /***************************************************/

        //Add methods for converting From BHoM to the specific software types, if possible to do without any BHoM calls
        //Example:
        //public static TeklaNode ToTekla(this Node node)
        //{
        //    //Insert code for convertion
        //}

        /***************************************************/

        public static tsGeo.Point ToTeklaPoint(this Node node)
        {
            return new tsGeo.Point(node.Coordinates.Origin.X, node.Coordinates.Origin.Y, node.Coordinates.Origin.Z);
        }
    }
}
