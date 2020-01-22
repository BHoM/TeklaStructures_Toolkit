/*
 * This file is part of the Buildings and Habitats object Model (BHoM)
 * Copyright (c) 2015 - 2019, the respective contributors. All rights reserved.
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

using BH.Engine.Tekla;

using Tekla.Structures;
using Tekla.Structures.Model;
using tsGeo = Tekla.Structures.Geometry3d;
using System.Collections;
using BH.oM.Geometry;

namespace BH.Adapter.Tekla
{
    public partial class TeklaAdapter
    {

        /***************************************************/
        /**** Private methods                           ****/
        /***************************************************/

        private bool CreateCollection(IEnumerable<Cut> cuts)
        {
            bool success = true;

            WorkPlaneHandler PlaneHandler = m_TeklaModel.GetWorkPlaneHandler();
            TransformationPlane GlobalPlane = new TransformationPlane();
            PlaneHandler.SetCurrentTransformationPlane(GlobalPlane);

            foreach (Cut c in cuts)
            {
                //Crete New Polygon Cut
                ContourPlate contourPlate = new ContourPlate();
                contourPlate.AssemblyNumber.Prefix = "XX";
                contourPlate.AssemblyNumber.StartNumber = 1;
                contourPlate.PartNumber.Prefix = "xx";
                contourPlate.Name = "CUT";
                contourPlate.Profile.ProfileString = "200";
                contourPlate.Material.MaterialString = "ANTIMATERIAL";
                contourPlate.Finish = "";
                contourPlate.Class = BooleanPart.BooleanOperativeClassName;
                if (c.CustomData.ContainsKey("InsertionPoint"))
                {
                    if ((int)c.CustomData["InsertionPoint"] == 8)
                    {
                        contourPlate.Position.Depth = Position.DepthEnum.BEHIND;
                    }
                    else if ((int)c.CustomData["InsertionPoint"] == 2)
                    {
                        contourPlate.Position.Depth = Position.DepthEnum.FRONT;
                    }
                    else
                    {
                        contourPlate.Position.Depth = Position.DepthEnum.MIDDLE;
                    }
                }
                Profile pfl = new Profile();

                foreach (Point point in Engine.Geometry.Query.IControlPoints(c.curve))
                    contourPlate.Contour.AddContourPoint(new ContourPoint(new tsGeo.Point(point.X, point.Y, point.Z), new Chamfer(1, 2, Chamfer.ChamferTypeEnum.CHAMFER_NONE)));

                contourPlate.Profile.ProfileString = "PL" + c.thickness.ToString();

                if (!contourPlate.Insert()) //Insert Plate
                {
                    success = false;
                }
                else //Use Plate to Cut Parent
                {
                    Beam tsBeam = new Beam();
                    if(c.cutObject.GetType() == typeof(BH.oM.Structure.Elements.FramingElement))
                    {
                       int ids  = (int)c.cutObject.CustomData[AdapterIdName] ;
                       ModelObject tsObj = m_TeklaModel.SelectModelObject(new Identifier(ids));
                       tsBeam =  tsObj as Beam;
                    }

                    BooleanPart polygonCut = new BooleanPart();
                    polygonCut.Father = tsBeam;
                    polygonCut.OperativePart = contourPlate;
                    polygonCut.Type = BooleanPart.BooleanTypeEnum.BOOLEAN_CUT;

                    if (!polygonCut.Insert()) //if the cut wasn't made, return false
                    {
                        success = false;
                    }
                    else //otherwise, delete dummy plate created
                    {
                        contourPlate.Delete();
                    }
                }
            }
            return success;
        }
    }
}