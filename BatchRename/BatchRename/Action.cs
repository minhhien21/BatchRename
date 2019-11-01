using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchRename
{
    public abstract class Action
    {

        public abstract string Operate(string Name, string extension);

        public abstract string GetStringName();

        public StringArgs Args { get; set; }

        public abstract string Classname { get; }

        public abstract string Description { get; }

        public abstract bool Check { get; set; }

        public abstract Action Clone();
    }
}
