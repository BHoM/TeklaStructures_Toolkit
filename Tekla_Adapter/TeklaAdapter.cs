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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BH.Adapter;
using BH.oM.Adapter;

using Tekla.Structures;
using Tekla.Structures.Model;
using Tekla.Structures.Catalogs;


namespace BH.Adapter.TeklaStructures
{
    public partial class TeklaAdapter : BHoMAdapter
    {

        /***************************************************/
        /**** Constructors                              ****/
        /***************************************************/

        public List<string> connectionLibrary { get; set; } = new List<string>();
        public List<string> m_ProfileLibrary { get; set; } = new List<string>();//---There is curently no point in storing a dictionary with the profile properties; only name is used to create framing elements

        //Add any applicable constructors here, such as linking to a specific file or anything else as well as linking to that file through the (if existing) com link via the API
        public TeklaAdapter(string filePath = "", bool active = false)
        {
            //Initialise
            AdapterIdName = BH.Engine.Adapters.TeklaStructures.Convert.AdapterIdName;
            SetupComparers();
            GetDependencyTypes();
            BH.Adapter.Modules.Structure.ModuleLoader.LoadModules(this);


            if (active)
            {

                m_TeklaModel = new Model();
                m_CatalogHandler = new CatalogHandler();
                m_ProfileLibrary = new List<string>();
                m_MaterialLibrary = new List<string>();
                m_ConnectionLibrary = new Dictionary<string, int>();


                if (m_TeklaModel.GetConnectionStatus())
                {
                    m_ObjectSelector = m_TeklaModel.GetModelObjectSelector();
                }
                else
                {
                    Console.WriteLine("Could not connect to Tekla!");
                }

                if (m_CatalogHandler.GetConnectionStatus())
                {
                    m_ComponentEnumerator = m_CatalogHandler.GetComponentItems();

                    // build profile library
                    ProfileItemEnumerator profileEnumerator = m_CatalogHandler.GetProfileItems();
                    while (profileEnumerator.MoveNext())
                    {
                        LibraryProfileItem profileItem = profileEnumerator.Current as LibraryProfileItem;
                        if (profileItem != null && !string.IsNullOrEmpty(profileItem.ProfileName))
                            m_ProfileLibrary.Add(profileItem.ProfileName);
                    }

                    // build material library
                    MaterialItemEnumerator materialEnumerator = m_CatalogHandler.GetMaterialItems();
                    while (materialEnumerator.MoveNext())
                    {
                        MaterialItem materialItem = materialEnumerator.Current as MaterialItem;
                        if (materialItem != null && !string.IsNullOrEmpty(materialItem.MaterialName))
                            m_MaterialLibrary.Add(materialItem.MaterialName);
                    }
                   
                    //build connection library
                    while (m_ComponentEnumerator.MoveNext())
                    {
                        ComponentItem item = m_ComponentEnumerator.Current as ComponentItem;
                        if (item.Type == ComponentItem.ComponentTypeEnum.CONNECTION)
                        {
                            // NOTE 
                            // item.Name is mostly empty. when it is not empty is a duplicate of item.UIName and in this case the item.Nunber is -200000
                            // examples in documentation show connections created from item.Number but it could be that when this is -200000 it can be created from item.Name
                            if (!m_ConnectionLibrary.ContainsKey(item.UIName))
                            {
                                m_ConnectionLibrary.Add(item.UIName, item.Number);
                            }
                            else
                            {
                                // add some warning ! ! ! ! ! ! 
                            }
                        }
                    }
                    connectionLibrary.AddRange(m_ConnectionLibrary.Keys.ToList());

                }
                else
                {
                    Console.WriteLine("Could not access Tekla Catalog Handler!");
                }


            }
        }



        /***************************************************/
        /**** Private  Fields                           ****/
        /***************************************************/

        private Model m_TeklaModel;
        private CatalogHandler m_CatalogHandler;
        private ModelObjectSelector m_ObjectSelector;
        private ComponentItemEnumerator m_ComponentEnumerator;
        private Dictionary<string, int> m_ConnectionLibrary;
        private List<string> m_MaterialLibrary;//---There is curently no point in storing a dictionary with Material properties; only name is used to create framing elements



        /***************************************************/


    }
}
