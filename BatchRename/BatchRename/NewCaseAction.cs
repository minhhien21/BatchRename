using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchRename
{
    class NewCaseAction : Action
    {
        string Action.Classname => "New Case";

        string Action.Description => throw new NotImplementedException();

        string Action.Extend => "+";
    }
}
