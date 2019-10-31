using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BatchRename
{
    public abstract class Control: UserControl
    {
        //private List<string> arguments;

        public delegate void DimensionDelegate(List<string> arguments);
        public event DimensionDelegate DimensionChanged = null;
        public void FireDimensionChangedEvent(List<string> arguments)
        {
            DimensionChanged?.Invoke(Global.arguments);
        }
        
    }
}
