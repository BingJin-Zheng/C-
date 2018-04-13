using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Command命令模式
{
    class Program
    {
        static void Main(string[] args)
        {
            Command.doJob();
            服务员 mm = new 服务员();
            厨师 cook = new 厨师();

            //30个鸡翅
            命令 cmd30个鸡翅 = new 烤鸡翅命令(cook);
            cmd30个鸡翅.数量 = 30;

            //40个羊肉串 
            命令 cmd40个羊肉串 = new 烤羊肉串命令(cook);
            cmd40个羊肉串.数量 = 40;

            Console.WriteLine("========先来30个鸡翅,40个羊肉串=========");

            mm.下单(cmd30个鸡翅);
            mm.下单(cmd40个羊肉串);

            mm.全部执行();

            Console.WriteLine("========实在太爽了,再来40个鸡翅,50个羊肉串=========");

            //40个鸡翅
            命令 cmd40个鸡翅 = new 烤鸡翅命令(cook);
            cmd40个鸡翅.数量 = 40;


            //50个羊肉串 
            命令 cmd50个羊肉串 = new 烤羊肉串命令(cook);
            cmd50个羊肉串.数量 = 50;

            mm.下单(cmd40个鸡翅);
            mm.下单(cmd50个羊肉串);
            mm.全部执行();

            Console.WriteLine("========不爽了,取消30个鸡翅,40个羊肉串=========");
            mm.取消订单(cmd30个鸡翅);
            mm.取消订单(cmd40个羊肉串);

            Console.ReadLine();
        }
    }

    /// <summary>
    /// //Command 命令抽象类
    /// </summary>
    public abstract class 命令
    {
        protected 厨师 _cook; //receiver接受者
        public 命令(厨师 cook)
        {
            this._cook = cook;
        }
        //Execute执行
        public abstract void 烧烤();
        //State状态
        public bool 执行完毕 = false;

        private int _数量;
        public int 数量
        {
            get
            {
                return this._数量;
            }
            set
            {
                if (value < 0)
                {
                    Console.WriteLine("数量应>0");
                }
                else
                {
                    this._数量 = value;
                }
            }
        }
    }

    /// <summary>
    /// 命令调用者Invoker
    /// </summary>
    public class 服务员
    {
        private int 鸡翅数量 = 60;
        private int 羊肉串数量 = 80;
        private System.Collections.Generic.List<命令> orders = new List<命令>();
        public void 下单(命令 command)
        {
            if (command is 烤鸡翅命令)
            {
                if (this.鸡翅数量 < command.数量)
                {
                    Console.WriteLine("对不起,鸡翅不够了,现在只剩下:{0}", this.鸡翅数量);
                    return;
                }
                else
                {
                    this.鸡翅数量 -= command.数量;
                }
            }
            if (command is 烤羊肉串命令)
            {
                if (this.羊肉串数量 < command.数量)
                {
                    Console.WriteLine("对不起,羊肉串不够了,现在只剩下:{0}", this.鸡翅数量);
                    return;
                }
                else
                {
                    this.羊肉串数量 -= command.数量;
                }
            }
            orders.Add(command);
            Console.WriteLine("新下单:{0},数量:{1}\t{2}", command, command.数量, DateTime.Now);

        }
        public void 取消订单(命令 command)
        {
            if (command.执行完毕)
            {
                Console.WriteLine("订单已执行完毕,东西都吃到肚子里了.不能再取消");
                return;
            }
            orders.Remove(command);
            if (command is 烤鸡翅命令)
            {
                this.鸡翅数量 += command.数量;
            }
            else
            {
                this.羊肉串数量 += command.数量;
            }
            Console.WriteLine("订单已取消:" + command.ToString() + "\t" + DateTime.Now.ToString());
        }
        public void 全部执行()
        {
            foreach (命令 cmd in orders)
            {
                if (!cmd.执行完毕)
                {
                    cmd.烧烤();
                }
            }
        }
    }

    /// <summary>
    /// //ConcreteCommand具体命令 ,需要被命令调用者调用的命令
    /// </summary>
    public class 烤鸡翅命令 : 命令
    {
        public 烤鸡翅命令(厨师 cook) : base(cook) { }
        public override void 烧烤()
        {
            base._cook.烤鸡翅(base.数量);
            this.执行完毕 = true;
        }
    }
    /// <summary>
    /// //ConcreteCommand具体命令 ,需要被命令调用者调用的命令
    /// </summary>
    public class 烤羊肉串命令 : 命令
    {
        public 烤羊肉串命令(厨师 cook) : base(cook) { }
        public override void 烧烤()
        {
            base._cook.烤羊肉串(base.数量);
            this.执行完毕 = true;
        }
    }

    /// <summary>
    /// //Receiver 具体干活的人,不会被直接调用,被命令调用
    /// </summary>
    public class 厨师
    {
        //Action
        public void 烤鸡翅(int 数量)
        {
            Console.WriteLine("厨师烤鸡翅:{0}", 数量);
        }
        //Action
        public void 烤羊肉串(int 数量)
        {
            Console.WriteLine("厨师烤羊肉串:{0}", 数量);
        }
    }
}

/*
 *  在软件系统中，“行为请求者”与“行为实现者”通常呈现一种“紧耦合”。但在某些场合，比如要对行为进行“记录、撤销/重做、事务”等处理，这种无法抵御变化的紧耦合是不合适的。
    在这种情况下，如何将“行为请求者”与“行为实现者”解耦？将一组行为抽象为对象，可以实现二者之间的松耦合。
   将一个请求封装为一个对象，从而使你可用不同的请求对客户进行参数化；对请求排队或记录请求日志，以及支持可撤消的操作。
 */
