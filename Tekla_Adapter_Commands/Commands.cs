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

namespace BH.oM.Adapter.Commands
{
    public class ChangeWorkPlane_Tekla : IExecuteCommand, IObject
    {

    }