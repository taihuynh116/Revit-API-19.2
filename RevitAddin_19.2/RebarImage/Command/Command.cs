using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SingleData;
using Utility;
using Autodesk.Revit.DB.Structure;
using Selection_FilterElement;
namespace RebarImage
{
    [Transaction (TransactionMode.Manual)]
    public class Command : IExternalCommand
    {
        private RevitData revitData
        {
            get { return RevitData.Instance; }
        }
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            revitData.UIApplication = commandData.Application;
            var doc = revitData.Document;
            var collector = new FilteredElementCollector(doc);
            var instance = collector.WhereElementIsNotElementType();

            var sel = revitData.Selection;
            var rebarSelectionFilter = new GenericSelectionFilter(x => x is Rebar);
            var rf = sel.PickObject(Autodesk.Revit.UI.Selection.ObjectType.Element, rebarSelectionFilter);
            var rb = doc.GetElement(rf) as Rebar;

           // var rebarCuvre = rb.GetCenterlineCurves(false,false,false,MultiplanarOption.IncludeOnlyPlanarCurves,)

            TaskDialog.Show("Revit", $"{rb.Name}");


           
           // TaskDialog.Show("Revit", $"{revitData.Families.Count()}");

            RevitDataUtil.Dispose();
            return Result.Succeeded;
        }
    }
}
