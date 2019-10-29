using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchRename
{
    class UniqueNameAction : Action
    {
        string Action.Classname => "Unique Name";

        string Action.Description => throw new NotImplementedException();
    }
}
