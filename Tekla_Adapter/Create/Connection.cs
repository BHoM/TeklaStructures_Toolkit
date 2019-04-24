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

            foreach (Connection connection in connections)
            {
                int connectionId = (int)connection.CustomData[AdapterId];

                if (m_ConnectionLibrary.ContainsKey(connection.Name))
                {
                    connectionFromLibary(connection);
                }
                else
                {
                    connectionFromComponents(connection);
                }
            }

            return success;

        }

        /***************************************************/
        //Private Methods
        /***************************************************/

        private bool connectionFromLibary(Connection conn)
        {
            bool success = true;

            tsModel.Connection tsConnection = new tsModel.Connection();
            tsConnection.Name = conn.Name;
            tsConnection.Number = m_ConnectionLibrary[conn.Name];

            // ! ! ! !  ---- get connection name and number from a preloaded library ----- ! ! ! ! !
            //con.Name = "apiCreatedConnectionTest";
            //tsConnection.Number = 310000041;
            tsConnection.LoadAttributesFromFile("standard");
            tsConnection.UpVector = new tsGeo.Vector(0, 0, 1);
            tsConnection.PositionType = PositionTypeEnum.COLLISION_PLANE;

            //set primary connecting element
            tsModel.Beam primary = m_TeklaModel.SelectModelObject(new Identifier(conn.ConnectingElementIds[0])) as tsModel.Beam;
            tsConnection.SetPrimaryObject(primary);

            //set secondary connecting elements
            if (conn.ConnectingElementIds.Count == 2)
            {
                tsModel.Beam secondary = m_TeklaModel.SelectModelObject(new Identifier(conn.ConnectingElementIds[1])) as tsModel.Beam;
                tsConnection.SetSecondaryObject(secondary);// -- it might be that .SetSecondaryObjects() makes this unneccessary
            }
            else if (conn.ConnectingElementIds.Count > 2)
            {
                List<tsModel.Beam> beamList = new List<tsModel.Beam>();

                for (int i = 1; i < conn.ConnectingElementIds.Count; i++)
                {
                    beamList.Add(m_TeklaModel.SelectModelObject(new Identifier(conn.ConnectingElementIds[i])) as tsModel.Beam);
                }
                tsConnection.SetSecondaryObjects(new ArrayList(beamList));
            }

            Hashtable userProps = new Hashtable();
            tsConnection.GetDoubleUserProperties(ref userProps);

            if (!tsConnection.Insert())
                success = false;
            return success;
        }

        /***************************************************/

        private bool connectionFromComponents(Connection conn)
        {
            bool success = true;

            Create(conn.Plates);

            CreateCollection(conn.Bolts);

            return success;
        }
    }
}
