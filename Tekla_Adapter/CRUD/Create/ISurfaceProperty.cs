﻿/*
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
using BH.Engine.Adapters.Tekla;
using BH.oM.Structure.SurfaceProperties;

using Tekla.Structures;
using Tekla.Structures.Model;
using Tekla.Structures.Filtering;
using tsGeo = Tekla.Structures.Geometry3d;

namespace BH.Adapter.TeklaStructures
{
    public partial class TeklaAdapter
    {
        private bool Create(IEnumerable<ISurfaceProperty> properties)
        {
            //Code for creating a collection of section properties in the software
            bool success = true;

            foreach (ISurfaceProperty sectionProperty in properties)
            {
                if (sectionProperty is ConstantThickness)
                {
                    Profile asdf = new Profile();
                    asdf.ProfileString = "PL" + "5";
                }
                ////Tip: if the NextId method has been implemented you can get the id to be used for the creation out as (cast into applicable type used by the software):
                //object secPropId = sectionProperty.CustomData[AdapterId];
                ////If also the default implmentation for the DependencyTypes is used,
                ////one can from here get the id's of the subobjects by calling (cast into applicable type used by the software): 
                //object materialId = sectionProperty.Material.CustomData[AdapterId];

                ////proposed functionality:
                ////check if section exists in the loaded catalog from tekla - m_ProfileEnumerator() - if not add the material
            }

            return success;
        }
    }
}
