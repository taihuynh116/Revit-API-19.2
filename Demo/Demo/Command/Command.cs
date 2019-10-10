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

namespace Demo
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

            TaskDialog.Show("Revit", $"{revitData.Families.Count()}");

            

            RevitDataUtil.Dispose();
            return Result.Succeeded;
        }
    }
}
