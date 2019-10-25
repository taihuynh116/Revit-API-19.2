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


        private List<Element> instersectElements;
        public List<Element> InstersectElements
        {
            get
            {
                //if (instersectElements == null) instersectElements = this.GetIntersectEntityElements();
                return instersectElements;
            }
        }
    }
}
