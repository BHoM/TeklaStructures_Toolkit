using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BH.oM.Physical.Elements;
using tsModel = Tekla.Structures.Model;


namespace BH.Engine.Adapters.Tekla
{
    public static partial class Convert
    {
        /***************************************************/
        /**** Public Methods                            ****/
        /***************************************************/

        public static tsModel.Beam.BeamTypeEnum ToTeklaFramingType(this IFramingElement framing)
        {
            string typeString = framing.GetType().ToString();
            switch (typeString)
            {
                case "Beam":
                    return tsModel.Beam.BeamTypeEnum.BEAM;
                case "Column":
                    return tsModel.Beam.BeamTypeEnum.COLUMN;
                case "Bracing":
                    throw new Exception("Brace-Type not implemented");
                case "Cable":
                    throw new Exception("Cable-Type not implemented");
                default:
                    return tsModel.Beam.BeamTypeEnum.BEAM;
            }
        }
    }
}
