using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChainOfResponsibility职责链模式
{
    class Program
    {
        static void Main(string[] args)
        {

            申请 request = new 申请();
            request.类型 = 申请类型.请假;
            request.数量 = 1;
            request.内容 = "小李要请假";

            总监 张三 = new 总监("张三");

            经理 李四 = new 经理("李四");
            张三.上级主管 = 李四;

            总经理 王五 = new 总经理("王五");
            李四.上级主管 = 王五;

            Console.WriteLine("\n第1次申请:{0}", request);
            张三.处理员工申请(request);
            request.数量 = 8;

            Console.WriteLine("\n第2次申请:{0}", request);
            张三.处理员工申请(request);

            request.类型 = 申请类型.加薪;
            request.内容 = "小李要涨工资";
            request.数量 = 800;

            Console.WriteLine("\n第3次申请:{0}", request);
            张三.处理员工申请(request);

            request.数量 = 1500;

            Console.WriteLine("\n第4次申请:{0}", request);
            张三.处理员工申请(request);

        }
    }
    public enum 申请类型
    {
        请假,
        加薪
    }
    public class 申请
    {
        public 申请类型 类型;
        public string 内容;
        public int 数量;
        public override string ToString()
        {
            return string.Format("内容:{0},类型:{1},数量:{2}", this.内容, this.类型, this.数量);
        }
    }

    public abstract class 管理者
    {
        protected string _name;
        public 管理者(string 姓名)
        {
            this._name = 姓名;
        }
        protected 管理者 _superior;
        public 管理者 上级主管
        {
            get { return this._superior; }
            set { this._superior = value; }
        }
        public abstract void 处理员工申请(申请 request);
    }
    public class 总监 : 管理者
    {
        public 总监(string 姓名) : base(姓名) { }
        public override void 处理员工申请(申请 request)
        {
            if (request.类型 == 申请类型.请假 && request.数量 <= 2)
            {
                Console.WriteLine("总监-{0}:通过{1}申请,数量{2}", base._name, request.内容, request.数量);
            }
            else
            {
                if (base._superior != null)
                {
                    Console.WriteLine("*总监-{0}:无权处理{1}申请,数量{2},转交给上级.", base._name, request.内容, request.数量);
                    base._superior.处理员工申请(request);
                }
            }
        }
    }
    public class 经理 : 管理者
    {
        public 经理(string 姓名) : base(姓名) { }
        public override void 处理员工申请(申请 request)
        {
            if (request.类型 == 申请类型.请假 && request.数量 <= 5)
            {
                Console.WriteLine("经理-{0}:通过{1}申请,数量{2}", base._name, request.内容, request.数量);
            }
            else
            {
                if (base._superior != null)
                {
                    Console.WriteLine("*经理-{0}:无权处理{1}申请,数量{2},转交给上级.", base._name, request.内容, request.数量);
                    base._superior.处理员工申请(request);
                }
            }
        }
    }
    public class 总经理 : 管理者
    {
        public 总经理(string 姓名) : base(姓名) { }
        public override void 处理员工申请(申请 request)
        {
            if (request.类型 == 申请类型.请假)
            {
                Console.WriteLine("总经理-{0}:通过{1}申请,数量{2}", base._name, request.内容, request.数量);
                return;
            }
            else
            {
                if (request.类型 == 申请类型.加薪 && request.数量 <= 1000)
                {
                    Console.WriteLine("总经理-{0}:通过{1}申请,数量{2}", base._name, request.内容, request.数量);
                    return;
                }
                else
                {
                    Console.WriteLine("总经理-{0}:没有通过{1}申请,数量{2}.再考察一下工作表现再说吧.", base._name, request.内容, request.数量);
                }
            }
        }
    }
}
/*
 * 
 */
