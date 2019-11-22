using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public static class ElementViewUtil
    {
        public static Model.ViewModel.ElementView GetElementView(this Model.Entity.Element elem)
        {
            return new Model.ViewModel.ElementView
            {
                Element = elem
            };
        }
    }
}
