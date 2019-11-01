using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Text.RegularExpressions;
using System.ComponentModel;

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

        public event PropertyChangedEventHandler PropertyChanged;
        public bool check = true;
        public override bool Check
        {
            get => check; set
            {
                check = value;
                Notify("Check");
            }
        }

        public void Notify(String Check)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(Check));
        }

        public override string GetStringName()
        {
            return "";
        }

        public override string Operate(string name , string extension, ref string Error)
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