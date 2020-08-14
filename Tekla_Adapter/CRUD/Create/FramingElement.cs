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

using BH.Engine.Adapters.Tekla;
using BH.oM.Physical.Elements;
using Tekla.Structures;
using tsModel = Tekla.Structures.Model;
using Tekla.Structures.Geometry3d;
using Tekla.Structures.Filtering;
using BH.oM.Physical.FramingProperties;


namespace BH.Adapter.Tekla
{
    public partial class TeklaAdapter
    {
        private bool CreateCollection(IEnumerable<IFramingElement> framingElements)
        {
            bool success = true;

            foreach (IFramingElement framing in framingElements)
            {
                //Tip: if the NextId method has been implemented you can get the id to be used for the creation out as (cast into applicable type used by the software):
                int framingId = (int)framing.CustomData[AdapterIdName];
                //If also the default implmentation for the DependencyTypes is used,
                //one can from here get the id's of the subobjects by calling (cast into applicable type used by the software): 
                //object startNodeId = bar.StartNode.CustomData[AdapterId];
                //object endNodeId = bar.EndNode.CustomData[AdapterId];
                //object SecPropId = bar.SectionProperty.CustomData[AdapterId];


                tsModel.Beam tsBeam = new tsModel.Beam(framing.ToTeklaFramingType());

                

                if (framing.Location.GetType() == typeof(BH.oM.Geometry.Line))
                {
                    BH.oM.Geometry.Line centreLine = framing.Location as BH.oM.Geometry.Line;
                    tsBeam.StartPoint = new Point { X = centreLine.Start.ToTekla().X * 1000, Y = centreLine.Start.ToTekla().Y * 1000, Z = centreLine.Start.ToTekla().Z * 1000 };
                    tsBeam.EndPoint = new Point { X = centreLine.End.ToTekla().X * 1000, Y = centreLine.End.ToTekla().Y * 1000, Z = centreLine.End.ToTekla().Z * 1000 };
                }
                else if (framing.Location.GetType() == typeof(BH.oM.Geometry.Polyline))
                {
                    BH.oM.Geometry.Polyline centreLine = framing.Location as BH.oM.Geometry.Polyline;
                    tsBeam.StartPoint = centreLine.ControlPoints.First().ToTekla();
                    tsBeam.EndPoint = centreLine.ControlPoints.Last().ToTekla();
                    // add warning that polyline has been changed to line ! ! ! ! 
                }

                tsBeam.Identifier = new Identifier(framingId);
                tsBeam.Name = framing.Name;
              
                tsBeam.Position.Plane = tsModel.Position.PlaneEnum.MIDDLE;
                tsBeam.Position.Depth = tsModel.Position.DepthEnum.MIDDLE;

                if (framing.CustomData.ContainsKey("InsertionPoint"))
                {
                    if ((int)framing.CustomData["InsertionPoint"] == 8)
                    {
                        tsBeam.Position.Depth = tsModel.Position.DepthEnum.BEHIND;
                    }
                    else if ((int)framing.CustomData["InsertionPoint"] == 2)
                    {
                        tsBeam.Position.Depth = tsModel.Position.DepthEnum.FRONT;
                    }
                    else
                    {
                        tsBeam.Position.Depth = tsModel.Position.DepthEnum.MIDDLE;
                    }
                }
                

                ConstantFramingProperty framingProperty = framing.Property as ConstantFramingProperty;
                //tsBeam.Position.Rotation = Position.RotationEnum.FRONT; /// -- it is unclear what changing this enum actually does; 
                //tsBeam.Position.RotationOffset = framingProperty.OrientationAngle * (180 / Math.PI);

                if (m_MaterialLibrary.Contains(framingProperty.Material.Name))
                    tsBeam.Material.MaterialString = framingProperty.Material.Name;
                else
                    tsBeam.Material.MaterialString = "S355";


                if (m_ProfileLibrary.Contains(framing.Property.Name))
                {
                    tsBeam.Profile.ProfileString = framing.Property.Name;
                }
                else
                {
                    if(m_ProfileLibrary.Count>=1)
                    {
                        //add warning that profile does not exist and standard section has been used
                        Engine.Reflection.Compute.RecordWarning("Profile " + framing.Property.Name + " was not found in library - replaced with: " + m_ProfileLibrary[0]);
                        tsBeam.Profile.ProfileString = m_ProfileLibrary[0];
                    }
                    else
                    {
                        //set a new profile NOT from library i.e. some parametric profile 
                    }

                }

                if (!tsBeam.Insert())
                    success = false;

                framing.CustomData.Remove(AdapterIdName);
                framing.CustomData.Add(AdapterIdName, tsBeam.Identifier.ID);
            }

            return success;

        }

    }
}
