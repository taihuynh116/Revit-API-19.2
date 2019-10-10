using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SingleData;

namespace Utility
{
    public static class ElementUtil
    {
        public static Model.Entity.Element GetEntityElement(this Autodesk.Revit.DB.Element elem)
        {
            return new Model.Entity.Element()
                {
                    RevitElement = elem
                };
        }

        public static Model.Entity.ElementType GetElementType(this Model.Entity.Element ettElem)
        {
            var revitElem = ettElem.RevitElement;
            var cate = revitElem.Category;
            if (revitElem is Autodesk.Revit.DB.Floor)
                return Model.Entity.ElementType.Floor;
            if (revitElem is Autodesk.Revit.DB.Wall)
                return Model.Entity.ElementType.Wall;
            if (cate.Id.IntegerValue == (int)Autodesk.Revit.DB.BuiltInCategory.OST_StructuralFraming)
                return Model.Entity.ElementType.Framing;
            if (cate.Id.IntegerValue == (int)Autodesk.Revit.DB.BuiltInCategory.OST_StructuralColumns)
                return Model.Entity.ElementType.Column;
            return Model.Entity.ElementType.Undefined;
        }
        
    }
}
