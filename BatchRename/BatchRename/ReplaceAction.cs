using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchRename
{
    class ReplaceAction : Action
    {
        string Action.Classname => "Replace";

        string Action.Description => throw new NotImplementedException();

        string Action.Extend => "+";
    }
}
