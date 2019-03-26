﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BH.Adapter;
using BH.Engine.Tekla;

using Tekla.Structures;
using Tekla.Structures.Model;
using Tekla.Structures.Catalogs;


namespace BH.Adapter.Tekla
{
    public partial class TeklaAdapter : BHoMAdapter
    {

        /***************************************************/
        /**** Constructors                              ****/
        /***************************************************/

        //Add any applicable constructors here, such as linking to a specific file or anything else as well as linking to that file through the (if existing) com link via the API
        public TeklaAdapter(string filePath = "", bool active = false)
        {
            if (active)
            {
                AdapterId = BH.Engine.Tekla.Convert.AdapterId;   //Set the "AdapterId" to "SoftwareName_id". Generally stored as a constant string in the convert class in the SoftwareName_Engine

                Config.SeparateProperties = true;   //Set to true to push dependant properties of objects before the main objects are being pushed. Example: push nodes before pushing bars
                Config.MergeWithComparer = true;    //Set to true to use EqualityComparers to merge objects. Example: merge nodes in the same location
                Config.ProcessInMemory = false;     //Set to false to to update objects in the toolkit during the push
                Config.CloneBeforePush = true;      //Set to true to clone the objects before they are being pushed through the software. Required if any modifications at all, as adding a software ID is done to the objects
                Config.UseAdapterId = true;         //Tag objects with a software specific id in the CustomData. Requires the NextIndex method to be overridden and implemented


                m_TeklaModel = new Model();
                m_CatalogHandler = new CatalogHandler();

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
                    m_ProfileEnumerator = m_CatalogHandler.GetProfileItems();
                    m_MaterialEnumerator = m_CatalogHandler.GetMaterialItems();

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

        //Add any comlink object as a private field here, example named:

        //private SoftwareComLink m_softwareNameCom;

        private Model m_TeklaModel;
        private CatalogHandler m_CatalogHandler;
        private ModelObjectSelector m_ObjectSelector;
        private ComponentItemEnumerator m_ComponentEnumerator;
        private ProfileItemEnumerator m_ProfileEnumerator;
        private MaterialItemEnumerator m_MaterialEnumerator;

        
        /***************************************************/


    }
}
