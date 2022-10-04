/*
 * This file is part of the Buildings and Habitats object Model (BHoM)
 * Copyright (c) 2015 - 2022, the respective contributors. All rights reserved.
 *
 * Each contributor holds copyright over their respective contributions.
 * The project versioning (Git) records all such contribution source information.
 *                                           
 *                                                                              
 * The BHoM is free software: you can redistribute it and/or modify         
 * it under the terms of the GNU Lesser General Public License as published by  
 * the Free Software Foundation, either version 3.0 of the License, or          
 * (at your option) any later version.                                          
 *                                                                              
 * The BHoM is distributed in the hope that it will be useful,              
 * but WITHOUT ANY WARRANTY; without even the implied warranty of               
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the                 
 * GNU Lesser General Public License for more details.                          
 *                                                                            
 * You should have received a copy of the GNU Lesser General Public License     
 * along with this code. If not, see <https://www.gnu.org/licenses/lgpl-3.0.html>.      
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BH.oM.Structure.Elements;

using BH.Engine.Adapters.Tekla;
using BH.oM.Geometry;

using Tekla.Structures;
using Tekla.Structures.Model;
using tsGeo = Tekla.Structures.Geometry3d;

namespace BH.Adapter.TeklaStructures
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

            WorkPlaneHandler PlaneHandler = m_TeklaModel.GetWorkPlaneHandler();
            TransformationPlane GlobalPlane = new TransformationPlane();
            PlaneHandler.SetCurrentTransformationPlane(GlobalPlane);

            foreach (Bolt bolt in bolts)
            {
                //BH.oM.Geometry.Plane plane = new oM.Geometry.Plane() { Origin = bolt.Centerline.Start, Normal = new Vector() { X = bolt.Centerline.Start.X - bolt.Centerline.End.X, Y = bolt.Centerline.Start.Y - bolt.Centerline.End.Y, Z = bolt.Centerline.Start.Z - bolt.Centerline.End.Z } };
                //Dictionary<string, object> parameters = new Dictionary<string, object>();
                //parameters.Add("plane", (object)plane);
                //Execute("CHANGE WORK PLANE", parameters);

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

                //Create line orthogonal to bolt centerline for purposes of creating local array axes
                Vector v = new Vector();
                BH.oM.Geometry.Vector boltV = BH.Engine.Geometry.Create.Vector(bolt.Centerline.Start.X - bolt.Centerline.End.X, bolt.Centerline.Start.Y - bolt.Centerline.End.Y, bolt.Centerline.Start.Z - bolt.Centerline.End.Z);

                if ( bolt.Centerline.Start.Z == bolt.Centerline.End.Z )
                 v = BH.Engine.Geometry.Query.CrossProduct(boltV, new Vector { X = 0, Y=0, Z=1});
                else
                 v = BH.Engine.Geometry.Query.CrossProduct(boltV, new Vector { X = 1, Y = 0, Z = 0 });

                B.FirstPosition = new tsGeo.Point(bolt.Centerline.Start.X, bolt.Centerline.Start.Y, bolt.Centerline.Start.Z);
                B.SecondPosition = new tsGeo.Point(BH.Engine.Geometry.Create.Line(bolt.Centerline.Start, v).End.X, BH.Engine.Geometry.Create.Line(bolt.Centerline.Start, v).End.Y, BH.Engine.Geometry.Create.Line(bolt.Centerline.Start, v).End.Z);

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
                B.Position.Rotation = Position.RotationEnum.TOP;

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

                //B.AddBoltDistX(100);
                //B.AddBoltDistX(90);
                //B.AddBoltDistX(80);

                //B.AddBoltDistY(70);
                //B.AddBoltDistY(60);
                //B.AddBoltDistY(50);

                if (!B.Insert())
                    success = false;

            }

            return success;
        }

        /***************************************************/

    }
}

