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
    /// Interaction logic for MoveControl.xaml
    /// </summary>
    public partial class MoveControl : Control
    {
        public MoveAction move;
        public MoveControl()
        {
            InitializeComponent();
        }

        private void Add_to_list(object sender, RoutedEventArgs e)
        {

            if(moveCombobox.SelectedIndex > -1)
            {
                ComboBoxItem typeItem = (ComboBoxItem)moveCombobox.SelectedItem;
                move = new MoveAction()
                {
                    Args = new MoveArgs()
                    {
                        startAt = int.Parse(txtStartAt.Text),
                        length = int.Parse(txtLength.Text),
                        moveAt = typeItem.Content.ToString()
                    }
                };
                if (Global.action == null)
                {
                    Global.action = new List<Action>();
                    Global.addlist = new BindingList<Action>();
                }
                Global.action.Add(move);
                Global.addlist.Add(move);
            }
            else
            {
                MessageBox.Show("Ban chua setting");
            }
        }
    }
}
