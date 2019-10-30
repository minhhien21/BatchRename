using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchRename
{
    public class MoveArgs : StringArgs
    {

    }
    public class MoveAction : Action
    {
        public override string Classname => "Move";

        public override string Description => throw new NotImplementedException();

        public override Action Clone()
        {
            throw new NotImplementedException();
        }

        public override string Operate(string origin)
        {
            var args = Args as MoveArgs;
            var startAt = args.startAt;
            var length = args.length;
            var moveAt = args.moveAt;
            var ISBN = "";

            ISBN = origin.Substring(startAt, length);// lấy chuỗi ISBN tại vị trí startAt với độ dài Length

            origin = origin.Replace(ISBN, "").Trim();// xóa khoảng trắng thừa ở đầu hoặc cuối

            if (moveAt == "begin")
                return ISBN + " " + origin;
            else
                return origin + " " + ISBN;
        }
    }
    public class MoveArgs : StringArgs
    {
        public int startAt { get; set; }
        public int length { get; set; }
        public string moveAt { get; set; }

}
