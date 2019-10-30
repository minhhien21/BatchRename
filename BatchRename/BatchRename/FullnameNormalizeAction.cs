using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchRename
{
    public class FullnameNormalizeArgs : StringArgs
    {

    }
    public class FullnameNormalizeAction : Action
    {
        public override string Classname => "Fullname Normalize";

        public override string Description => throw new NotImplementedException();

        public override Action Clone()
        {
            throw new NotImplementedException();
        }

        public override string Operate(string origin)
        {
            throw new NotImplementedException();
        }
    }

}
