using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace State状态模式
{
    class Program
    {
        static void Main(string[] args)
        {
            情人节 loverday = new 情人节();

            Console.WriteLine("\n第一年的情人节:");
            loverday.状态 = new 单身状态();
            loverday.过节();

            Console.WriteLine("\n第二年的情人节:");
            loverday.状态 = new 热恋状态();
            loverday.过节();

            Console.WriteLine("\n第三年的情人节:");
            loverday.状态 = new 失恋状态();
            loverday.过节();

        }
    }
    //Context
    public class 情人节
    {
        //-state 默认是单身状态
        private 节日状态 state = new 单身状态();

        public 节日状态 状态
        {
            get { return this.state; }
            set { this.state = value; }
        }
        //Request()
        public void 过节()
        {
            this.state.庆祝节日();
        }
    }
    //State
    public abstract class 节日状态
    {
        public abstract void 庆祝节日();//Handle()
    }
    public class 单身状态 : 节日状态     //ConcreteSate
    {
        public override void 庆祝节日()
        {
            Console.WriteLine("单身情歌,没有情人的情人节:( ~~~~");
        }
    }
    public class 热恋状态 : 节日状态   //ConcreteSate
    {
        public override void 庆祝节日()
        {
            Console.WriteLine("花好月园,花前月下,生活真美好:) ~~~~");
        }
    }
    public class 失恋状态 : 节日状态    //ConcreteSate
    {
        public override void 庆祝节日()
        {
            Console.WriteLine("对酒当歌几多愁,恰似一江春水向东流 &_& ~~~~");
        }
    }
}
