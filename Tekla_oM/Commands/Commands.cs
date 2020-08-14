using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BH.oM.Base;
using BH.oM.Adapter;
using BH.oM.Adapter.Commands;
using BH.oM.Geometry;
using BH.oM.Reflection;


namespace BH.oM.Adapters.Tekla.Commands
{
    public class ChangeWorkPlane : IExecuteCommand, IObject
    {
        public BH.oM.Geometry.Plane Plane { get; set; } = null;
        public BH.oM.Geometry.Vector XVector { get; set; } = null;
        public BH.oM.Geometry.Vector YVector { get; set; } = null;
        public BH.oM.Geometry.Point Origin { get; set; } = null;

    }
}