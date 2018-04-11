using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 抽象工厂
{
    /// <summary>
    /// 工厂方法
    /// 一个产品基类,多个产品子类,一个工厂基类,多个工厂子类,还是一种产品
    /// 产品的生成,推迟到了工厂的子类里面去做
    /// </summary>
    class FactoryMethod
    {
    }

    /// <summary>
    /// 工厂抽象类
    /// </summary>
    public abstract class FactoryFM
    {
        public abstract ComputerFM CreateComputer(int a,int b);
    }
    /// <summary>
    /// 基类可以不是抽象类,可以有默认实现
    /// </summary>
    public class FactoryFM2
    {
        public virtual ComputerFM CreateComputer(int a, int b)
        {
            return (new AddFactoryFM()).CreateComputer(a,b);
        }
    }



    public class AddFactoryFM : FactoryFM
    {
        public override ComputerFM CreateComputer(int a, int b)
        {
            return new AddComputerFM() {NumberA=a,NumberB=b };
        }
    }
    public class SubtractionFactoryFM : FactoryFM
    {
        public override ComputerFM CreateComputer(int a, int b)
        {
            return new SubtractionComputerFM() { NumberA = a, NumberB = b };
        }
    }
    public class RideFactoryFM : FactoryFM
    {
        public override ComputerFM CreateComputer(int a, int b)
        {
            return new RideComputerFM() { NumberA = a, NumberB = b };
        }
    }
    public class DivideFactoryFM : FactoryFM
    {
        public override ComputerFM CreateComputer(int a, int b)
        {
            return new DivideComputerFM() { NumberA = a, NumberB = b };
        }
    }




    /// <summary>
    /// 产品基类
    /// </summary>
    public abstract class ComputerFM
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
    public class AddComputerFM : ComputerFM
    {
        public override int Result
        {
            get { return base.numberA + base.numberB; }
        }
    }
    /// <summary>
    /// 减法
    /// </summary>
    public class SubtractionComputerFM : ComputerFM
    {
        public override int Result
        {
            get { return base.numberA - base.numberB; }
        }
    }
    /// <summary>
    /// 乘法
    /// </summary>
    public class RideComputerFM : ComputerFM
    {
        public override int Result
        {
            get { return base.numberA * base.numberB; }
        }
    }
    /// <summary>
    /// 除法
    /// </summary>
    public class DivideComputerFM : ComputerFM
    {
        public override int Result
        {
            get { return base.numberA / base.numberB; }
        }
    }
}

