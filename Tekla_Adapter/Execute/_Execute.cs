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

using Tekla.Structures;
using Tekla.Structures.Model;
using tsGeo = Tekla.Structures.Geometry3d;

namespace BH.Adapter.Tekla
{
    public partial class TeklaAdapter
    {
        /***************************************************/
        /****           Adapter Methods                 ****/
        /***************************************************/

        public override bool Execute(string command, Dictionary<string, object> parameters = null, Dictionary<string, object> config = null)
        {
            bool success = true;

            if (command == "CHANGE WORK PLANE")
            {
                BH.oM.Geometry.Vector xAxis = new Vector();
                string[] caseStringAlt =
                {
                    "X Axis",
                    "xaxis",
                    "XAXIS",
                    "x axis",
                };
                foreach (string str in caseStringAlt)
                {
                    object obj;
                    if (parameters.TryGetValue(str, out obj))
                    {
                        xAxis = obj as BH.oM.Geometry.Vector;
                        break;
                    }
                }

                BH.oM.Geometry.Vector yAxis = new Vector();
                string[] caseStringAlt2 =
{
                    "Y Axis",
                    "yaxis",
                    "YAXIS",
                    "y axis",
                };
                foreach (string str in caseStringAlt2)
                {
                    object obj;
                    if (parameters.TryGetValue(str, out obj))
                    {
                        yAxis = obj as BH.oM.Geometry.Vector;
                        break;
                    }
                }

                BH.oM.Geometry.Point origin = new Point();
                string[] caseStringAlt3 =
{
                    "origin",
                    "Origin",
                };
                foreach (string str in caseStringAlt3)
                {
                    object obj;
                    if (parameters.TryGetValue(str, out obj))
                    {
                        origin = obj as BH.oM.Geometry.Point;
                        break;
                    }
                }

                BH.oM.Geometry.Plane plane = null;
                string[] caseStringAlt4 =
{
                    "Plane",
                    "plane",
                };
                foreach (string str in caseStringAlt4)
                {
                    object obj;
                    if (parameters.TryGetValue(str, out obj))
                    {
                        plane = obj as BH.oM.Geometry.Plane;
                        break;
                    }
                }

                //first change to global coordinates plane
                WorkPlaneHandler PlaneHandler = m_TeklaModel.GetWorkPlaneHandler();
                TransformationPlane GlobalPlane = new TransformationPlane();
                PlaneHandler.SetCurrentTransformationPlane(GlobalPlane);

                //then change to plane of interest
                tsGeo.CoordinateSystem cs = new tsGeo.CoordinateSystem();
                if (plane == null)
                {
                    //If x and y have been provided use those (plane orientation matters)
                    cs = new tsGeo.CoordinateSystem(BH.Engine.Tekla.Convert.ToTekla(origin), BH.Engine.Tekla.Convert.ToTekla(xAxis), BH.Engine.Tekla.Convert.ToTekla(yAxis));
                }
                else
                {
                    //If only a plane has been provided, then only the normal matters and x/y are irrelevant.  
                    if (Math.Abs(BH.Engine.Geometry.Query.DotProduct(plane.Normal, new Vector { X = 0, Y = 0, Z = 1 })) > 0.95)
                    {
                        cs = new tsGeo.CoordinateSystem(BH.Engine.Tekla.Convert.ToTekla(plane.Origin), new tsGeo.Vector(1, 0, 0), BH.Engine.Tekla.Convert.ToTekla(plane.Normal));
                    }
                    else
                    {
                        cs = new tsGeo.CoordinateSystem(BH.Engine.Tekla.Convert.ToTekla(plane.Origin), new tsGeo.Vector(0, 0, 1), BH.Engine.Tekla.Convert.ToTekla(plane.Normal));
                    }
                }

                success = PlaneHandler.SetCurrentTransformationPlane(new TransformationPlane(cs));
            }

            return success;
        }
    }
}
