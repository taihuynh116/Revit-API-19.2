using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.Attributes;
using SingleData;
using Utility;
using Autodesk.Revit.UI.Selection;

namespace BeamSection
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
            var sel = revitData.Selection;

            var collector = new FilteredElementCollector(doc);
            var types = collector.WhereElementIsElementType();

            Func<Element, bool> sectionFamilyTypeFilter = x => x is ViewFamilyType
                                && (x as ViewFamilyType).ViewFamily == ViewFamily.Detail;
            var sectionFamilyType = types.Single(sectionFamilyTypeFilter).Id;                    
            
            
            Func<Element, bool> beamFilter = x => 
                                x.Category != null &&
                                (BuiltInCategory)x.Category.Id.IntegerValue == BuiltInCategory.OST_StructuralFraming;

            var beamSelectionFilter = new GenericSelectionFilter(beamFilter);
            var beam = sel.PickObject(ObjectType.Element, beamSelectionFilter).GetElement()
                .GetEntityElement();

            var geometry = beam.Geometry;
            var basisX = geometry.BasisX;// be rong
            var basisY = geometry.BasisY; // be cao
            var basisZ = geometry.BasisZ; // chieu dai

            var origin = geometry.Origin;
            var lengthX = geometry.LengthX;
            var lengthY = geometry.LengthY;
            var lengthZ = geometry.LengthZ;

            TaskDialog.Show("Revit", $"Toa do X: {basisX}, Toa do Y: {basisY}, Toa do Z: {basisZ}" +
                            $"\n Chieu dai X: {lengthX}, chieu dai Y: {lengthY}");

            

            BoundingBoxXYZ bb = new BoundingBoxXYZ();
            Transform tf = Transform.Identity;
            //tf.BasisX = 
            tf.Origin = geometry.Origin + basisZ * lengthZ / 2;
            tf.BasisX = geometry.BasisX;
            tf.BasisY = geometry.BasisY;
            tf.BasisZ = geometry.BasisZ;
           
            bb.Transform = tf;

            var pntMax = new XYZ(lengthX, lengthY, 100.milimeter2Feet());
            var pntMin = new XYZ(0, 0, 100.milimeter2Feet());

            bb.Max = pntMax;
            bb.Min = pntMin;
            var tx = revitData.Transaction;

            tx.Start();
            var sectionDetail = ViewSection.CreateDetail(doc, sectionFamilyType,bb);

            //var bb1 = new BoundingBoxXYZ();
            //bb1.Min = XYZ.Zero;
            //bb1.Max = XYZ.Zero + XYZ.BasisX * 1.0.milimeter2Feet()
            //    + XYZ.BasisY * 1.0.milimeter2Feet()
            //    + XYZ.BasisZ * 1.0.milimeter2Feet();

            sectionDetail.CropBox = bb;
            tx.Commit();
            RevitDataUtil.Dispose();

            return Result.Succeeded;
        }
    }
}
