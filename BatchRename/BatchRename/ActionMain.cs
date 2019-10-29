using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BatchRename
{
    class ActionMain : INotifyPropertyChanged
    {
        private Action action;
        public Control control;

        public event PropertyChangedEventHandler PropertyChanged;

        public string ClassName => action.Classname;
        public string Extend => action.Extend;


        public ActionMain(Action action, Control control)
        {
            this.action = action;
            this.control= control;
            this.control.Visibility = Visibility.Hidden;
            //Extend = "+";
        }

        public int ShowControl()
        {
            control.Visibility = Visibility.Visible;
            return 1;
        }
    }
}
