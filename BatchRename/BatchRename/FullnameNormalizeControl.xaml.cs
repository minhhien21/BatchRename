using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for FullnameNormalizeControl.xaml
    /// </summary>
    public partial class FullnameNormalizeControl : Control
    {
        public FullnameNormalizeAction normalizeFullFileName;
        public FullnameNormalizeControl()
        {
            InitializeComponent();
        }

        private void Add_to_list(object sender, RoutedEventArgs e)
        {
            ComboBoxItem selectedItem = (ComboBoxItem)normalizefullnameCombobox.SelectedItem;
            normalizeFullFileName = new FullnameNormalizeAction()
            {
                Args = new FullnameNormalizeArgs()
                {
                    option = selectedItem.Content.ToString()
                }

            };
            if (Global.action == null)
            {
                Global.action = new BindingList<Action>();

            }
            Global.action.Add(normalizeFullFileName);

        }
    }
}
