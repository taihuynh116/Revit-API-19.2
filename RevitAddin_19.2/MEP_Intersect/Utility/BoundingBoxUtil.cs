using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SingleData;

namespace Utility
{
    public static class BoundingBoxUtil
    {
        public static RevitData Instance
        {
            get
            {
                return RevitData.Instance;
            }
        }
        public static double GetBoundingBox(this Autodesk.Revit.DB.Element elem)
        {
            var bb = elem.get_BoundingBox(null);
            return bb.Max.Z - bb.Min.Z;
        }
<<<<<<< HEAD
        public static BoundingBoxIntersectsFilter GetBoundingBoxIntersectsFilter(this Autodesk.Revit.DB.Element elem)
        {
            var bb = elem.get_BoundingBox(null);
            Outline outLine = new Outline(bb.Min, bb.Max);
            BoundingBoxIntersectsFilter bbFilter = new BoundingBoxIntersectsFilter(outLine);
=======
        public static BoundingBoxIntersectsFilter GetBoundingBoxIntersectsFilter()
        {
            var bb = new BoundingBoxXYZ();
            Outline outLine = new Outline(bb.Max,bb.Min);
            BoundingBoxIntersectsFilter bbFilter = new BoundingBoxIntersectsFilter(outLine);
            var collector = new FilteredElementCollector(RevitData.Instance.Document);
            List<Element> elements = collector.WherePasses(bbFilter).ToList();

>>>>>>> a83d5df3a889ba4aaee4e9fa82b4f67c346ff260
            return bbFilter;
        }
    }
}
