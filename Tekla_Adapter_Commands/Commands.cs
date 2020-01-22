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
using Tekla.Structures;
using Tekla.Structures.Model;
using tsGeo = Tekla.Structures.Geometry3d;

namespace BH.oM.Adapter.Commands.Tekla
{
    public class ChangeWorkPlane : IExecuteCommand, IObject
    {
        public BH.oM.Geometry.Plane Plane { get; set; } = null;

        public BH.oM.Geometry.Vector XVextor { get; set; } = null;
        public BH.oM.Geometry.Vector YVextor { get; set; } = null;


    }