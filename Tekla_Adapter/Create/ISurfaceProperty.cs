using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BH.oM.Structure.Elements;
using BH.oM.Structure.Properties.Section;
using BH.oM.Structure.Properties.Constraint;
using BH.oM.Common.Materials;
using BH.Engine.Tekla;
using BH.oM.Structure.Properties.Surface;

using Tekla.Structures;
using Tekla.Structures.Model;
using Tekla.Structures.Filtering;
using tsGeo = Tekla.Structures.Geometry3d;

namespace BH.Adapter.Tekla
{
    public partial class TeklaAdapter
    {
        private bool Create(IEnumerable<ISurfaceProperty> properties)
        {
            //Code for creating a collection of section properties in the software
            bool success = true;

            foreach (ISurfaceProperty sectionProperty in properties)
            {
                if (sectionProperty is ConstantThickness)
                {
                    Profile asdf = new Profile();
                    asdf.ProfileString = "PL" + "5";
                }
                ////Tip: if the NextId method has been implemented you can get the id to be used for the creation out as (cast into applicable type used by the software):
                //object secPropId = sectionProperty.CustomData[AdapterId];
                ////If also the default implmentation for the DependencyTypes is used,
                ////one can from here get the id's of the subobjects by calling (cast into applicable type used by the software): 
                //object materialId = sectionProperty.Material.CustomData[AdapterId];

                ////proposed functionality:
                ////check if section exists in the loaded catalog from tekla - m_ProfileEnumerator() - if not add the material
            }

            return success;
        }
    }
}
