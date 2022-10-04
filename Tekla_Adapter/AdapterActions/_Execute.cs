using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BH.oM.Structure.Elements;

using BH.Engine.Adapters.Tekla;
using BH.oM.Geometry;
using BH.oM.Adapter;
using BH.oM.Adapters.Tekla.Commands;
using BH.oM.Base;
using BH.oM.Adapter.Commands;

using Tekla.Structures;
using Tekla.Structures.Model;
using tsGeo = Tekla.Structures.Geometry3d;

namespace BH.Adapter.TeklaStructures
{
    public partial class TeklaAdapter
    {
        /***************************************************/
        /****           IAdapter Interface              ****/
        /***************************************************/

        public override Output<List<object>, bool> Execute(IExecuteCommand command, ActionConfig actionConfig = null)
        {
            var output = new Output<List<object>, bool>() { Item1 = null, Item2 = false };

            output.Item2 = RunCommand(command as dynamic);

            return output;
        }

        public bool RunCommand(IExecuteCommand command)
        {

            BH.Engine.Base.Compute.RecordError($"The command {command.GetType().Name} is not recognised by {this.GetType().Name}.");
            return false;
        }

        public bool RunCommand(ChangeWorkPlane command)
        {
            bool success;

            BH.oM.Geometry.Vector xAxis = command.XVector;
            BH.oM.Geometry.Vector yAxis = command.YVector;
            BH.oM.Geometry.Point origin = command.Origin;
            BH.oM.Geometry.Plane plane = command.Plane;



            //first change to global coordinates plane
            WorkPlaneHandler PlaneHandler = m_TeklaModel.GetWorkPlaneHandler();
            TransformationPlane GlobalPlane = new TransformationPlane();
            PlaneHandler.SetCurrentTransformationPlane(GlobalPlane);

            //then change to plane of interest
            tsGeo.CoordinateSystem cs = new tsGeo.CoordinateSystem();
            if (plane == null)
            {
                //If x and y have been provided use those (plane orientation matters)
                cs = new tsGeo.CoordinateSystem(BH.Engine.Adapters.Tekla.Convert.ToTekla(origin), BH.Engine.Adapters.Tekla.Convert.ToTekla(xAxis), BH.Engine.Adapters.Tekla.Convert.ToTekla(yAxis));
            }
            else
            {
                //If only a plane has been provided, then only the normal matters and x/y are irrelevant.  
                if (Math.Abs(BH.Engine.Geometry.Query.DotProduct(plane.Normal, new Vector { X = 0, Y = 0, Z = 1 })) > 0.95)
                {
                    cs = new tsGeo.CoordinateSystem(BH.Engine.Adapters.Tekla.Convert.ToTekla(plane.Origin), new tsGeo.Vector(1, 0, 0), BH.Engine.Adapters.Tekla.Convert.ToTekla(plane.Normal));
                }
                else
                {
                    cs = new tsGeo.CoordinateSystem(BH.Engine.Adapters.Tekla.Convert.ToTekla(plane.Origin), new tsGeo.Vector(0, 0, 1), BH.Engine.Adapters.Tekla.Convert.ToTekla(plane.Normal));
                }
            }

            success = PlaneHandler.SetCurrentTransformationPlane(new TransformationPlane(cs));

            return success;
        }


    }
}
