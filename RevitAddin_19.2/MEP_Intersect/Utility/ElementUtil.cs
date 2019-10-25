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
    public static class ElementUtil
    {
        public static RevitData Instance
        {
            get
            {
                return RevitData.Instance;
            }
        }

        public static List<Autodesk.Revit.DB.Element> GetIntersectElements(this Autodesk.Revit.DB.Element elem)
        {
            var doc = RevitData.Instance.Document;
            var sel = RevitData.Instance.Selection;
            //var collector = new FilteredElementCollector(doc);

            var instances = RevitData.Instance.InstanceElements;
            var mepBic = new List<BuiltInCategory>
            {
                BuiltInCategory.OST_PipeCurves,
                BuiltInCategory.OST_PipeInsulations,
                BuiltInCategory.OST_DuctCurves,
                BuiltInCategory.OST_DuctInsulations,
                BuiltInCategory.OST_CableTray,
                BuiltInCategory.OST_MechanicalEquipment
            };
            Func<Element, bool> mepFilter = x => x.Category != null && x.Id.IntegerValue != x.Id.IntegerValue &&
                                                 mepBic.Contains((BuiltInCategory)x.Category.Id.IntegerValue);
            // Trừ đi đối tượng là chính nó
            var mepElements = instances.Where(mepFilter);
            var mepElementIds = mepElements.Select(x => x.Id).ToList();

            var eisFilter = new ElementIntersectsElementFilter(elem);
            var mepCollector = new FilteredElementCollector(doc, mepElementIds);

            var intersecElements = mepCollector.WherePasses(eisFilter);
            var intersecElementIds = intersecElements.Select(x => x.Id).ToList();

            sel.SetElementIds(intersecElementIds);
            return null;
        }

        //public List<Model.Entity.Element> GetIntersectEntityElements(this Model.Entity.Element entityElement)
        //{
        //    var revitElem = entityElement.RevitElement;

        //}
    }
}
