using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
<<<<<<< HEAD
using SingleData;
using Utility;
=======
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.UI.Selection;
using Utility;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using SingleData;
using Autodesk.Revit.DB.Plumbing;
using Autodesk.Revit.DB.Mechanical;
using Autodesk.Revit.DB.Electrical;
>>>>>>> a83d5df3a889ba4aaee4e9fa82b4f67c346ff260

namespace SingleData
{
    public class ModelData
    {
        private static ModelData instance;
        public static ModelData Instance
        {
<<<<<<< HEAD
            get { if (instance == null) instance = new ModelData(); return instance; }
            set { instance = value; }
        }

        private List<Model.Entity.ElementType> elementTypes;
        public List<Model.Entity.ElementType> ElementTypes
        {
            get
            {
                if (elementTypes == null)
                {
                    elementTypes = new List<Model.Entity.ElementType>
                    {
                        Model.Entity.ElementType.Duct,
                        Model.Entity.ElementType.Pipe,
                        Model.Entity.ElementType.CableTray,
                        Model.Entity.ElementType.Equipment
                    };
                }
                return elementTypes;
            }
        }

        private List<Model.Entity.Element> mepEntityElements;
        public List<Model.Entity.Element> MEPEntityElements
        {
            get
            {
                if (mepEntityElements == null) MEPUtil.GetMEPEntityElement();
                return mepEntityElements;
            }
            set
            {
                mepEntityElements = value;
            }
        }

        private List<Model.Entity.Element> equipEntityElements;
        public List<Model.Entity.Element> EquipEntityElements
        {
            get
            {
                if (equipEntityElements == null) MEPUtil.GetEquipmentEntityElement();
                return equipEntityElements;
            }
            set { equipEntityElements = value; }
        }

        private List<Model.Entity.Element> pipeEntityElements;
        public List<Model.Entity.Element> PipeEntityElements
        {
            get
            {
                if (pipeEntityElements == null) MEPUtil.GetPipeEntityElement();
=======
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
>>>>>>> a83d5df3a889ba4aaee4e9fa82b4f67c346ff260
                return mepEntityElements;
            }
            set
            {
<<<<<<< HEAD
                pipeEntityElements = value;
            }
        }

        private List<Model.Entity.Element> ductEntityElements;
        public List<Model.Entity.Element> DuctEntityElements
        {
            get
            {
                if (ductEntityElements == null) MEPUtil.GetDuctEntityElement();
                return ductEntityElements;
            }
            set
            {
                ductEntityElements = value;
            }
        }

        private List<Model.Entity.Element> cableTrayEntityElements;
        public List<Model.Entity.Element> CableTrayEntityElements
        {
            get
            {
                if (cableTrayEntityElements == null) MEPUtil.GetCableTrayEntityElement();
                return cableTrayEntityElements;
            }
            set
            {
                cableTrayEntityElements = value;
=======
                mepEntityElements = value;
>>>>>>> a83d5df3a889ba4aaee4e9fa82b4f67c346ff260
            }
        }
    }
}
