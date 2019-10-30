using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchRename
{
    public class MoveArgs : StringArgs
    {
        public int startAt { get; set; }
        public int length { get; set; }
        public string moveAt { get; set; }
    }

    public class MoveAction : Action
    {
        public override string Classname => "Move";

        public override string Description => throw new NotImplementedException();

        public override Action Clone()
        {
            throw new NotImplementedException();
        }

        public override string Operate(ref string name, ref string extension)
        {
            var args = Args as MoveArgs;
            var startAt = args.startAt;
            var length = args.length;
            var moveAt = args.moveAt;
            var ISBN = "";

            ISBN = name.Substring(startAt, length);// lấy chuỗi ISBN tại vị trí startAt với độ dài Length

            name = name.Replace(ISBN, "").Trim();// xóa khoảng trắng thừa ở đầu hoặc cuối

            if (moveAt == "Begin")
                return ISBN + " " + name;
            else
                return name + " " + ISBN;
        }
    }


}
