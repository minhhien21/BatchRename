using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Text.RegularExpressions;
namespace BatchRename
{
    public class FullnameNormalizeArgs : StringArgs
    {
        public string option { get; set; }
    }
    public class FullnameNormalizeAction : Action
    {
        public override string Classname => "Fullname Normalize";

        public override string Description => getDescription();

        public string getDescription()
        {
            var args = Args as FullnameNormalizeArgs;
            return $"{args.option}";
        }

        public override Action Clone()
        {
            throw new NotImplementedException();
        }

        public override string GetStringName()
        {
            return "";
        }

        public override string Operate(string name , string extension)
        {
            var args = Args as FullnameNormalizeArgs;
            var choosenOption = args.option;
            var result = name;
            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;

            if (choosenOption == "No space at two endings")
                return result.Trim();
            else if (choosenOption == "Capitalize first letter")
            {
                result = textInfo.ToTitleCase(name);
                return result;
            }
            else
            {
                result = Regex.Replace(name, @"\s+", " ");
                return result;
            }
        }
    }
}