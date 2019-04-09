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

                Beam tsBeam = new Beam(framing.StructuralUsage.ToTekla());
                tsBeam.StartPoint = framing.LocationCurve.Start.ToTekla();
                tsBeam.EndPoint = framing.LocationCurve.End.ToTekla();
                tsBeam.Identifier = new Identifier(framingId);
                tsBeam.Name = framing.Name;
                tsBeam.Position.Plane = Position.PlaneEnum.MIDDLE;
                tsBeam.Position.Depth = Position.DepthEnum.MIDDLE;

                if (m_ProfileLibrary.Contains(framing.Property.Name))
                {
                    tsBeam.Profile.ProfileString = framing.Property.Name;
                }
                else
                {
                    //add warning that profile does not exist and standard section has been used
                    Engine.Reflection.Compute.RecordWarning("Profile " + framing.Property.Name + " was not found in library - replaced with: " + m_ProfileLibrary[0]);

                    tsBeam.Profile.ProfileString = m_ProfileLibrary[0];// "HEA320";
                }

                if (!tsBeam.Insert())
                    success = false;
            }

            return success;

        }

    }
}
