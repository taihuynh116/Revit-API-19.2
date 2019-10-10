using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;

namespace Utility
{
    // Kiểu dữ liệu định nghĩa bộ lọc đối tượng một cách tổng quát
    public class GenericSelectionFilter : ISelectionFilter
    {
        private Func<Element, bool> filterElement;
        public GenericSelectionFilter(Func<Element,bool> filterElement)
        {
            this.filterElement = filterElement;
        }
        public bool AllowElement(Element elem)
        {
            return filterElement(elem);
        }

        public bool AllowReference(Reference reference, XYZ position)
        {
            throw new NotImplementedException();
        }
    }
}
