using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 抽象工厂
{
    /// <summary>
    /// 简单工厂,一个工厂只生产一种产品,但是产品类型有很多种
    /// 这个工厂通过传入的参数不同,生产出不同的产品
    /// 一个工厂,一个产品基类,很多个产品子类
    /// </summary>
    class SimpleFactory
    {
        public static Computer SimpleFactoryComputer(string op)
        {
            Computer com = null;
            switch (op)
            {
                case "+":
                    com = new AddComputer();
                    break;

                case "-":
                    com = new SubtractionComputer();
                    break;
                case "*":
                    com = new RideComputer();
                    break;
                case "/":
                    com = new DivideComputer();
                    break;
                default:
                    com = new AddComputer();
                    break;
            }
            return com;        }


    }


    public abstract class Computer
    {
        protected int numberA;
        protected int numberB;

        public int NumberA { get => numberA; set => numberA = value; }
        public int NumberB { get => numberB; set => numberB = value; }

        public abstract int Result
        {
            get;
        }

    }

    /// <summary>
    /// 加法
    /// </summary>
    public class AddComputer : Computer
    {
        public override int Result
        {
            get { return base.numberA + base.numberB; }
        }
    }
    /// <summary>
    /// 减法
    /// </summary>
    public class SubtractionComputer : Computer
    {
        public override int Result
        {
            get { return base.numberA - base.numberB; }
        }
    }
    /// <summary>
    /// 乘法
    /// </summary>
    public class RideComputer : Computer
    {
        public override int Result
        {
            get { return base.numberA * base.numberB; }
        }
    }
    /// <summary>
    /// 除法
    /// </summary>
    public class DivideComputer : Computer
    {
        public override int Result
        {
            get { return base.numberA / base.numberB; }
        }
    }
}
