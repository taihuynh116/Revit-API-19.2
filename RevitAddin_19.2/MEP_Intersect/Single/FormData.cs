using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace SingleData
{
    public class FormData : NotifyClass
    {
        private static FormData instance;
        public static FormData Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = new FormData();
                }
                return instance;
            }
            set
            {
                instance = value;
            }
        }

        private Model.Form.InputForm inputForm;
        public Model.Form.InputForm InputForm
        {
            get
            {
                if (inputForm == null)
                {
                    inputForm = new Model.Form.InputForm { DataContext = this };
                    // Get data => Form
                }
                return inputForm;
            }
        }

        private Model.ViewModel.ElementView elementView;
        public Model.ViewModel.ElementView ElementView
        {
            get { return elementView; }
            set { elementView = value; OnPropertyChanged(); }
        }

        private ObservableCollection<Model.ViewModel.ElementView> elementViews;
        public ObservableCollection<Model.ViewModel.ElementView> ElementViews
        {
            get { return elementViews; }
            set { elementViews = value; OnPropertyChanged(); }
        }

    }
}
