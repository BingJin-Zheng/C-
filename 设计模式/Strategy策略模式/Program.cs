using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strategy策略模式
{
    class Program
    {
        static void Main(string[] args)
        {
            double 总价 = 1000;

            优惠策略 strategy;

            顾客 customer = new 顾客(总价);

            strategy = new 要赠品();
            customer.使用优惠(strategy);

            strategy = new 返现金();
            customer.使用优惠(strategy);

            strategy = new 打8折();
            customer.使用优惠(strategy);
        }
    }
    //Strategy策略
    public abstract class 优惠策略
    {
        //algorithmInterface 算法定义
        public abstract double 计算优惠(double 总价);
    }
    public class 要赠品 : 优惠策略      //ConcreteStrategy
    {
        public override double 计算优惠(double 总价)
        {
            Console.WriteLine("不折扣现金,要赠品:\t送一个玩具狗熊.");
            return 总价;
        }
    }

    public class 打8折 : 优惠策略      //ConcreteStrategy
    {
        public override double 计算优惠(double 总价)
        {
            Console.WriteLine("打8折.");
            return 总价 * 0.8;
        }
    }
    public class 返现金 : 优惠策略      //ConcreteStrategy
    {
        public override double 计算优惠(double 总价)
        {
            Console.WriteLine("满300元送100元.");
            if (总价 >= 300)
            {
                return 总价 - 100;
            }
            else
            {
                return 总价;
            }
        }
    }
    //Context
    public class 顾客
    {
        private double total;
        public 顾客(double 总价)
        {
            this.total = 总价;
        }
        public void 使用优惠(优惠策略 strategy)
        {
            double 实价 = 0;
            实价 = strategy.计算优惠(this.total);
            Console.WriteLine("本次优惠后,实际价格为:{0}\n", 实价);
        }
    }
}
/*
 * 定义一系列算法，把它们一个个封装起来，并且使它们可互相替换。该模式使得算法可独立于使用它的客户而变化。
 * 状态模式侧重于当前只有一种状态下,做什么功能,策略侧重于可以使用多种算法组合进行对类的补充
 */
