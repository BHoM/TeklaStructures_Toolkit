/*
 * This file is part of the Buildings and Habitats object Model (BHoM)
 * Copyright (c) 2015 - 2024, the respective contributors. All rights reserved.
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
using BH.oM.Adapter;
using BH.oM.Base;
using Tekla.Structures.Model;


namespace BH.Adapter.TeklaStructures
{
    public partial class TeklaAdapter
    {
        /***************************************************/
        /**** Adapter overload method                   ****/
        /***************************************************/

        protected override object NextFreeId(Type objectType, bool refresh = false)
        {
            //Method that returns the next free index for a specific object type. 
            //Software dependent which type of index to return. Could be int, string, Guid or whatever the specific software is using
            //At the point of index assignment, the objects have not yet been created in the target software. 
            //The if statement below is designed to grab the first free index for the first object being created and after that increment.

            //Change from object to what the specific software is using
            int index = 1;

            if (!refresh && m_indexDict.TryGetValue(objectType, out index))
            {
                //If possible to find the next index based on the previous one (for example index++ for an int based index system) do it here

                index++;
                m_indexDict[objectType] = index;

            }
            else
            {
                //index = 5000;//Insert code to get the next index of the specific type
                index = GetLastIdOfType(objectType) + 1;
                m_indexDict[objectType] = index;

            }

            m_indexDict[objectType] = index;
            return index;
        }


        private int GetLastIdOfType(Type objectType)
        {
            int lastId;
            string typeString = objectType.ToString();

            switch (typeString)
            {
                case "Node":
                    lastId = 0;// --- not needed ? only for analytical node...?
                    break;

                case "Bar":
                    foreach (ModelObject m in m_ObjectSelector.GetAllObjectsWithType(ModelObject.ModelObjectEnum.BEAM))
                    {
                        Beam b = m as Beam;
                        lastId = b.Identifier.ID;
                        //get highest id value
                    }

                    lastId = 5000;
                    break;

                case "Material":
                    lastId = 0;    
                    break;
                case "SectionProperty":
                    lastId = 0;
                    break;

                default:
                    lastId = 0;//<---- log error
                    break;
            }
            return lastId;
        }

        /***************************************************/
        /**** Private Fields                            ****/
        /***************************************************/

        //Change from object to the index type used by the specific software
        private Dictionary<Type, int> m_indexDict = new Dictionary<Type, int>();


        /***************************************************/

    }
}



