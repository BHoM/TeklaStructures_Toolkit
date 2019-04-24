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
using BH.oM.Structure.Properties;
using BH.oM.Structure.Properties.Section;
using BH.oM.Structure.Properties.Constraint;
using BH.Engine.Tekla;
using  BH.oM.Geometry;

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

        private bool ChangeWorkPlane(BH.oM.Geometry.Plane pln)
        {
            bool success = true;

                WorkPlaneHandler PlaneHandler = m_TeklaModel.GetWorkPlaneHandler();
                tsGeo.GeometricPlane tsPlane = BH.Engine.Tekla.Convert.ToTekla(pln);

                //need to revisit this - bhom definition of plan doesnt suffice needs x/y axis
                success = PlaneHandler.SetCurrentTransformationPlane(new TransformationPlane(BH.Engine.Tekla.Convert.ToTekla(pln.Origin), new tsGeo.Vector { X = 0, Y = 0, Z = 0 }, BH.Engine.Tekla.Convert.ToTekla(pln.Normal)));
 
            return success;

        }

        /***************************************************/

    }
}
