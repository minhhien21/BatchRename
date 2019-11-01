using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchRename
{
    public class UniqueNameArgs : StringArgs
    {
        public string option { get; set; }

    }
    public class UniqueNameAction : Action
    {
        public override string Classname => "Unique Name";

        public override string Description => "Unique Name";

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

        public override string Operate(string name, string extension, ref string Error)
        {
            Guid id = Guid.NewGuid();
            name = id.ToString();
            return name;
        }
    }

}
