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

using tsGeo = Tekla.Structures.Geometry3d;

namespace BH.Engine.Adapters.TeklaStructures
{
    public static partial class Convert
    {
        /***************************************************/
        /**** Public Methods                            ****/
        /***************************************************/

        //Add methods for converting to BHoM from the specific software types. 
        //Only do this if possible to do without any com-calls or similar to the adapter
        //Example:
        //public static Node ToBHoM(this TeklaNode node)
        //{
        //    //Insert code for convertion
        //}

        /***************************************************/

        public static Point ToBHoM(this tsGeo.Point tsPoint)
        {
            Point point = new Point() { X = tsPoint.X, Y = tsPoint.Y, Z = tsPoint.Z };
            return point;
        }

        public static Vector ToBHoM(this tsGeo.Vector tsVector)
        {
            Vector vect = new Vector() { X = tsVector.X, Y = tsVector.Y, Z = tsVector.Z };
            return vect;
        }

        public static Plane ToBHoMPlane(this tsGeo.GeometricPlane tsPlane)
        {
            Plane plane = new Plane() { Origin = BH.Engine.Adapters.TeklaStructures.Convert.ToBHoM(tsPlane.Origin), Normal = BH.Engine.Adapters.TeklaStructures.Convert.ToBHoM(tsPlane.Normal) };

            return plane;
        }


    }
}

