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
using BH.oM.Structure.SurfaceProperties;
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

        private bool CreateCollection(IEnumerable<Panel> panels)
        {
            //Code for creating a collection of panel planar in the software

            bool success = true;

            foreach (Panel p in panels)
            {
                ////Create a Plate Profile
                ContourPlate contourPlate = new ContourPlate();
                Profile pfl = new Profile();
                foreach(Point point in Engine.Spatial.Query.ControlPoints(p))
                    contourPlate.Contour.AddContourPoint(new ContourPoint(new tsGeo.Point(point.X, point.Y, point.Z), new Chamfer()));

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
                    contourPlate.Profile.ProfileString = "PL" + ct.Thickness.ToString();
                }

                if (!contourPlate.Insert())
                {
                    success = false;
                }
            }
            return success;
        }

        /***************************************************/

    }
}


