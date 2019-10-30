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
        public delegate void DimensionDelegate(Action newaction);
        public event DimensionDelegate DimensionChanged = null;
        ReplaceArgs myArgs;
        public ReplaceAction replace;

        public ReplaceControl()
        {
            InitializeComponent();
        }

        private void Add_to_list(object sender, RoutedEventArgs e)
        {
            var x = TextBoxFrom.Text;
            replace = new ReplaceAction()
            {
                Args = new ReplaceArgs()
                {
                    From = TextBoxFrom.Text,
                    To = TextBoxTo.Text
                }
            };
            //replace.Clone();            
            DimensionChanged?.Invoke(replace);
        }
    }
}
