﻿using System;
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

        public override string Description => getDescription();

        public override bool Check => true;

        public string getDescription()
        {
            var args = Args as NewCaseArgs;
            return $"Make string {args.type}";
        }

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
            var args = Args as NewCaseArgs;
            var type = args.type;
            
            // Creates a TextInfo based on the "en-US" culture.
            TextInfo myTI = new CultureInfo("en-US", false).TextInfo;

            if (type == "UpperCase")
                return myTI.ToUpper(name);
            else if (type == "LowerCase")
                return myTI.ToLower(name);
            else
                return myTI.ToTitleCase(name);
        }
    }
}
