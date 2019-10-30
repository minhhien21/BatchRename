using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchRename
{
    public class NewCaseArgs : StringArgs
    {
        public string type { get; set; }
    }
    public class NewCaseAction : Action
    {
        public override string Classname => "New Case";

        public override string Description => throw new NotImplementedException();

        public override Action Clone()
        {
            throw new NotImplementedException();
        }

        public override string Operate(ref string name, ref string extension)
        {
            var args = Args as NewCaseArgs;
            var type = args.type;
            
            // Creates a TextInfo based on the "en-US" culture.
            TextInfo myTI = new CultureInfo("en-US", false).TextInfo;

            if (type == "UpperCase")
            {
                name = myTI.ToUpper(name);
                return name;
            }
            else if (type == "LowerCase")
            {
                name = myTI.ToLower(name);
                return name;
            }
            else
            {
                name = myTI.ToTitleCase(name);
                return name;
            }
        }
    }
}
