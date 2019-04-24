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
        /**** Private methods                           ****/
        /***************************************************/

        private bool CreateCollection(IEnumerable<Bolt> bolts)
        {
            //Code for creating a collection of bars in the software

            bool success = true;

            foreach (Bolt bolt in bolts)
            {
                BH.oM.Geometry.Plane plane = new oM.Geometry.Plane() {Origin = bolt.Centerline.Start, Normal = new Vector() { X = bolt.Centerline.Start.X - bolt.Centerline.End.X, Y = bolt.Centerline.Start.Y - bolt.Centerline.End.Y, Z = bolt.Centerline.Start.Z - bolt.Centerline.End.Z } };
                Dictionary<string, object> parameters = new Dictionary<string, object>();
                parameters.Add("plane", (object)plane);
                Execute("CHANGE WORK PLANE",parameters);

                ContourPlate cp = new ContourPlate();
                cp.AddContourPoint(new ContourPoint(new tsGeo.Point(30, 60, 0), null));
                cp.AddContourPoint(new ContourPoint(new tsGeo.Point(60, 60, 0), null));
                cp.AddContourPoint(new ContourPoint(new tsGeo.Point(60, 120, 0), null));
                cp.AddContourPoint(new ContourPoint(new tsGeo.Point(30, 120, 0), null));
                cp.Profile.ProfileString = "PL10";

                if (!cp.Insert())
                    success = false ;
                BoltArray B = new BoltArray();

                B.PartToBeBolted = cp;
                B.PartToBoltTo = cp;

                B.FirstPosition = new tsGeo.Point(30, 60, 0);
                B.SecondPosition = new tsGeo.Point(60, 120, 0);

                B.BoltSize = 16;
                B.Tolerance = 3.00;
                B.BoltStandard = "NELSON";
                B.BoltType = BoltGroup.BoltTypeEnum.BOLT_TYPE_WORKSHOP;
                B.CutLength = 105;

                B.Length = 100;
                B.ExtraLength = 15;
                B.ThreadInMaterial = BoltGroup.BoltThreadInMaterialEnum.THREAD_IN_MATERIAL_NO;

                B.Position.Depth = Position.DepthEnum.MIDDLE;
                B.Position.Plane = Position.PlaneEnum.MIDDLE;
                B.Position.Rotation = Position.RotationEnum.FRONT;

                B.Bolt = true;
                B.Washer1 = true;
                B.Washer2 = true;
                B.Washer3 = true;
                B.Nut1 = true;
                B.Nut2 = true;

                B.Hole1 = true;
                B.Hole2 = true;
                B.Hole3 = true;
                B.Hole4 = true;
                B.Hole5 = true;

                B.AddBoltDistX(0);

                B.AddBoltDistY(0);


                if (!B.Insert())
                    success = false;

            }
            return success;
        }

        /***************************************************/

    }
}
