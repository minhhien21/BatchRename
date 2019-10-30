using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchRename
{
    public class FullnameNormalizeAction : Action
    {
        public override string Classname => "Fullname Normalize";

        public override string Description => throw new NotImplementedException();

        public override string Operate(string origin)
        {
            throw new NotImplementedException();
        }
    }
    public class FullnameNormalizeArgs : StringArgs
    {

    }
}
