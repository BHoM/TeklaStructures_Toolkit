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
using BH.oM.Geometry;

using Tekla.Structures;
using tekmodel =  Tekla.Structures.Model;
using tsGeo = Tekla.Structures.Geometry3d;

namespace BH.Adapter.Tekla
{
    public partial class TeklaAdapter
    {

        /***************************************************/
        /**** Private methods                           ****/
        /***************************************************/

        private bool CreateCollection(IEnumerable<BH.oM.Structure.Elements.Weld> welds)
        {
            bool success = false;

            tsGeo.Point Beam1P1 = new tsGeo.Point(0, 12000, 0);
            tsGeo.Point Beam1P2 = new tsGeo.Point(3000, 12000, 0);

            tsGeo.Point Beam2P1 = new tsGeo.Point(3000, 12000, 0);
            tsGeo.Point Beam2P2 = new tsGeo.Point(3000, 18000, 0);

            tekmodel.Beam Beam1 = new tekmodel.Beam(Beam1P1, Beam1P2);
            tekmodel.Beam Beam2 = new tekmodel.Beam(Beam2P1, Beam2P2);

            Beam1.Profile.ProfileString = "HI400-15-20*400";
            Beam1.Finish = "PAINT";
            Beam1.Name = "Beam 1";
            Beam2.Profile.ProfileString = "HI300-15-20*300";
            Beam2.Name = "Beam 2";

            success = !Beam1.Insert();


          success = !Beam2.Insert();


            Beam1.Profile.ProfileString = "HI400-15-20*400";
            Beam1.Finish = "PAINT";
            Beam1.Name = "Beam 1";
            Beam2.Profile.ProfileString = "HI300-15-20*300";
            Beam2.Name = "Beam 2";

            tekmodel.Weld Weld = new tekmodel.Weld();
            Weld.MainObject = Beam1;
            Weld.SecondaryObject = Beam2;
            Weld.TypeAbove = tekmodel.Weld.WeldTypeEnum.WELD_TYPE_SQUARE_GROOVE_SQUARE_BUTT;

            success = !Weld.Insert();

            success = !Weld.Select();

                Weld.LengthAbove = 12;
            Weld.TypeBelow = tekmodel.Weld.WeldTypeEnum.WELD_TYPE_SLOT;

            success = !Weld.Modify();

            //tekmodel.PolygonWeld polyWeld = new tekmodel.PolygonWeld();
            //polyWeld.MainObject = Beam1;
            //polyWeld.SecondaryObject = Beam2;
            //polyWeld.TypeAbove = tekmodel.Weld.WeldTypeEnum.WELD_TYPE_SQUARE_GROOVE_SQUARE_BUTT;
            //polyWeld.Polygon.Points.Add(new tsGeo.Point(3000, 12000, 0));
            //polyWeld.Polygon.Points.Add(new tsGeo.Point(3000, 18000, 0));

            //success = !polyWeld.Insert();

            return success;
        }
    }
}