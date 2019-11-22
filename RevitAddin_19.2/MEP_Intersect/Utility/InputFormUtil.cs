using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SingleData;
using Utility;

namespace Utility
{
    public static class InputFormUtil
    {
        private static RevitData revitData
        {
            get { return RevitData.Instance; }
        }
        private static ModelData modelData
        {
            get { return ModelData.Instance; }
        }
        private static FormData formData
        {
            get { return FormData.Instance; }
        }
        public static void PickElement()
        {
            var form = formData.InputForm;
            form.Hide();
            var sel = revitData.Selection;

            var elem = sel.PickObject(Autodesk.Revit.UI.Selection.ObjectType.Element).GetElement().GetIntersectElements();
            var elemIds = elem.Select(x => x.Id).ToList();
            sel.SetElementIds(elemIds);
            form.ShowDialog();
        }
        public static void ShowClash()
        {

        }
    }
}
