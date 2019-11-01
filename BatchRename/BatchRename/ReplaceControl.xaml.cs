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
            var from = TextBoxFrom.Text;
            var to = TextBoxTo.Text;
            if (from != "")
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
                    Global.action = new BindingList<Action>();
                }
                Global.action.Add(replace);
            }
            if(from == "")
            {
                MessageBox.Show("Ô From không được để trống");
            }
            else if(to == "/" || to == ":" || to == "*" || to == "?" || to == "<" || to == ">" || to == "|" || (int)to[0] == 34 || (int)to[0] == 92)
            {
                
                //to chứa kí tự không được đặt tên file: \/:*?"<>|
                MessageBox.Show($"Tên file không chứa kí tự {(char)92} / : * ? {(char)34} < > |");
            }
        }
    }
}
