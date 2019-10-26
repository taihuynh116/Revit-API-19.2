using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SingleData;
using Autodesk.Revit.DB;
using Utility;

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

        public static MEPData mepData
        {
            get
            {
                return MEPData.Instance;
            }
        }

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
        }

        public static IEnumerable<Autodesk.Revit.DB.Element> GetIntersectElements(this Autodesk.Revit.DB.Element elem)
        {
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

            var eisFilter = new ElementIntersectsElementFilter(elem);
            var mepCollector = new FilteredElementCollector(doc, mepElementIds);

            var intersecElements = mepCollector.WherePasses(bbiFilter).WherePasses(eisFilter);
            //var intersecElementIds = intersecElements.Select(x => x.Id).ToList();

            //sel.SetElementIds(intersecElementIds);
            return intersecElements;
        }

        public static IEnumerable<Model.Entity.Element> GetIntersectEntityElements(this Model.Entity.Element entityElement)
        {
            return modelData.MEPEntityElements.Where(x => entityElement.RevitElement.GetIntersectElements().Contains(x));
        }
    }
}
