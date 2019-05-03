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
using BH.oM.Structure.Properties.Section;
using BH.oM.Structure.Properties.Constraint;
using BH.oM.Common.Materials;
using BH.Engine.Tekla;

using Tekla.Structures;
using Tekla.Structures.Model;
using Tekla.Structures.Filtering;


namespace BH.Adapter.Tekla
{
    public partial class TeklaAdapter
    {
        private bool CreateCollection(IEnumerable<FramingElement> framingElements)
        {
            bool success = true;

            foreach (FramingElement framing in framingElements)
            {
                //Tip: if the NextId method has been implemented you can get the id to be used for the creation out as (cast into applicable type used by the software):
                int framingId = (int)framing.CustomData[AdapterId];
                //If also the default implmentation for the DependencyTypes is used,
                //one can from here get the id's of the subobjects by calling (cast into applicable type used by the software): 
                //object startNodeId = bar.StartNode.CustomData[AdapterId];
                //object endNodeId = bar.EndNode.CustomData[AdapterId];
                //object SecPropId = bar.SectionProperty.CustomData[AdapterId];

                Beam tsBeam = new Beam(framing.StructuralUsage.ToTekla());

                if (framing.LocationCurve.GetType() == typeof(BH.oM.Geometry.Line))
                {
                    BH.oM.Geometry.Line centreLine = framing.LocationCurve as BH.oM.Geometry.Line;
                    tsBeam.StartPoint = centreLine.Start.ToTekla();
                    tsBeam.EndPoint = centreLine.End.ToTekla();
                }
                else if (framing.LocationCurve.GetType() == typeof(BH.oM.Geometry.Polyline))
                {
                    BH.oM.Geometry.Polyline centreLine = framing.LocationCurve as BH.oM.Geometry.Polyline;
                    tsBeam.StartPoint = centreLine.ControlPoints.First().ToTekla();
                    tsBeam.EndPoint = centreLine.ControlPoints.Last().ToTekla();
                    // add warning that polyline has been changed to line ! ! ! ! 
                }

                tsBeam.Identifier = new Identifier(framingId);
                tsBeam.Name = framing.Name;

                tsBeam.Position.Plane = Position.PlaneEnum.MIDDLE;
                tsBeam.Position.Depth = Position.DepthEnum.MIDDLE;

                BH.oM.Structure.Properties.Framing.ConstantFramingElementProperty framingProperty = framing.Property as BH.oM.Structure.Properties.Framing.ConstantFramingElementProperty;
                //tsBeam.Position.Rotation = Position.RotationEnum.FRONT; /// -- it is unclear what changing this enum actually does; 
                //tsBeam.Position.RotationOffset = framingProperty.OrientationAngle * (180 / Math.PI);

                if (m_MaterialLibrary.Contains(framingProperty.SectionProperty.Material.Name))
                    tsBeam.Material.MaterialString = framingProperty.SectionProperty.Material.Name;
                else
                    tsBeam.Material.MaterialString = "S355";


                if (m_ProfileLibrary.Contains(framing.Property.Name))
                {
                    tsBeam.Profile.ProfileString = framing.Property.Name;
                }
                else
                {
                    //add warning that profile does not exist and standard section has been used
                    Engine.Reflection.Compute.RecordWarning("Profile " + framing.Property.Name + " was not found in library - replaced with: " + m_ProfileLibrary[0]);
                    tsBeam.Profile.ProfileString = m_ProfileLibrary[0];
                }

                if (!tsBeam.Insert())
                    success = false;
            }

            return success;

        }

    }
}
