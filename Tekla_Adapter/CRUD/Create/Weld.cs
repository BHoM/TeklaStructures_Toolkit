/*
 * This file is part of the Buildings and Habitats object Model (BHoM)
 * Copyright (c) 2015 - 2023, the respective contributors. All rights reserved.
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


using BH.oM.Geometry;
using BH.Engine.Adapters.TeklaStructures;

using Tekla.Structures;
using tekmodel =  Tekla.Structures.Model;
using tsGeo = Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;


namespace BH.Adapter.TeklaStructures
{
    public partial class TeklaAdapter
    {

        /***************************************************/
        /**** Private methods                           ****/
        /***************************************************/

        private bool CreateCollection(IEnumerable<BH.oM.Structure.Elements.Weld> welds)
        {
            bool success = false;

            WorkPlaneHandler PlaneHandler = m_TeklaModel.GetWorkPlaneHandler();
            TransformationPlane GlobalPlane = new TransformationPlane();
            PlaneHandler.SetCurrentTransformationPlane(GlobalPlane);

            foreach (oM.Structure.Elements.Weld w in welds)
            {
                tekmodel.PolygonWeld polyWeld = new tekmodel.PolygonWeld();
                polyWeld.TypeAbove = tekmodel.Weld.WeldTypeEnum.WELD_TYPE_FILLET;
                //Needs updated to work with ICurve
                List<Point> points = BH.Engine.Geometry.Query.ControlPoints((Polyline) w.weldPath);

                //Convert Path to Tekla
                foreach (Point p in points)
                {
                    polyWeld.Polygon.Points.Add(p.ToTekla());
                }

                //Find Relevant Model Elements
                ModelObject tsObjWelded = m_TeklaModel.SelectModelObject(new Identifier((int)w.objWelded.CustomData[AdapterIdName]));
                polyWeld.MainObject = tsObjWelded;

                ModelObject tsObjWeldedTo = m_TeklaModel.SelectModelObject(new Identifier((int)w.objWeldedTo.CustomData[AdapterIdName]));
                polyWeld.SecondaryObject = tsObjWeldedTo;

                success = !polyWeld.Insert();
            }

            return success;
        }
    }
}

