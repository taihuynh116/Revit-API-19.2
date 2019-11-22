using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SingleData;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Electrical;

namespace Utility
{
    public static class MEPUtil
    {
        public static RevitData revitData
        {
            get
            {
                return RevitData.Instance;
            }
        }
        public static ModelData modelData
        {
            get
            {
                return ModelData.Instance;
            }
        }
        public static MEPData mepData
        {
            get
            {
                return MEPData.Instance;
            }
        }
        private static Document doc
        {
            get
            {
                return revitData.Document;
            }
        }

        public static Model.Entity.ElementType GetElementType(this Model.Entity.Element elem)
        {
            var revitElem = elem.RevitElement;
            if (revitElem is Autodesk.Revit.DB.Mechanical.Duct)
                return Model.Entity.ElementType.Duct;
            if (revitElem is Autodesk.Revit.DB.Plumbing.Pipe)
                return Model.Entity.ElementType.Pipe;
            if (revitElem is Autodesk.Revit.DB.Electrical.CableTray)
                return Model.Entity.ElementType.CableTray;
            return Model.Entity.ElementType.Equipment;
        }
        public static List<Model.Entity.Element> GetMEPEntityElement()
        {
            return mepData.MepElements.Select(x => new Model.Entity.Element { RevitElement = x }).ToList();
        }
        public static List<Model.Entity.Element> GetEquipmentEntityElement()
        {
            return mepData.MechEquipments.Select(x => new Model.Entity.Element { RevitElement = x }).ToList();
        }
        public static List<Model.Entity.Element> GetPipeEntityElement()
        {
            return mepData.MepPipes.Select(x => new Model.Entity.Element { RevitElement = x }).ToList();
        }
        public static List<Model.Entity.Element> GetDuctEntityElement()
        {
            return mepData.MepDucts.Select(x => new Model.Entity.Element { RevitElement = x }).ToList();
        }
        public static List<Model.Entity.Element> GetCableTrayEntityElement()
        {
            return mepData.CableTrays.Select(x => new Model.Entity.Element { RevitElement = x }).ToList();
        }

        // Check MEP Entity Element with MEP Revit Doc
        public static bool IsEqual(this Model.Entity.Element entElement,Autodesk.Revit.DB.Element revitElement)
        {
            return entElement.RevitElement.Id == revitElement.Id;
        }
        // Check List<> Revit Element with Mep Entity Element
        public static bool Contains(this IEnumerable<Autodesk.Revit.DB.Element> elements,Model.Entity.Element entElement)
        {
            return elements.Any(x => entElement.IsEqual(x));
        }

        public static IEnumerable<Autodesk.Revit.DB.Element> GetIntersectElements(this Autodesk.Revit.DB.Element elem)
        {
            var instances = MEPData.Instance.MepElements;
            var mepElements = instances.Where(x => x.Id != elem.Id);
            var mepElementIds = mepElements.Select(x => x.Id).ToList();

            // Used BoundingBoxIntersectFilter
            var bb = elem.get_BoundingBox(null);
            var ol = new Outline(bb.Min, bb.Max);
            var bbFilter = new BoundingBoxIntersectsFilter(ol);

            var eisFilter = new ElementIntersectsElementFilter(elem);
            var mepCollector = new FilteredElementCollector(doc, mepElementIds);

            var intersectElements = mepCollector.WherePasses(eisFilter);
            return intersectElements;
        }
        private static IEnumerable<Autodesk.Revit.DB.Element> GetEquipmentIntersectElement(this Autodesk.Revit.DB.Element elem)
        {
            var instances = MEPData.Instance.MechEquipments;
            var equipElements = instances.Where(x => x.Id != elem.Id);
            var equipElementIds = equipElements.Select(x => x.Id).ToList();

            var bb = elem.GetBoundingBoxIntersectsFilter();
            var eisEquipFilter = new ElementIntersectsElementFilter(elem);
            var equipCollector = new FilteredElementCollector(doc, equipElementIds);
            var equipIntersectElements = equipCollector.WherePasses(eisEquipFilter);
            return equipIntersectElements;
        }
        private static IEnumerable<Autodesk.Revit.DB.MEPCurve> GetPipeIntersecElements(this Autodesk.Revit.DB.Element elem)
        {
            var instances = MEPData.Instance.MepPipes;
            var pipeElements = instances.Where(x => x.Id != elem.Id);
            var pipeElementIds = pipeElements.Select(x => x.Id).ToList();
            var bb = elem.GetBoundingBoxIntersectsFilter();

            var eisPipeFilter = new ElementIntersectsElementFilter(elem);
            var pipeCollector = new FilteredElementCollector(doc, pipeElementIds);

            var pipeIntersectElements = pipeCollector.WherePasses(eisPipeFilter).Cast<MEPCurve>();
            return pipeIntersectElements;
        }
        private static IEnumerable<Autodesk.Revit.DB.MEPCurve> GetDuctIntersecElements(this Autodesk.Revit.DB.Element elem)
        {
            var instances = MEPData.Instance.MepDucts;
            var ductElements = instances.Where(x => x.Id != elem.Id);
            var ductElementIds = ductElements.Select(x => x.Id).ToList();
            var bb = elem.GetBoundingBoxIntersectsFilter();

            var eisDuctFilter = new ElementIntersectsElementFilter(elem);
            var ductCollector = new FilteredElementCollector(doc, ductElementIds);

            var ductIntersectElements = ductCollector.WherePasses(eisDuctFilter).Cast<MEPCurve>();
            return ductIntersectElements;
        }
        private static IEnumerable<Autodesk.Revit.DB.Electrical.CableTray> GetCableTrayIntersecElements(this Autodesk.Revit.DB.Element elem)
        {
            var instances = MEPData.Instance.CableTrays;
            var trayElements = instances.Where(x => x.Id != elem.Id);
            var trayElementIds = trayElements.Select(x => x.Id).ToList();
            var bb = elem.GetBoundingBoxIntersectsFilter();

            var eisTrayFilter = new ElementIntersectsElementFilter(elem);
            var trayCollector = new FilteredElementCollector(doc, trayElementIds);

            var trayIntersectElements = trayCollector.WherePasses(eisTrayFilter).Cast<CableTray>();
            return trayIntersectElements;
        }

        public static IEnumerable<Model.Entity.Element> GetIntersectEntityElements(this Model.Entity.Element entityElement)
        {
            return modelData.MEPEntityElements.Where(x => entityElement.RevitElement.GetIntersectElements().Contains(x));
        }
        public static IEnumerable<Model.Entity.Element> GetEquipmentIntersectElements(this Model.Entity.Element equipEntityElement)
        {
            return modelData.EquipEntityElements.Where(x => equipEntityElement.RevitElement.GetIntersectElements().Contains(x));
        }
        // RevitMEPCurve ??
        public static IEnumerable<Model.Entity.Element> GetPipeIntersectEntityElements(this Model.Entity.Element pipeEntityElement)
        {
            return modelData.PipeEntityElements.Where(x => pipeEntityElement.RevitElement.GetPipeIntersecElements().Contains(x));
        }
        public static IEnumerable<Model.Entity.Element> GetDuctIntersectEntityElements(this Model.Entity.Element ductEntityElement)
        {
            return modelData.DuctEntityElements.Where(x => ductEntityElement.RevitElement.GetDuctIntersecElements().Contains(x));
        }
        public static IEnumerable<Model.Entity.Element> GetCableTrayIntersectElements(this Model.Entity.Element cableTrayEntityElement)
        {
            return modelData.CableTrayEntityElements.Where(x => cableTrayEntityElement.RevitElement.GetCableTrayIntersecElements().Contains(x));
        }
    }
}
