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

        private RevitData revitData
        {
            get
            {
                return RevitData.Instance;
            }
        }

        // Get MEP Elements
        private IEnumerable<Autodesk.Revit.DB.Element> mepElements;
        public IEnumerable<Autodesk.Revit.DB.Element> MepElements
        {
            get
            {
                if(mepElements == null)
                {
                    mepElements = revitData.InstanceElements.Where(x => x is CableTray || x is MEPCurve ||
                        (x is FamilyInstance && x.Category != null && x.Category.IsEqual(BuiltInCategory.OST_MechanicalEquipment)));
                }
                return mepElements;
            }
            set
            {
                mepElements = value;
            }
        }

        // Get MEPCurve : Pipe, Duct, Insulation
        private IEnumerable<Autodesk.Revit.DB.MEPCurve> mepCurves;
        public IEnumerable<Autodesk.Revit.DB.MEPCurve> MepCurves
        {
            get
            {
                if(mepCurves == null)
                {
                    mepCurves = MepElements.OfType<MEPCurve>();
                }
                return mepCurves;
            }
            set
            {
                mepCurves = value;
            }
        }

        private IEnumerable<Autodesk.Revit.DB.MEPCurve> mepPipes;
        public virtual IEnumerable<Autodesk.Revit.DB.MEPCurve> MepPipes
        {
            get
            {
                if (mepPipes == null)
                {
                    mepPipes = mepCurves.Where(x => x is Pipe || x is PipeInsulation);
                }
                return mepPipes;
            }
            set
            {
                mepPipes = value;
            }
        }

        private IEnumerable<Autodesk.Revit.DB.MEPCurve> mepDucts;
        public virtual IEnumerable<Autodesk.Revit.DB.MEPCurve> MepDucts
        {
            get
            {
                if (mepDucts == null)
                {
                    mepDucts = mepCurves.Where(x => x is Duct || x is DuctInsulation);
                }
                return mepDucts;
            }
            set
            {
                mepDucts = value;
            }
        }

        private IEnumerable<Autodesk.Revit.DB.Electrical.CableTray> cableTrays;
        public virtual IEnumerable<Autodesk.Revit.DB.Electrical.CableTray> CableTrays
        {
            get
            {
                if (cableTrays == null)
                {
                    cableTrays = MepElements.OfType<CableTray>();
                }
                return cableTrays;
            }
            set
            {
                cableTrays = value;
            }
        }

        private IEnumerable<FamilyInstance> mechEquipments;
        public virtual IEnumerable<FamilyInstance> MechEquipments
        {
            get
            {
                if (mechEquipments == null)
                {
                    mechEquipments = MepElements.OfType<FamilyInstance>()
                                     .Where(x => x.Category != null && x.Category.IsEqual(BuiltInCategory.OST_MechanicalEquipment));
                }
                return mechEquipments;
            }
        }
    }
}
