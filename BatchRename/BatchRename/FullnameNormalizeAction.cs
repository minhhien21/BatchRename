using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchRename
{
    class FullnameNormalizeAction : Action
    {
        string Action.Classname => "Fullname Normalize";

        string Action.Description => throw new NotImplementedException();

        string Action.Extend => "+";
    }
}
