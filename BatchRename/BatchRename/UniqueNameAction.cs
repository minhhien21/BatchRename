﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchRename
{
    public class UniqueNameAction : Action
    {
        public override string Classname => "Unique Name";

        public override string Description => throw new NotImplementedException();

        public override string Operate(string origin)
        {
            throw new NotImplementedException();
        }
    }
    public class UniqueNameArgs : StringArgs
    {

    }
}