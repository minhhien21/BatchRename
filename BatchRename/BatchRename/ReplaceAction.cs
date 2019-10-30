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
                    To = oldArgs.To
                }
            };
        }

        public override string Operate(string origin)
        {
            var args = Args as ReplaceArgs;
            var from = args.From;
            var to = args.To;
            return origin.Replace(from, to);
        }
    }
}
