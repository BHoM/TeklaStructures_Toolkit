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
using BH.oM.Structure.Elements;
using BH.oM.Geometry;
using BH.Engine.Reflection;

using Tekla.Structures.Solid;
using Tekla.Structures.Geometry3d;

using Tekla.Structures;
using Tekla.Structures.Model;
using Tekla.Structures.Filtering;
using tsGeo = Tekla.Structures.Geometry3d;
using Face = Tekla.Structures.Solid.Face;
using Point = Tekla.Structures.Geometry3d.Point;

namespace BH.Engine.Adapters.TeklaStructures
{
    public static partial class Query
    {
        /***************************************************/
        /**** Public Methods                            ****/
        /***************************************************/

        public static List<oM.Geometry.Polyline> PartGeo(Part part)
        {
            List<oM.Geometry.Polyline> lines = new List<oM.Geometry.Polyline>();
            List<BH.oM.Geometry.Point> vertices = new List<BH.oM.Geometry.Point>();
            List<BH.oM.Geometry.Face> faces = new List<oM.Geometry.Face>();

            Solid solid = part.GetSolid();
            FaceEnumerator MyFaceEnum = solid.GetFaceEnumerator();

            while (MyFaceEnum.MoveNext())
            {
                Face MyFace = MyFaceEnum.Current as Face;
                if (MyFace != null)
                {
                    LoopEnumerator MyLoopEnum = MyFace.GetLoopEnumerator();
                    while (MyLoopEnum.MoveNext())
                    {
                        Loop MyLoop = MyLoopEnum.Current as Loop;
                        if (MyLoop != null)
                        {
                            VertexEnumerator MyVertexEnum = MyLoop.GetVertexEnumerator() as VertexEnumerator;
                            oM.Geometry.Polyline tempPolyline = new oM.Geometry.Polyline();

                            while (MyVertexEnum.MoveNext())
                            {
                                Point MyVertex = MyVertexEnum.Current as Point;
                                if (MyVertex != null)
                                {
                                    tempPolyline.ControlPoints.Add(MyVertex.ToBHoM());
                                }
                            }
                            tempPolyline.ControlPoints.Add(tempPolyline.ControlPoints[0]);
                            lines.Add(tempPolyline);
                        }
                    }
                }
            }
            return lines;
        }

        /***************************************************/
    }
}
