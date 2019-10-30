using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchRename
{
    public class NewCaseAction : Action
    {
        public override string Classname => "New Case";

        public override string Description => throw new NotImplementedException();

        public override string Operate(string origin)
        {
            throw new NotImplementedException();
        }
    }
    public class NewCaseArgs : StringArgs
    {

    }
}
