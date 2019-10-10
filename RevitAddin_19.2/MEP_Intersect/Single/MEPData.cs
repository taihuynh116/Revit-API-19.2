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

namespace SingleData
{
    public class MEPData
    {
        private static MEPData instance;
        public static MEPData Instance
        {
            get
            {
                if(instance == null)
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

        
    }
}
