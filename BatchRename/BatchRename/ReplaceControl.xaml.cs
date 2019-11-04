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
            int check = 1;
            var from = TextBoxFrom.Text;
            var to = TextBoxTo.Text;
            if(from == "")
            {
                check = 0;
                MessageBox.Show("Ô From không được để trống");
            }
            if(to !="")
            {
                int flag = 0;
                for (int i = 0; i < to.Length; i++) 
                {
                    if (to[i] == '/' || to[i] == ':' || to[i] == '*' || to[i] == '?' || to[i] == '<'
                        || to[i] == '>' || to[i] == '|' || (int)to[i] == 34 || (int)to[i] == 92)
                    {
                        flag = 1;
                        break;
                    }
                }
                if (flag == 1)
                {
                    //to chứa kí tự không được đặt tên file: \/:*?"<>|
                    check = 0;
                    MessageBox.Show($"Tên file không chứa kí tự {(char)92} / : * ? {(char)34} < > |");
                }
            }
            if(check == 1)
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
        }
    }
}
