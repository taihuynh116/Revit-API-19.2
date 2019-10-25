using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.Attributes;
using Selection_FilterElement;
using SingleData;
using Autodesk.Revit.DB.Mechanical;
using Autodesk.Revit.DB.Plumbing;
using Autodesk.Revit.DB.Electrical;
using Utility;

namespace MEP_Intersect
{
    [Transaction(TransactionMode.Manual)]
    public class Class1 : IExternalCommand
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
            var sel = revitData.UIDocument.Selection;

            var collector = new FilteredElementCollector(doc);
            Func<Element, ElementId> getElemIdFunc = x => x.Id;

            #region Tap hop cac doi tuong Viewport & ViewSheet
            Func<Element, bool> viewportFilter = x => x.Category != null && x.Category.IsEqual(BuiltInCategory.OST_Viewports);
            var viewport = revitData.InstanceElements.Where(viewportFilter);

            Func<View, bool> viewSheetFilter = x => x.Category != null && x.Category.IsEqual(BuiltInCategory.OST_Sheets);
            var viewSheet = revitData.Views.Where(viewSheetFilter);
            #endregion

            // Tap hop cac doi tuong MEP
            Func<Element, bool> pipeFilter = x => x.Category != null && (x.Category.IsEqual(BuiltInCategory.OST_PipeCurves) || x.Category.IsEqual(BuiltInCategory.OST_PipeFitting) ||
                                                                         x.Category.IsEqual(BuiltInCategory.OST_PipeInsulations) || x.Category.IsEqual(BuiltInCategory.OST_PipeFittingInsulation));
            var pipes = revitData.InstanceElements.Where(pipeFilter);
            var pipeIds = pipes.Select(getElemIdFunc).ToList();

            Func<Element, bool> ductFilter = x => x.Category != null && (x.Category.IsEqual(BuiltInCategory.OST_DuctCurves) || x.Category.IsEqual(BuiltInCategory.OST_DuctFitting) ||
                                                                         x.Category.IsEqual(BuiltInCategory.OST_DuctInsulations) || x.Category.IsEqual(BuiltInCategory.OST_DuctFittingInsulation) ||
                                                                         x.Category.IsEqual(BuiltInCategory.OST_MechanicalEquipment));
            var ducts = revitData.InstanceElements.Where(ductFilter);
            var ductIds = ducts.Select(x => x.Id).ToList();

            Func<Element, bool> cableTrayFilter = x => x.Category != null && (x.Category.IsEqual(BuiltInCategory.OST_CableTray) || x.Category.IsEqual(BuiltInCategory.OST_CableTrayFitting));
            var cableTrays = revitData.InstanceElements.Where(cableTrayFilter);
            var cableTrayIds = cableTrays.Select(x => x.Id).ToList();

            TaskDialog.Show("Revit", $"{ducts.Count()}");
            sel.SetElementIds(ductIds);

            RevitDataUtil.Dispose();
            return Result.Succeeded;
        }
    }
}
