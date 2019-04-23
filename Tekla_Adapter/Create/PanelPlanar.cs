using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BH.oM.Structure.Elements;
using BH.oM.Structure.Properties;
using BH.oM.Structure.Properties.Section;
using BH.oM.Structure.Properties.Constraint;
using BH.Engine.Tekla;
using BH.oM.Geometry;
using BH.oM.Structure.Properties.Surface;

using Tekla.Structures;
using Tekla.Structures.Model;
using tsGeo = Tekla.Structures.Geometry3d;

namespace BH.Adapter.Tekla
{
    public partial class TeklaAdapter
    {

        /***************************************************/
        /**** Private methods                           ****/
        /***************************************************/

        private bool CreateCollection(IEnumerable<PanelPlanar> PanelPlanar)
        {
            //Code for creating a collection of panel planar in the software

            bool success = true;

            foreach (PanelPlanar p in PanelPlanar)
            {
                ////Create a Plate Profile
                ContourPlate countourPlate = new ContourPlate();
                Profile pfl = new Profile();
                foreach(Point point in Engine.Structure.Query.ControlPoints(p))
                    countourPlate.Contour.AddContourPoint(new ContourPoint(new tsGeo.Point(point.X, point.Y, point.Z), new Chamfer()));

                ////Check if the section property already exists, if not, create and apply

                if (!m_ProfileLibrary.Contains(p.Property.Name))
                {
                    List<ISurfaceProperty> asdf = new List<ISurfaceProperty>();
                    asdf.Add(p.Property);
                    Create(asdf);
                }

                if (p.Property is ConstantThickness)
                {
                    ConstantThickness ct = (ConstantThickness) p.Property;
                    countourPlate.Profile.ProfileString = "PL" + ct.Thickness.ToString();
                }

                if (!countourPlate.Insert())
                {
                    success = false;
                }
            }
            return success;
        }

        /***************************************************/

    }
}
