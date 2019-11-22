using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SingleData;
using Autodesk.Revit.DB;
<<<<<<< HEAD
using Autodesk.Revit.DB.Electrical;
=======
using Utility;
>>>>>>> a83d5df3a889ba4aaee4e9fa82b4f67c346ff260

namespace Utility
{
    public static class MEPUtil
    {
        public static RevitData revitData
        {
            get
            {
                return RevitData.Instance;
            }
        }
<<<<<<< HEAD
        public static ModelData modelData
        {
            get
            {
                return ModelData.Instance;
            }
        }
=======

>>>>>>> a83d5df3a889ba4aaee4e9fa82b4f67c346ff260
        public static MEPData mepData
        {
            get
            {
                return MEPData.Instance;
            }
        }
<<<<<<< HEAD
        private static Document doc
        {
            get
            {
                return revitData.Document;
            }
        }

        public static Model.Entity.ElementType GetElementType(this Model.Entity.Element elem)
        {
            var revitElem = elem.RevitElement;
            if (revitElem is Autodesk.Revit.DB.Mechanical.Duct)
                return Model.Entity.ElementType.Duct;
            if (revitElem is Autodesk.Revit.DB.Plumbing.Pipe)
                return Model.Entity.ElementType.Pipe;
            if (revitElem is Autodesk.Revit.DB.Electrical.CableTray)
                return Model.Entity.ElementType.CableTray;
            return Model.Entity.ElementType.Equipment;
        }
        public static List<Model.Entity.Element> GetMEPEntityElement()
        {
            return mepData.MepElements.Select(x => new Model.Entity.Element { RevitElement = x }).ToList();
        }
        public static List<Model.Entity.Element> GetEquipmentEntityElement()
        {
            return mepData.MechEquipments.Select(x => new Model.Entity.Element { RevitElement = x }).ToList();
        }
        public static List<Model.Entity.Element> GetPipeEntityElement()
        {
            return mepData.MepPipes.Select(x => new Model.Entity.Element { RevitElement = x }).ToList();
        }
        public static List<Model.Entity.Element> GetDuctEntityElement()
        {
            return mepData.MepDucts.Select(x => new Model.Entity.Element { RevitElement = x }).ToList();
        }
        public static List<Model.Entity.Element> GetCableTrayEntityElement()
        {
            return mepData.CableTrays.Select(x => new Model.Entity.Element { RevitElement = x }).ToList();
        }

        // Check MEP Entity Element with MEP Revit Doc
        public static bool IsEqual(this Model.Entity.Element entElement,Autodesk.Revit.DB.Element revitElement)
        {
            return entElement.RevitElement.Id == revitElement.Id;
        }
        // Check List<> Revit Element with Mep Entity Element
        public static bool Contains(this IEnumerable<Autodesk.Revit.DB.Element> elements,Model.Entity.Element entElement)
        {
            return elements.Any(x => entElement.IsEqual(x));
=======

        public static ModelData modelData
        {
            get
            {
                return ModelData.Instance;
            }
        }

        // Tạo thêm phương thức để lấy ra các MEP Entity Element để quản lý
        public static List<Model.Entity.Element> GetMEPEntityElements()
        {
            return mepData.MEPElements.Select(x => new Model.Entity.Element { RevitElement = x }).ToList();
        }

        // Tạo phương thức kiểm tra đối tượng Entity Element có trùng khớp với Revit Element không
        public static bool IsEqual(this Model.Entity.Element entElem, Autodesk.Revit.DB.Element revitElem)
        {
            return entElem.RevitElement.Id == revitElem.Id;
        }

        // Tạo phương thức kiểm tra tập hợp Revit Element có chứa đối tượng Entity Element đang xét hay không
        public static bool Contains(this IEnumerable<Autodesk.Revit.DB.Element> elements, Model.Entity.Element entElem)
        {
            return elements.Any(x => entElem.IsEqual(x));
>>>>>>> a83d5df3a889ba4aaee4e9fa82b4f67c346ff260
        }

        public static IEnumerable<Autodesk.Revit.DB.Element> GetIntersectElements(this Autodesk.Revit.DB.Element elem)
        {
<<<<<<< HEAD
            var instances = MEPData.Instance.MepElements;
            var mepElements = instances.Where(x => x.Id != elem.Id);
            var mepElementIds = mepElements.Select(x => x.Id).ToList();

            // Used BoundingBoxIntersectFilter
            var bb = elem.get_BoundingBox(null);
            var ol = new Outline(bb.Min, bb.Max);
            var bbFilter = new BoundingBoxIntersectsFilter(ol);
=======
            var doc = revitData.Document;
            var sel = revitData.Selection;
            //var collector = new FilteredElementCollector(doc);

            //var instances = RevitData.Instance.InstanceElements;
            // Ở đây mình sẽ lấy từ tập hợp các đối tượng MEPElement chứ không tất cả đối tượng trong dự án
            var instances = MEPData.Instance.MEPElements;

            //var mepBic = new List<BuiltInCategory>
            //{
            //    BuiltInCategory.OST_PipeCurves,
            //    BuiltInCategory.OST_PipeInsulations,
            //    BuiltInCategory.OST_DuctCurves,
            //    BuiltInCategory.OST_DuctInsulations,
            //    BuiltInCategory.OST_CableTray,
            //    BuiltInCategory.OST_MechanicalEquipment
            //};
            //Func<Element, bool> mepFilter = x => x.Category != null && x.Id.IntegerValue != x.Id.IntegerValue &&
            //                                     mepBic.Contains((BuiltInCategory)x.Category.Id.IntegerValue);

            // Khi đã lấy ra các đối tượng MEPElement, mình chỉ cần trừ ra chỉnh đối tượng đang kiểm tra là elem là đủ
            var mepElements = instances.Where(x=> x.Id != elem.Id);
            var mepElementIds = mepElements.Select(x => x.Id).ToList();

            // Dùng BoudingBoxIntersectFilter trước để hạn lọc bớt các đối tượng kiểm tra
            var bb = elem.get_BoundingBox(null);
            var ol = new Outline(bb.Min, bb.Max);
            var bbiFilter = new BoundingBoxIntersectsFilter(ol);
>>>>>>> a83d5df3a889ba4aaee4e9fa82b4f67c346ff260

            var eisFilter = new ElementIntersectsElementFilter(elem);
            var mepCollector = new FilteredElementCollector(doc, mepElementIds);

<<<<<<< HEAD
            var intersectElements = mepCollector.WherePasses(eisFilter);
            return intersectElements;
        }
        private static IEnumerable<Autodesk.Revit.DB.Element> GetEquipmentIntersectElement(this Autodesk.Revit.DB.Element elem)
        {
            var instances = MEPData.Instance.MechEquipments;
            var equipElements = instances.Where(x => x.Id != elem.Id);
            var equipElementIds = equipElements.Select(x => x.Id).ToList();

            var bb = elem.GetBoundingBoxIntersectsFilter();
            var eisEquipFilter = new ElementIntersectsElementFilter(elem);
            var equipCollector = new FilteredElementCollector(doc, equipElementIds);
            var equipIntersectElements = equipCollector.WherePasses(eisEquipFilter);
            return equipIntersectElements;
        }
        private static IEnumerable<Autodesk.Revit.DB.MEPCurve> GetPipeIntersecElements(this Autodesk.Revit.DB.Element elem)
        {
            var instances = MEPData.Instance.MepPipes;
            var pipeElements = instances.Where(x => x.Id != elem.Id);
            var pipeElementIds = pipeElements.Select(x => x.Id).ToList();
            var bb = elem.GetBoundingBoxIntersectsFilter();

            var eisPipeFilter = new ElementIntersectsElementFilter(elem);
            var pipeCollector = new FilteredElementCollector(doc, pipeElementIds);

            var pipeIntersectElements = pipeCollector.WherePasses(eisPipeFilter).Cast<MEPCurve>();
            return pipeIntersectElements;
        }
        private static IEnumerable<Autodesk.Revit.DB.MEPCurve> GetDuctIntersecElements(this Autodesk.Revit.DB.Element elem)
        {
            var instances = MEPData.Instance.MepDucts;
            var ductElements = instances.Where(x => x.Id != elem.Id);
            var ductElementIds = ductElements.Select(x => x.Id).ToList();
            var bb = elem.GetBoundingBoxIntersectsFilter();

            var eisDuctFilter = new ElementIntersectsElementFilter(elem);
            var ductCollector = new FilteredElementCollector(doc, ductElementIds);

            var ductIntersectElements = ductCollector.WherePasses(eisDuctFilter).Cast<MEPCurve>();
            return ductIntersectElements;
        }
        private static IEnumerable<Autodesk.Revit.DB.Electrical.CableTray> GetCableTrayIntersecElements(this Autodesk.Revit.DB.Element elem)
        {
            var instances = MEPData.Instance.CableTrays;
            var trayElements = instances.Where(x => x.Id != elem.Id);
            var trayElementIds = trayElements.Select(x => x.Id).ToList();
            var bb = elem.GetBoundingBoxIntersectsFilter();

            var eisTrayFilter = new ElementIntersectsElementFilter(elem);
            var trayCollector = new FilteredElementCollector(doc, trayElementIds);

            var trayIntersectElements = trayCollector.WherePasses(eisTrayFilter).Cast<CableTray>();
            return trayIntersectElements;
=======
            var intersecElements = mepCollector.WherePasses(bbiFilter).WherePasses(eisFilter);
            //var intersecElementIds = intersecElements.Select(x => x.Id).ToList();

            //sel.SetElementIds(intersecElementIds);
            return intersecElements;
>>>>>>> a83d5df3a889ba4aaee4e9fa82b4f67c346ff260
        }

        public static IEnumerable<Model.Entity.Element> GetIntersectEntityElements(this Model.Entity.Element entityElement)
        {
            return modelData.MEPEntityElements.Where(x => entityElement.RevitElement.GetIntersectElements().Contains(x));
        }
<<<<<<< HEAD
        public static IEnumerable<Model.Entity.Element> GetEquipmentIntersectElements(this Model.Entity.Element equipEntityElement)
        {
            return modelData.EquipEntityElements.Where(x => equipEntityElement.RevitElement.GetIntersectElements().Contains(x));
        }
        // RevitMEPCurve ??
        public static IEnumerable<Model.Entity.Element> GetPipeIntersectEntityElements(this Model.Entity.Element pipeEntityElement)
        {
            return modelData.PipeEntityElements.Where(x => pipeEntityElement.RevitElement.GetPipeIntersecElements().Contains(x));
        }
        public static IEnumerable<Model.Entity.Element> GetDuctIntersectEntityElements(this Model.Entity.Element ductEntityElement)
        {
            return modelData.DuctEntityElements.Where(x => ductEntityElement.RevitElement.GetDuctIntersecElements().Contains(x));
        }
        public static IEnumerable<Model.Entity.Element> GetCableTrayIntersectElements(this Model.Entity.Element cableTrayEntityElement)
        {
            return modelData.CableTrayEntityElements.Where(x => cableTrayEntityElement.RevitElement.GetCableTrayIntersecElements().Contains(x));
        }
=======
>>>>>>> a83d5df3a889ba4aaee4e9fa82b4f67c346ff260
    }
}
