using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BH.oM.Structure.Elements;
using BH.oM.Structure.Properties.Section;
using BH.oM.Structure.Properties.Constraint;
using BH.oM.Common.Materials;
using BH.Engine.Tekla;

using Tekla.Structures;
using Tekla.Structures.Model;
using Tekla.Structures.Filtering;


namespace BH.Adapter.Tekla
{
    public partial class TeklaAdapter
    {
        private bool CreateCollection(IEnumerable<FramingElement> framingElements)
        {
            bool success = true;

            foreach (FramingElement framing in framingElements)
            {
                //Tip: if the NextId method has been implemented you can get the id to be used for the creation out as (cast into applicable type used by the software):
                int framingId = (int)framing.CustomData[AdapterId];
                //If also the default implmentation for the DependencyTypes is used,
                //one can from here get the id's of the subobjects by calling (cast into applicable type used by the software): 
                //object startNodeId = bar.StartNode.CustomData[AdapterId];
                //object endNodeId = bar.EndNode.CustomData[AdapterId];
                //object SecPropId = bar.SectionProperty.CustomData[AdapterId];

                Beam tsBeam = new Beam(framing.StructuralUsage.ToTekla());//new Beam();
                tsBeam.StartPoint = framing.LocationCurve.Start.ToTekla();
                tsBeam.EndPoint = framing.LocationCurve.End.ToTekla();
                tsBeam.Identifier = new Identifier(framingId);
                tsBeam.Name = framing.Name;
                tsBeam.Position.Plane = Position.PlaneEnum.MIDDLE;
                tsBeam.Position.Depth = Position.DepthEnum.MIDDLE;

                //tsBeam.Type = framing.StructuralUsage.ToTekla();

                Profile p = new Profile();


                tsBeam.Profile.ProfileString = "HEA320";//"SHS150*150*10";// profileName; //<--- this looks like the minimum needed but would be better to set the actual profile

                if (!tsBeam.Insert())
                    success = false;
            }

            return success;

        }

    }
}
