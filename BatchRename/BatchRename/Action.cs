using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchRename
{
    public interface Action
    {
        String Classname { get; }
        String Extend { get; }
        String Description { get; }
    }
}
