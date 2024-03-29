﻿using System;
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
        private Action action { get; }
        public Control control { get; }
        public string ClassName => action.Classname;
        public string expand { get; set; }
        public int count { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        public ActionMain(Action action, Control control)
        {
            this.action = action;
            this.control = control;
            this.control.Visibility = Visibility.Visible;
            expand = "+";
            count = 0;
        }
        public string Expand { get => expand; set {
                expand = value;
                Notify("Expand");
            } }

        public void Notify(string Expand)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(Expand));
        }
        

        public void ShowControl()
        {
            count++;
            if (count % 2 == 1)
            {
                Expand = "-";
                control.Visibility = Visibility.Visible;
            }
            else
            {
                Expand = "+";
                control.Visibility = Visibility.Hidden;
            }
        }
    }
}
