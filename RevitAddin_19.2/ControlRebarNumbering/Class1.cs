using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;
using SingleData;

namespace ControlRebarNumbering
{
    [Transaction(TransactionMode.Manual)]
    public class Command : IExternalCommand
    {
        private RevitData revitData
        {
            get
            {
                return RevitData.Instance;
            }
        }
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            revitData.UIApplication = commandData.Application;
            var doc = revitData.Document;

            //TAP HOP SHEET
            var collector = new FilteredElementCollector(doc);
            var sheet = collector.WhereElementIsNotElementType();
            Func<Element, bool> sheetFilter = x => x is ViewSheet;
            var sheets = sheet.Where(sheetFilter);
            TaskDialog.Show("Revit", $"Quantity of Sheet:{sheets.Count()})");

            //TAP HOP VIEWPORT
            collector = new FilteredElementCollector(doc);
            var viewPort = collector.WhereElementIsNotElementType();
            Func<Element, bool> viewPortFilter = x => x is Viewport;
            var viewPorts = viewPort.Where(viewPortFilter);
            TaskDialog.Show("Revit", $"Quantity of ViewPort: {viewPorts.Count()}");


            RevitDataUtil.Dispose();
            return  Result.Succeeded;
        }
    }
}
