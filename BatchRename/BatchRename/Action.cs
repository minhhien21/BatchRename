using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchRename
{
    public abstract class Action
    {
        public abstract string Operate(string origin);
        public String Args { get; set; }

        public abstract String Classname { get; }

        public abstract String Description { get; }
    }
}
