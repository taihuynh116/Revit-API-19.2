using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using SingleData;

namespace Utility
{
    public static class SolidUtil
    {
        public static double GetProximityHeight(this Autodesk.Revit.DB.Element elem)
        {
            var bb = elem.get_BoundingBox(null);
            return bb.Max.Z - bb.Min.Z;
        }
        public static Autodesk.Revit.DB.Solid MoveToOrigin(this Autodesk.Revit.DB.Solid solid)
        {
            var bb = solid.GetBoundingBox();
            var tf = bb.Transform;
            var origin = tf.Origin;
            var translationTf = Autodesk.Revit.DB.Transform.CreateTranslation(-origin);
            return Autodesk.Revit.DB.SolidUtils.CreateTransformed(solid, translationTf);
        }
        public static Autodesk.Revit.DB.Solid GetSingleSolid(this Autodesk.Revit.DB.Element element)
        {
            var geoElem = element.get_Geometry(new Autodesk.Revit.DB.Options());
            return geoElem.GetSingleSolid();
        }
        public static Autodesk.Revit.DB.Solid GetSingleSolid(this IEnumerable<Autodesk.Revit.DB.GeometryObject> geoObjs)
        {
            foreach (Autodesk.Revit.DB.GeometryObject geoObj in geoObjs)
            {
                if(geoObj is Autodesk.Revit.DB.GeometryInstance)
                {
                    var s = (geoObj as Autodesk.Revit.DB.GeometryInstance).GetSingleSolid();
                    if (s != null) return s;
                }
                if(geoObj is Autodesk.Revit.DB.Solid)
                {
                    var solid = geoObj as Autodesk.Revit.DB.Solid;
                    if(solid != null && solid.Faces.Size != 0 && solid.Edges.Size != 0)
                    {
                        return solid;
                    }
                }
            }
            return null;
        }
        public static Autodesk.Revit.DB.Solid GetSingleSolid(this Autodesk.Revit.DB.GeometryInstance geoIns)
        {
            return GetSingleSolid(geoIns.GetInstanceGeometry());
        }
        public static Autodesk.Revit.DB.Solid GetOrigin(this Autodesk.Revit.DB.Element element)
        {
            if(element is Autodesk.Revit.DB.FamilyInstance)
            {
                var fi = element as Autodesk.Revit.DB.FamilyInstance;
                var orgGeoElem = fi.GetOriginalGeometry(new Autodesk.Revit.DB.Options());
                var solid = GetSingleSolid(orgGeoElem);
                var fiTrf = fi.GetTransform();
                return Autodesk.Revit.DB.SolidUtils.CreateTransformed(solid, fiTrf);
            }
            if(element is Autodesk.Revit.DB.Floor)
            {
                var floor = element as Autodesk.Revit.DB.Floor;
                var bottomRf = Autodesk.Revit.DB.HostObjectUtils.GetBottomFaces(floor).First();
                var bottomFace = element.GetGeometryObjectFromReference(bottomRf) as Autodesk.Revit.DB.PlanarFace;
                return Autodesk.Revit.DB.GeometryCreationUtilities.CreateExtrusionGeometry(bottomFace.GetEdgesAsCurveLoops(),
                                                                  -bottomFace.FaceNormal, floor.GetProximityHeight());
            }
            if(element is Autodesk.Revit.DB.Wall)
            {
                var wall = element as Autodesk.Revit.DB.Wall;
                var sideRf = Autodesk.Revit.DB.HostObjectUtils.GetSideFaces(wall,Autodesk.Revit.DB.ShellLayerType.Exterior).First();
                var sideFace = element.GetGeometryObjectFromReference(sideRf) as Autodesk.Revit.DB.PlanarFace;
                return Autodesk.Revit.DB.GeometryCreationUtilities.CreateExtrusionGeometry(sideFace.GetEdgesAsCurveLoops(),
                                                                   -sideFace.FaceNormal, wall.Width);
            }
            throw new Exception();
        }
        public static Autodesk.Revit.DB.Solid Scale(this Autodesk.Revit.DB.Solid solid, double factor)
        {
            var centerPoint = solid.ComputeCentroid();
            var tf = Autodesk.Revit.DB.Transform.Identity.ScaleBasis(factor);
            var scaleSolid = Autodesk.Revit.DB.SolidUtils.CreateTransformed(solid, tf);
            var newCenterPoint = scaleSolid.ComputeCentroid();
            var translateVec = centerPoint - newCenterPoint;
            var translateTf = Autodesk.Revit.DB.Transform.CreateTranslation(translateVec);

            return Autodesk.Revit.DB.SolidUtils.CreateTransformed(scaleSolid, translateTf);
        }
        public static Autodesk.Revit.DB.Solid Merge(this IEnumerable<Autodesk.Revit.DB.Solid> solids)
        {
            var mergeSolid = solids.First();
            foreach (var solid in solids)
            {
                if (mergeSolid == solid)
                    continue;
                mergeSolid = Autodesk.Revit.DB.BooleanOperationsUtils.ExecuteBooleanOperation(mergeSolid, solid, Autodesk.Revit.DB.BooleanOperationsType.Union);
            }
            return mergeSolid;
        }
        public static Autodesk.Revit.DB.Solid Difference(this Autodesk.Revit.DB.Solid targetSolid, IEnumerable<Autodesk.Revit.DB.Solid> otherSolid)
        {
            var mergeOtherSolid = otherSolid.Merge();
            var mergeAllSolid = BooleanOperationsUtils.ExecuteBooleanOperation(mergeOtherSolid, targetSolid, BooleanOperationsType.Union);
            return BooleanOperationsUtils.ExecuteBooleanOperation(mergeAllSolid, mergeOtherSolid, BooleanOperationsType.Difference);
        }
        public static Autodesk.Revit.DB.BoundingBoxXYZ ScaleBoundingBox(Autodesk.Revit.DB.BoundingBoxXYZ bb, double a)
        {
            var min = bb.Min; var max = bb.Max; var origin = (min + max) / 2;
            return new Autodesk.Revit.DB.BoundingBoxXYZ
            { Min = origin + (min - origin) * a, Max = origin + (max - origin) * a };
        }
    }
}
