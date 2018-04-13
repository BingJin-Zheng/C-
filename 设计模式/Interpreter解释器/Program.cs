using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter解释器
{
    class Program
    {
        static void Main(string[] args)
        {
            机器人 robot = new 机器人();

            指令 command = new 指令();
            command.内容 = "SLRBFLLRRBBFFE";

            robot.执行命令(command);

            Console.ReadLine();
        }
    }
    public class 机器人
    {

        public void 执行命令(指令 context)
        {
            表达式 expression = null;

            while (context.内容.Length > 0)
            {
                string 当前命令 = context.内容.Substring(0, 1);
                if (当前命令 == "S" || 当前命令 == "E")
                {
                    expression = new 状态表达式();
                }
                else
                {
                    expression = new 动作表达式();
                }
                expression.解释(context);
            }
        }
    }
    public class 指令//Context
    {
        private string _txt = "";
        public string 内容
        {
            get { return this._txt; }
            set { this._txt = value; }
        }
    }

    public abstract class 表达式
    {
        public abstract void 解释(指令 context);
    }
    public class 动作表达式 : 表达式
    {
        public override void 解释(指令 context)
        {
            string key = context.内容.Substring(0, 1);
            string result = "";
            switch (key)
            {
                case "F":
                    result = "前进";
                    break;
                case "B":
                    result = "后退";
                    break;
                case "L":
                    result = "左转弯";
                    break;
                case "R":
                    result = "右转弯";
                    break;
            }
            Console.WriteLine("执行动作:{0}", result);
            context.内容 = context.内容.Substring(1, context.内容.Length - 1);//指令减少
        }
    }
    public class 状态表达式 : 表达式
    {
        public override void 解释(指令 context)
        {
            string key = context.内容.Substring(0, 1);
            string result = "";
            switch (key)
            {
                case "S":
                    result = "启动";
                    break;
                case "E":
                    result = "关机";
                    break;
            }
            Console.WriteLine("状态改变:{0}", result);
            context.内容 = context.内容.Substring(1, context.内容.Length - 1);//指令减少
        }
    }

}

/*
 * 解释器模式提供了一个简单的方式来执行语法，而且容易修改或者扩展语法。
 * 给定一个语言，定义它的文法的一种表示，并定义一种解释器，这个解释器使用该表示来解释语言中的句子。
 */
