using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchRename
{
    public class NewCaseAction : Action
    {
        public override string Classname => "New Case";

        public override string Description => throw new NotImplementedException();

        public override string Operate(string origin)
        {
            var args = Args as NewCaseArgs;
            var type = args.type;

            // Creates a TextInfo based on the "en-US" culture.
            TextInfo myTI = new CultureInfo("en-US", false).TextInfo;

            if (type == 0)
                return myTI.ToUpper(origin);
            else if (type == 1)
                return myTI.ToLower(origin);
            else
                return myTI.ToTitleCase(origin);
        }
    }

    public class NewCaseArgs : StringArgs
    {
        public int type { get; set; }
    }
}
