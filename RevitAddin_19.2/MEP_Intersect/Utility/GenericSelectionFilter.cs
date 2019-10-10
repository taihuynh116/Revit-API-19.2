using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;

namespace Selection_FilterElement
{
    // Kieu du lieu dinh nghia bo loc doi tuong Tong quat
    public class GenericSelectionFilter : ISelectionFilter
    {
        private Func<Element, bool> filterElement;
        public GenericSelectionFilter(Func<Element,bool> FilterElement)
        {
            this.filterElement = FilterElement;
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
