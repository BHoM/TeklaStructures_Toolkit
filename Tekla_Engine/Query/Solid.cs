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

namespace BH.Engine.Adapters.Tekla
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
