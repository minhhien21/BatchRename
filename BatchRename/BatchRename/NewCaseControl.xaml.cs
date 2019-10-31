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
    /// Interaction logic for NewCaseControl.xaml
    /// </summary>
    public partial class NewCaseControl : Control
    {
        public NewCaseAction newCase;
        public NewCaseControl()
        {
            InitializeComponent();
        }

        private void Add_to_list(object sender, RoutedEventArgs e)
        {
            ComboBoxItem typeItem = (ComboBoxItem)newCaseCombobox.SelectedItem;
            newCase = new NewCaseAction()
            {
                Args = new NewCaseArgs()
                {
                    type = typeItem.Content.ToString()
                }
            };
            if (Global.action == null)
            {
                Global.action = new BindingList<Action>();
            }
            Global.action.Add(newCase);

            FireDimensionChangedEvent(Global.action);
        }
    }
}
