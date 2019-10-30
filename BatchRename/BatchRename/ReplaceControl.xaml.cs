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
    /// Interaction logic for ReplaceControl.xaml
    /// </summary>
    public partial class ReplaceControl : Control
    {
        public ReplaceAction replace;
        public ReplaceControl()
        {
            InitializeComponent();
        }

        private void Add_to_list(object sender, RoutedEventArgs e)
        {
            if (CbbApplyTo.SelectedIndex > -1)
            {
                ComboBoxItem typeItem = (ComboBoxItem)CbbApplyTo.SelectedItem;
                replace = new ReplaceAction()
                {
                    Args = new ReplaceArgs()
                    {
                        From = TextBoxFrom.Text,
                        To = TextBoxTo.Text,
                        StringChange = typeItem.Content.ToString()
                    }
                };
                if (Global.action == null)
                {
                    Global.action = new List<Action>();
                }
                Global.action.Add(replace);
            }
            else
            {
                MessageBox.Show("Ban chua setting");
            }
        }
    }
}
