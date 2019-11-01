using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BatchRename
{
    public abstract class Control: UserControl
    {
        public delegate void DimensionDelegate(BindingList<Action> action);
        public event DimensionDelegate DimensionChanged = null;
        public void FireDimensionChangedEvent(BindingList<Action> action)
        {
            DimensionChanged?.Invoke(Global.action);
        }
        
    }
}
