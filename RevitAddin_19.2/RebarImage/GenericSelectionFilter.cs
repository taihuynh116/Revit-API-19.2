using Autodesk.Revit.DB;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selection_FilterElement
{
    //Kiểu dữ liệu định nghĩa bộ lọc đối tượng tổng quát
    public class GenericSelectionFilter : ISelectionFilter
    {
        private Func<Element, bool> FilterElement;
        public GenericSelectionFilter(Func<Element,bool> filterElement)
        {
            this.FilterElement = filterElement;
        }
        public bool AllowElement(Element elem)
        {
            return FilterElement(elem);
        }

        public bool AllowReference(Reference reference, XYZ position)
        {
            throw new NotImplementedException();
        }
    }
}
