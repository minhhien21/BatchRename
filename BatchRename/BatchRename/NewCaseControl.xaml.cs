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
using System.Windows.Shapes;

namespace BatchRename
{
    /// <summary>
    /// Interaction logic for NewCaseControl.xaml
    /// </summary>
    public partial class NewCaseControl : Control
    {
        //NewCaseArgs myArgs;
        public NewCaseControl()
        {
            InitializeComponent();
            //myArgs.type = 2; //default: FirstUppercase
        }

        private void Add_to_list(object sender, RoutedEventArgs e)
        {
            //var item = newCaseCombobox.SelectedItem as string;
            //if (item == "UpperCase")
            //    myArgs.type = 0;
            //else if (item == "LowerCase")
            //    myArgs.type = 1;
            //else
            //    myArgs.type = 2;
            
            
            
        }
    }
}
