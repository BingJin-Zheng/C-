using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Command命令模式
{
    //委托只对方法负责,命令对类负责
    public abstract class Command
    {
        /// <summary>
        /// 命令的接收者,干活的人
        /// </summary>
        protected WebService _webService = new WebService();
        /// <summary>
        /// 执行命令
        /// </summary>
        public abstract void Execute();

        public abstract Guid CommandID { get; }

        public static void doJob()
        {
            Invoker ik = new Invoker();
            Command cmd1 = new OrderCommand() { total = 500, Cumstomer = "ppp" };
            Command cmd2 = new OrderCommand() { total = 400, Cumstomer = "ccc" };
            Command cmd3 = new OrderCommand() { total = 300, Cumstomer = "vvv" };

            ik.AddCmd(cmd1);
            ik.AddCmd(cmd2);
            ik.AddCmd(cmd3);
            ik.RemoveCmd(cmd3);
            NetWork.OnLine = false;
            ik.ExceuteAllCommand();
            NetWork.OnLine = true;
            ik.ExceuteAllCommand();
        }
    }
    /// <summary>
    /// 具体的命令
    /// </summary>
    public class OrderCommand : Command
    {
        private Guid id = Guid.Empty;
        public OrderCommand()
        {
            this.id = Guid.NewGuid();
        }
        public override Guid CommandID
        {
            get
            {
                return this.id;
            }
        }
        public int total;
        public string Cumstomer;
        public string orderNumber
        {
            get
            {
                return Guid.NewGuid().ToString();
            }
        }
        public override void Execute()
        {
            this._webService.NewOrder(this.orderNumber, this.total, this.Cumstomer);
        }

        public override string ToString()
        {
            return string.Format("订单{0},客户{1},金额{2}",this.id,this.Cumstomer,this.total);
        }
    }
    /// <summary>
    /// 命令的管理以及调用者
    /// </summary>
    public class Invoker
    {
        private Dictionary<Guid, Command> cmdDict = new Dictionary<Guid, Command>();

        public void AddCmd(Command cmd)
        {
            if (!this.cmdDict.ContainsKey(cmd.CommandID))
            {
                this.cmdDict.Add(cmd.CommandID, cmd);
            }
            else
            {
                Console.WriteLine("不重复添加命令");
            }
        }

        public void RemoveCmd(Command cmd)
        {
            if (this.cmdDict.ContainsKey(cmd.CommandID))
            {
                this.cmdDict.Remove(cmd.CommandID);
                Console.WriteLine("命令已删除:{0}",cmd);
            }
            else
            {
                Console.WriteLine("没有这个命令");
            }
        }

        public void ExceuteAllCommand()
        {
            if (NetWork.OnLine)
            {
                foreach (var item in this.cmdDict)
                {
                    item.Value.Execute();
                    Console.WriteLine("{0}执行完毕",item.Value);
                }
            }
            else
            {
                Console.WriteLine("不在线,无法执行,命令");
            }
        }

    }

    /// <summary>
    /// 网络
    /// </summary>
    public static class NetWork
    {
        /// <summary>
        /// 网络是否通畅
        /// </summary>
        public static bool OnLine = true;
    }
    /// <summary>
    /// 命令真正的执行者,receiver
    /// </summary>
    public class WebService
    {
        /// <summary>
        /// action 真正干活的方法
        /// </summary>
        /// <param name="OrderNumber"></param>
        /// <param name="total"></param>
        /// <param name="customer"></param>
        public void NewOrder(string OrderNumber,int total,string customer)
        {
            Console.WriteLine("{0}在{1}下订单,订单金额为{2},订单编号为{3}",customer,DateTime.Now,total,OrderNumber);
        }


    }

    
}
