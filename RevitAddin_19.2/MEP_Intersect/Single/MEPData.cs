using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.UI.Selection;
using Utility;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using SingleData;
using Autodesk.Revit.DB.Plumbing;
using Autodesk.Revit.DB.Mechanical;
using Autodesk.Revit.DB.Electrical;

namespace SingleData
{
    public class MEPData
    {
        private static MEPData instance;
        public static MEPData Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MEPData();
                }
                return instance;
            }
            set
            {
                instance = value;
            }
        }

        private IEnumerable<Autodesk.Revit.DB.MEPCurve> mepPipes;
        public virtual IEnumerable<Autodesk.Revit.DB.MEPCurve> MepPipes
        {
            get
            {
                if (mepPipes == null)
                {
                    if (mepPipes is Pipe)
                    {
                        mepPipes = RevitData.Instance.InstanceElements.Where(x => x.Category != null && x is Pipe).Cast<Pipe>();
                    }
                    if (mepPipes is PipeInsulation)
                    {
                        mepPipes = RevitData.Instance.InstanceElements.Where(x => x.Category != null && x is PipeInsulation).Cast<PipeInsulation>();
                    }
                }
                return mepPipes;
            }
        }

        private IEnumerable<Autodesk.Revit.DB.MEPCurve> mepDucts;
        public virtual IEnumerable<Autodesk.Revit.DB.MEPCurve> MepDucts
        {
            get
            {
                if (mepDucts == null)
                {
                    {
                        if (mepDucts is Duct)
                        {
                            mepDucts = RevitData.Instance.InstanceElements.Where(x => x.Category != null && x is Duct).Cast<Duct>();
                        }
                        if (mepDucts is DuctInsulation)
                        {
                            mepDucts = RevitData.Instance.InstanceElements.Where(x => x.Category != null && x is DuctInsulation).Cast<DuctInsulation>();
                        }
                    }

                }
                return mepDucts;
            }
        }

        private IEnumerable<Autodesk.Revit.DB.Electrical.CableTray> cableTrays;
        public virtual IEnumerable<Autodesk.Revit.DB.Electrical.CableTray> CableTrays
        {
            get
            {
                if (cableTrays == null)
                {
                    cableTrays = RevitData.Instance.InstanceElements.Where(x => x.Category != null && x is CableTray).Cast<CableTray>();
                }
                return cableTrays;
            }
        }

        private IEnumerable<FamilyInstance> mechEquipments;
        public virtual IEnumerable<FamilyInstance> MechEquipments
        {
            get
            {
                if (mechEquipments == null)
                {
                    mechEquipments = RevitData.Instance.FamilyInstances.Where
                                     (x => x.Category.IsEqual(BuiltInCategory.OST_MechanicalEquipment));
                }
                return mechEquipments;
            }
        }

        private IEnumerable<Autodesk.Revit.DB.MEPCurve> mepElements;
        public IEnumerable<Autodesk.Revit.DB.MEPCurve> MepElements
        {
            get
            {
                if (mepElements == null)
                {
                    foreach (var elem in mepElements)
                    {
                        var mepIns = RevitData.Instance.InstanceElements;

                        if(elem is CableTray)
                            return mepIns.Where(x => x.Category != null && x is CableTray).Cast<CableTray>();
                        if (elem is Duct)
                            return mepIns.Where(x => x.Category != null && x is Duct).Cast<Duct>();
                        if (elem is DuctInsulation)
                            return mepIns.Where(x => x.Category != null && x is DuctInsulation).Cast<DuctInsulation>();
                        if (elem is Pipe)
                            return mepIns.Where(x => x.Category != null && x is Pipe).Cast<Pipe>();
                        if (elem is PipeInsulation)
                            return mepIns.Where(x => x.Category != null && x is PipeInsulation).Cast<PipeInsulation>();
                    }
                }
                return mepElements;
            }     
        }
    }
}
