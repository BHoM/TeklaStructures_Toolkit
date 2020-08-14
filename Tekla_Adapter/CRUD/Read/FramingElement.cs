using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using BH.Engine.Adapters.Tekla;
using BH.oM.Physical.Elements;
using BH.oM.Physical.FramingProperties;
using Tekla.Structures.Solid;
using Tekla.Structures.Geometry3d;

using Tekla.Structures;
using tsModel = Tekla.Structures.Model;
using Tekla.Structures.Filtering;
using tsGeo = Tekla.Structures.Geometry3d;

namespace BH.Adapter.Tekla
{
    public partial class TeklaAdapter
    {

        private List<IFramingElement> ReadFramingElements(List<string> ids = null)
        {
            //Tip: If the software stores depending types such as Nodes and SectionProperties in separate object tables,
            //it might be a massive preformance boost to read in and store these properties before reading in the bars 
            //and referenced these stored objects instead of reading them in each time.
            //For example, a case where 1000 bars share 5 total number of different SectionProperties you want, if possible,
            //to only read in the section properties 5 times, not 1000. This might of course vary from software to software.

            //Implement code for reading bars

            List<tsModel.Beam> tsBeamList = new List<tsModel.Beam>();
            List<IFramingElement> bhFramingList = new List<IFramingElement>();

            if (ids == null)
            {
                foreach (tsModel.ModelObject tsObj in m_ObjectSelector.GetAllObjectsWithType(tsModel.ModelObject.ModelObjectEnum.BEAM))
                {
                    tsBeamList.Add(tsObj as tsModel.Beam);
                }
            }
            else
            {
                //there might be some efficiency gain in understanding how to use: selection with ExpressionFilter, i.e. 'm_ObjectSelector.GetObjectsByFilter()'
                foreach (string id in ids)
                {
                    int idNum = System.Convert.ToInt32(id);
                    tsModel.ModelObject tsObj = m_TeklaModel.SelectModelObject(new Identifier(idNum));

                    tsBeamList.Add(tsObj as tsModel.Beam);
                }
            }

            foreach (tsModel.Beam tsBeam in tsBeamList)
            {
                IFramingElement framing = tsBeam.ToBHoM();

                //framing.LocationCurve = new oM.Geometry.Line() { Start = tsBeam.StartPoint.ToBHoM(), End = tsBeam.EndPoint.ToBHoM() };
                //framing.Name = tsBeam.Name;
                //framing.CustomData[AdapterId] = tsBeam.Identifier.ID;
                //framing.StructuralUsage = tsBeam.Type.ToBHoM();
                ////framing.Property = tsBeam.Profile.ToBHoM();// not implemented yet - should only be added when ready for analytical elements

                bhFramingList.Add(framing);

                framing.CustomData.Add("lines", BH.Engine.Adapters.Tekla.Query.PartGeo(tsBeam));
            }

            return bhFramingList;
        }

    }
}
