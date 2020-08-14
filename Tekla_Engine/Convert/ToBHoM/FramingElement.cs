using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BH.oM.Physical.Elements;
using BH.oM.Physical.FramingProperties;

using tsModel = Tekla.Structures.Model;


namespace BH.Engine.Adapters.Tekla
{
    public static partial class Convert
    {
        /***************************************************/
        /**** Public Methods                            ****/
        /***************************************************/

        public static IFramingElement ToBHoM(this tsModel.Beam tsBeam)
        {
            IFramingElement framing;

            if (tsBeam.Type == tsModel.Beam.BeamTypeEnum.BEAM)
                framing = new Beam();
            else if (tsBeam.Type == tsModel.Beam.BeamTypeEnum.COLUMN)
                framing = new Column();
            else if (tsBeam.Type == tsModel.Beam.BeamTypeEnum.PAD_FOOTING)
                throw new Exception("pad footing not implemented");
            else
                throw new Exception("something went wrong with a framing element");

            framing.Location = new BH.oM.Geometry.Polyline() { ControlPoints = new List<oM.Geometry.Point>() { tsBeam.StartPoint.ToBHoM(), tsBeam.EndPoint.ToBHoM() } };
            framing.Name = tsBeam.Name;
            framing.CustomData[AdapterIdName] = tsBeam.Identifier.ID;
            framing.Property = new ConstantFramingProperty() { Name = tsBeam.Profile.ProfileString };

            return framing;
        }

    }
}
