using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;
using SingleData;
using Autodesk.Revit.DB.Structure;

namespace Model.Entity
{
    public class Element
    {
        public Autodesk.Revit.DB.Element RevitElement { get; set; }
        private Geometry geometry;
        public Geometry Geometry
        {
            get
            {
                if (geometry == null)
                {
                    geometry = this.GetGeometry();
                }
                return geometry;
            }
        }
        private ElementType? elementType;
        public ElementType? ElementType
        {
            get
            {
                if(elementType == null)
                {
                    elementType = this.GetElementType();
                }
                return elementType;
            }
        }
    }
}
