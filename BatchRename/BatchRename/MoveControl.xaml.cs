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
            var n = 0;
            var m = 0;
            bool flag1 = true;
            bool flag2 = true;
            if (!int.TryParse(txtStartAt.Text, out n))
            {
                flag1 = false;
                MessageBox.Show("gia tri start at khong la so nguyen!");
            }
            if (!int.TryParse(txtLength.Text, out m))
            {
                flag2 = false;
                MessageBox.Show("Gia tri Length khong la so nguyen!");
            }
            if(flag1 && int.Parse(txtStartAt.Text) < 0)
            {
                flag1 = false;
                MessageBox.Show("Gia tri start at phai duong!");
            }
            if (flag2 && int.Parse(txtLength.Text) < 0)
            {
                flag2 = false;
                MessageBox.Show("Gia tri length phai duong!");
            }
            if (flag1 && flag2)
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
                    Global.action = new BindingList<Action>();
                }
                Global.action.Add(move);
            }
        }
    }
}
