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


using Tekla.Structures;
using tsModel = Tekla.Structures.Model;
using tsGeo = Tekla.Structures.Geometry3d;
using System.Collections;

namespace BH.Adapter.Tekla
{
    public partial class TeklaAdapter
    {

        /***************************************************/
        /**** Private methods                           ****/
        /***************************************************/

        private bool CreateCollection(IEnumerable<Connection> connections)
        {

            bool success = true;

            foreach (Connection bhConnection in connections)
            {
                int connectionId = (int)bhConnection.CustomData[AdapterId];
                //List<int> elementIds = new List<int>();

                //foreach (FramingElement fe in bhConnection.ConnectingElements)
                //    elementIds.Add((int)fe.CustomData[AdapterId]);


                tsModel.Connection tsConnection = new tsModel.Connection();

                if (m_ConnectionLibrary.ContainsKey(bhConnection.LibraryName))
                {
                    tsConnection.Name = bhConnection.LibraryName;
                    tsConnection.Number = m_ConnectionLibrary[bhConnection.LibraryName];
                }
                else
                {
                    // add error message ! ! ! ! ! 
                }

                // ! ! ! !  ---- get connection name and number from a preloaded library ----- ! ! ! ! !
                //con.Name = "apiCreatedConnectionTest";
                //tsConnection.Number = 310000041;
                tsConnection.LoadAttributesFromFile("standard");
                tsConnection.UpVector = new tsGeo.Vector(0, 0, 1);
                tsConnection.PositionType = PositionTypeEnum.COLLISION_PLANE;

                //set primary connecting element
                tsModel.Beam primary = m_TeklaModel.SelectModelObject(new Identifier(bhConnection.ConnectingElementIds[0])) as tsModel.Beam;
                tsConnection.SetPrimaryObject(primary);

                //set secondary connecting elements
                if (bhConnection.ConnectingElementIds.Count == 2)
                {
                    tsModel.Beam secondary = m_TeklaModel.SelectModelObject(new Identifier(bhConnection.ConnectingElementIds[1])) as tsModel.Beam;
                    tsConnection.SetSecondaryObject(secondary);// -- it might be that .SetSecondaryObjects() makes this unneccessary
                }
                else if (bhConnection.ConnectingElementIds.Count > 2)
                {
                    List<tsModel.Beam> beamList = new List<tsModel.Beam>();

                    for (int i = 1; i < bhConnection.ConnectingElementIds.Count; i++)
                    {
                        beamList.Add(m_TeklaModel.SelectModelObject(new Identifier(bhConnection.ConnectingElementIds[i])) as tsModel.Beam);
                    }
                    tsConnection.SetSecondaryObjects(new ArrayList(beamList));
                }

                //override any attributes set by '.LoadAttributesFromFile'
                //con.SetAttribute("e1", 12.5);
                //con.SetAttribute("e2", "something");
                //con.SetAttribute("e3", "something else");


                Hashtable userProps = new Hashtable();
                tsConnection.GetDoubleUserProperties(ref userProps);

                if (!tsConnection.Insert())
                    success = false;
            }

            return success;

        }

        /***************************************************/

    }
}
