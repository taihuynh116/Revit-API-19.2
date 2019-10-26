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
    public class ModelData
    {
        private static ModelData instance;
        public static ModelData Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ModelData();
                }
                return instance;
            }
            set
            {
                instance = value;
            }
        }

        // Tạo các tập hợp chứ các EntityElement để quản lý
        private List<Model.Entity.Element> mepEntityElements;
        public List<Model.Entity.Element> MEPEntityElements
        {
            get
            {
                if (mepEntityElements == null)
                {
                    mepEntityElements = MEPUtil.GetMEPEntityElements();
                }
                return mepEntityElements;
            }
            set
            {
                mepEntityElements = value;
            }
        }
    }
}
