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

        public static Beam.BeamTypeEnum ToTekla(this StructuralUsage1D structuralType)
        {
            switch (structuralType)
            {
                case StructuralUsage1D.Undefined:
                    return Beam.BeamTypeEnum.BEAM;
                case StructuralUsage1D.Beam:
                    return Beam.BeamTypeEnum.BEAM;
                case StructuralUsage1D.Column:
                    return Beam.BeamTypeEnum.COLUMN;
                case StructuralUsage1D.Brace:
                    throw new Exception("Brace-Type not implemented");
                case StructuralUsage1D.Cable:
                    throw new Exception("Cable-Type not implemented");
                case StructuralUsage1D.Pile:
                    throw new Exception("Pile-Type not implemented");
                default:
                    return Beam.BeamTypeEnum.BEAM;
            }
        }
    }
}
