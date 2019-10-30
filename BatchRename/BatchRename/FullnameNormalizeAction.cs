using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace BatchRename
{
    public class FullnameNormalizeArgs : StringArgs
    {
        public string option { get; set; }
    }
    public class FullnameNormalizeAction : Action
    {
        public override string Classname => "Fullname Normalize";

        public override string Description => throw new NotImplementedException();

        public override Action Clone()
        {
            throw new NotImplementedException();
        }

        public override string Operate(string origin)
        {
            var args = Args as FullnameNormalizeArgs;
            var choosenOption = args.option;
            var result = origin;
            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;

            if (choosenOption == "No space at two endings")
                return result.Trim();
            else if (choosenOption == "Capitalize first letter")
            {
                result = textInfo.ToTitleCase(origin);
                return result;
            }
            else
                return result;
        }
    }

}
