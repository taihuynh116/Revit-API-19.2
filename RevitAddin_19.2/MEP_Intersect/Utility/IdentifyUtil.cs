using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SingleData;

namespace Utility
{
    public static class IdentifyUtil
    {
        private static ModelData modelData
        {
            get { return ModelData.Instance; }
        }

        public static Model.Entity.Identify GetIdentify(this Model.Entity.Element element)
        {
            var identify = new Model.Entity.Identify();
            var revitElement = element.RevitElement;

            identify.Type = revitElement.GetTypeId().GetElement().Name;
            identify.Name = revitElement.LookupParameter("System Name").AsString();
            switch (element.ElementType)
            {
                case Model.Entity.ElementType.Duct:
                    identify.Level = revitElement.LookupParameter("Reference Level").AsElementId().GetElement().Name;
                    break;
                case Model.Entity.ElementType.Pipe:
                    identify.Level = revitElement.LookupParameter("Reference Level").AsElementId().GetElement().Name;
                    break;
                case Model.Entity.ElementType.CableTray:
                    identify.Level = revitElement.LookupParameter("Reference Level").AsElementId().GetElement().Name;
                    break;
                case Model.Entity.ElementType.Equipment:
                    identify.Level = revitElement.LevelId.GetElement().Name;
                    break;
                default:
                    break;
            }
            return identify;
        }
    }
}
