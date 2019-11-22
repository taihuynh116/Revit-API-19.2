using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SingleData;
using Utility;

namespace Model.Form
{
    /// <summary>
    /// Interaction logic for InputData.xaml
    /// </summary>
    public partial class InputForm : Window
    {
        public InputForm()
        {
            InitializeComponent();
        }
        private void Button_PickElement(object sender, RoutedEventArgs e)
        {
            InputFormUtil.PickElement();
        }

        private void Button_ShowClash(object sender, RoutedEventArgs e)
        {
            InputFormUtil.ShowClash();
        }
    }
}
