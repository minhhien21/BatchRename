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
    /// Interaction logic for UniqueNameControl.xaml
    /// </summary>
    public partial class UniqueNameControl : Control
    {
        public UniqueNameAction changetoUniqueName;
        public UniqueNameControl()
        {
            InitializeComponent();
        }

        private void Add_to_list(object sender, RoutedEventArgs e)
        {
            ComboBoxItem selectedItem = (ComboBoxItem)uniquenameCombobox.SelectedItem;
            changetoUniqueName = new UniqueNameAction()
            {
                Args = new UniqueNameArgs()
                {
                    option = selectedItem.Content.ToString()
                }
            };

            if (Global.action == null)
            {
                Global.action = new List<Action>();

            }
            Global.action.Add(changetoUniqueName);


        }
    }
}
