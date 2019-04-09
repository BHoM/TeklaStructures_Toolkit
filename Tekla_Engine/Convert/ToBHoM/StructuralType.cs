using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BH.oM.Structure.Elements;

using Tekla.Structures.Model;


namespace BH.Engine.Tekla
{
    public static partial class Convert
    {
        /***************************************************/
        /**** Public Methods                            ****/
        /***************************************************/

        public static StructuralUsage1D ToBHoM(this Beam.BeamTypeEnum structuralType)
        {
            switch (structuralType)
            {
                case Beam.BeamTypeEnum.BEAM:
                    return StructuralUsage1D.Beam;
                case Beam.BeamTypeEnum.PANEL:
                    throw new Exception("Panel-Type not implemented");
                case Beam.BeamTypeEnum.STRIP_FOOTING:
                    throw new Exception("StripFooting-Type not implemented");
                case Beam.BeamTypeEnum.PAD_FOOTING:
                    throw new Exception("PadFooting-Type not implemented");
                case Beam.BeamTypeEnum.COLUMN:
                    return StructuralUsage1D.Column;
                default:
                    return StructuralUsage1D.Beam;
            }
        }

    }
}
