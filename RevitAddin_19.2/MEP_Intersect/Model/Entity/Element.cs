using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace Model.Entity
{
    public class Element
    {
        public Autodesk.Revit.DB.Element RevitElement { get; set; }
        //public Autodesk.Revit.DB.MEPCurve RevitMEPCurve { get; set; }

        private ElementType? elementType;
        public ElementType? ElementType
        {
            get
            {
                if (elementType == null)
                    elementType = this.GetElementType();
                return elementType;
            }
        }

        private Identify identify;
        public Identify Identify
        {
            get
            {
                if (identify == null)
                    identify = this.GetIdentify();
                return identify;
            }
        }

        private IEnumerable<Element> instersectElements;
        public IEnumerable<Element> InstersectElements
        {
            get
            {
                if (instersectElements == null) instersectElements = this.GetIntersectEntityElements();
                return instersectElements;
            }
        }

        private IEnumerable<Autodesk.Revit.DB.MEPCurve> pipeInstersectElements;
        public IEnumerable<Autodesk.Revit.DB.MEPCurve> PipeInstersectElements
        {
            get
            {
                if (pipeInstersectElements == null) pipeInstersectElements = this.GetPipeIntersectEntityElements().Cast<Autodesk.Revit.DB.MEPCurve>();
                return pipeInstersectElements;
            }
        }

        private IEnumerable<Autodesk.Revit.DB.MEPCurve> ductIntersectElements;
        public IEnumerable<Autodesk.Revit.DB.MEPCurve> DuctIntersectElements
        {
            get
            {
                if (ductIntersectElements == null) ductIntersectElements = this.GetDuctIntersectEntityElements().Cast<Autodesk.Revit.DB.MEPCurve>();
                return ductIntersectElements;
            }
        }

        private IEnumerable<Autodesk.Revit.DB.Electrical.CableTray> cableTrayIntersectElements;
        public IEnumerable<Autodesk.Revit.DB.Electrical.CableTray> CableTrayIntersectElements
        {
            get
            {
                if (cableTrayIntersectElements == null) cableTrayIntersectElements = this.GetCableTrayIntersectElements().Cast<Autodesk.Revit.DB.Electrical.CableTray>();
                return cableTrayIntersectElements;
            }
        }
    }
}
