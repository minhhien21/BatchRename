using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchRename
{
    class MoveAction : Action
    {
        string Action.Classname => "Move";

        string Action.Description => throw new NotImplementedException();

        string Action.Extend => "+";
    }
}
