using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iterator迭代器
{
    class Program
    {
        static void Main(string[] args)
        {
            //系统提供的迭代器 IEnumerator
            车上的乘客 隧道六线 = new 车上的乘客();
            隧道六线.上车("张三");
            隧道六线.上车("李四");
            隧道六线.上车("王五");
            隧道六线.上车("赵六");

            Iterator iterator = 隧道六线.CreateIterator();

            while (!iterator.IsDone())
            {
                Console.WriteLine("{0},请你买票,谢谢!", iterator.CurrentItem());
                iterator.Next();
            }
            Console.ReadLine();
        }
    }


    //迭代器,用来执行迭代操作
    public abstract class Iterator
    {
        public abstract object First();//第一个
        public abstract object CurrentItem();//当前这一个
        public abstract object Next();//下一个
        public abstract bool IsDone();//结束了没有?

    }

    //ConcreteIterator//具体的迭代器
    public class ConcreteIterator : Iterator
    {
        private 车上的乘客 _乘客们 = null;

        private int 当前第几个 = 0;

        public ConcreteIterator(车上的乘客 乘客)
        {
            this._乘客们 = 乘客;
        }
        public override object First()
        {
            if (this._乘客们.数量 > 0)
            {
                this.当前第几个 = 0;
                return this._乘客们[0];
            }
            else
            {
                return null;
            }
        }
        public override object CurrentItem()
        {
            return this._乘客们[当前第几个];
        }

        public override object Next()
        {
            this.当前第几个++;
            if (this.当前第几个 < this._乘客们.数量)
            {
                return this._乘客们[当前第几个];
            }
            else
            {
                return null;
            }
        }

        public override bool IsDone()
        {
            return this.当前第几个 >= this._乘客们.数量;
        }
    }

    //聚合类,代表一个可迭代的集合
    public abstract class Aggregate
    {

        public abstract Iterator CreateIterator();//返回一个迭代器
    }
    //ConcreteAggregate//具体的聚合类
    public class 车上的乘客 : Aggregate
    {
        private System.Collections.ArrayList al = new System.Collections.ArrayList();

        public override Iterator CreateIterator()
        {
            return new ConcreteIterator(this);
        }

        public void 上车(string 乘客姓名)
        {
            this.al.Add(乘客姓名);
        }
        public string this[int index]
        {
            get
            {
                return this.al[index].ToString();
            }
        }
        public int 数量
        {
            get
            {
                return this.al.Count;
            }
        }
    }

}
/*
 * 提供一种方法顺序访问一个聚合对象中的各个元素，而又不暴露该对象的内部表示。
 在面向对象的软件设计中，我们经常会遇到一类集合对象，这类集合对象的内部结构可能有着各种各样的实现，但是归结起来，无非有两点是需要我们去关心的：一是集合内部的数据存储结构，二是遍历集合内部的数据。面向对象设计原则中有一条是类的单一职责原则，所以我们要尽可能的去分解这些职责，用不同的类去承担不同的职责。
Iterator模式就是分离了集合对象的遍历行为，抽象出一个迭代器类来负责，这样既可以做到不暴露集合的内部结构，又可让外部代码透明的访问集合内部的数据。

 */
