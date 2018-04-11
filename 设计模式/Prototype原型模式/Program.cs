using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prototype原型模式
{
    class Program
    {
        static void Main(string[] args)
        {
            Prototype p = new Build();
            p.Number = 8;
            p.Address = "郑州中原塔";

            Prototype p2 = p.Clone() as Prototype;

            Console.WriteLine(p);
            Console.WriteLine(p2);

            Console.WriteLine("\n 下面使用接口进行编程 \n");

            Build2 b2 = new Build2();
            b2.Number = 204;
            b2.Address = "南京燕子矶";
            Build2 b21 = (b2 as ICloneable).Clone() as Build2;

            Console.WriteLine(b2);
            Console.WriteLine(b21);

            Console.ReadKey();
        }

    }

    public abstract class Prototype
    {
        public int Number { get; set; }
        public string Address { get; set; }

        public abstract object Clone();

        public override string ToString()
        {
            return string.Format("门牌号:{0},地址:{1}",this.Number,this.Address); 
        }
    }

    public class Build : Prototype
    {
        public override object Clone()//深拷贝
        {
            Build b = new Build();
            b.Number = this.Number+ 1;
            b.Address = this.Address;
            return b;
        }
    }

    public class Build2 : ICloneable
    {
        public int Number { get; set; }
        public string Address { get; set; }
        public object obj;
        public object Clone()
        {
            ///最好
            Build2 b2 = base.MemberwiseClone() as Build2;
            obj = new object();
            return obj;
            //浅拷贝,只拷贝值,引用对象的引用,不拷贝引动对象的实例  
            //return base.MemberwiseClone();
            //深拷贝
            //Build2 b = new Build2();
            //b.Number = this.Number + 1;
            //b.Address = this.Address;
            //return b;
        }

        public override string ToString()
        {
            return string.Format("门牌号:{0},地址:{1}", this.Number, this.Address);
        }
    }
}
/*
 * 原型模式是从一个模板类,复制成另一个类使用,包括模板类自身所带的数据
 * 一个原型基类/接口,返回一个克隆的对象,有深拷贝(拷贝实例内存一份)与浅拷贝(拷贝引用一份)之分
 * 在系统库中提供了一个接口 ICloneable 可以直接做出 Clone 方法
 */
