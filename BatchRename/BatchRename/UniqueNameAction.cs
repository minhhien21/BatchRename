﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchRename
{
    public class UniqueNameArgs : StringArgs
    {

    }
    public class UniqueNameAction : Action
    {
        public override string Classname => "Unique Name";

        public override string Description => "Unique Name";

        public override Action Clone()
        {
            throw new NotImplementedException();
        }

        public override string GetStringName()
        {
            return "";
        }

        public override string Operate(string name, string extension)
        {
            throw new NotImplementedException();
        }
    }

}
