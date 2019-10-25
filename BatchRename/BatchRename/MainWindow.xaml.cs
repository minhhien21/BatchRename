using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
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

namespace BatchRename
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        BindingList<ButtonItem> _buttonitem = null;
        public class ButtonItem
        {
            public string ButtonName { get; set; }

            public string ClickButtonName { get; set; }

            public string CheckName { get; set; }

        }

        class ButtonItemDao
        {
            /// <summary>
            /// Lấy danh sách laptop từ cơ sở dữ liệu
            /// </summary>
            /// <returns></returns>
            public static BindingList<ButtonItem> GetAll()
            {
                BindingList<ButtonItem> result = null;
                var lines = File.ReadAllLines("ButtonName.txt");
                var count = int.Parse(lines[0]);
                if (count > 0)
                {
                    result = new BindingList<ButtonItem>();
                    const string Separator = " / ";
                    for (int i = 0; i < count; i++)
                    {
                        var tokens = lines[i + 1].Split(new string[]
                            { Separator }, StringSplitOptions.RemoveEmptyEntries);
                        var buttonitem = new ButtonItem()
                        {
                            ButtonName = tokens[0],
                            ClickButtonName = tokens[1],
                            CheckName = tokens[2]
                        };
                        result.Add(buttonitem);
                    }
                }
                return result;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _buttonitem = ButtonItemDao.GetAll();
            ButtonNames.ItemsSource = _buttonitem;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            TextBox.Text = "0";
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            TextBox.Text = "1";
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            TextBox.Text = "2";
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            TextBox.Text = "3";
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            TextBox.Text = "4";
        }

        private void Button_up(object sender, RoutedEventArgs e)
        {
            var x = _buttonitem[1];
            _buttonitem[1] = _buttonitem[0];
            _buttonitem[0] = x;
        }

        private void Button_up1(object sender, RoutedEventArgs e)
        {

        }
    }
}
