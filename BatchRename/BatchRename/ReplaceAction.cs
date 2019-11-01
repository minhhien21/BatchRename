using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        public override string Description => getDesciption();

        public string getDesciption()
        {
            var args = Args as ReplaceArgs;
            return $"Replace '{args.From}' to '{args.To}' in '{args.StringChange}'";
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

        public string StringChange { get; set; }

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

        public override string GetStringName()
        {
            return StringChange;
        }

        public override string Operate(string name, string extension, ref string Error)
        {
            var args = Args as ReplaceArgs;
            var from = args.From;
            var to = args.To;
            var stringchange = args.StringChange;
            bool flag = true;
            if (stringchange == "Name")
            {
                this.StringChange = "Name";
                //không chứa from trong name
                if (!name.Contains(from))
                    flag = false;

                //to chứa kí tự không được đặt tên file: \/:*?"<>|
                if (to.Contains(@"\") || to.Contains("/") || to.Contains(":") || to.Contains("*") || to.Contains("?") || to.Contains('"') || to.Contains("<")
                    || to.Contains(">") || to.Contains("|"))
                {
                    flag = false;
                }

                if (!flag)
                {
                    Error += this.Description + "\n";
                    return name;
                }

                return name.Replace(from, to);
            }
            else
            {
                this.StringChange = "Extension";
                //không chứa from trong extension
                if (!extension.Contains(from))
                    flag = false;
                
                if(!flag)
                {
                    Error += this.Description + "\n";
                    return extension;
                }
                return extension.Replace(from, to);
            }
        }
    }
}
