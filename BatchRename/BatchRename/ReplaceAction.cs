using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchRename
{
    public class ReplaceArgs : StringArgs
    {
        public string From { get; set; }
        public string To { get; set; }
        public string StringChange { get; set; }
    }

    public class ReplaceAction : Action
    {
        public override string Classname => "Replace";

        public override string Description => throw new NotImplementedException();

        public override Action Clone()
        {
            var oldArgs = Args as ReplaceArgs;
            return new ReplaceAction
            {
                Args = new ReplaceArgs()
                {
                    From = oldArgs.From,
                    To = oldArgs.To,
                    StringChange = oldArgs.StringChange
                }
            };
        }

        public override string Operate(ref string name, ref string extension)
        {
            var args = Args as ReplaceArgs;
            var from = args.From;
            var to = args.To;
            var stringchange = args.StringChange;
            if (stringchange == "Name")
            {
                name = name.Replace(from, to);
                return name;
            }
            else
            {
                extension = extension.Replace(from, to);
                return extension;
            }
        }
    }
}
