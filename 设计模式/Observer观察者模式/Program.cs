using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Observer观察者模式
{
    class Program
    {
        static void Main(string[] args)
        {
            艺术品拍卖 subject = new 艺术品拍卖(8);

            竞拍人 observer1 = new 艺术品竞拍人("比尔盖茨", 8, 11, subject);
            竞拍人 observer2 = new 艺术品竞拍人("布什", 8, 9, subject);
            竞拍人 observer3 = new 艺术品竞拍人("普京", 8, 8, subject);
            竞拍人 observer4 = new 艺术品竞拍人("萨打姆", 8, 8, subject);

            int n = 1;
            while (!subject.结束)
            {
                Console.WriteLine("========第{0}轮报价=========", n);
                subject.报价();
                n++;
            }
        }
    }

    //Observer
    public abstract class 竞拍人
    {
        //Update()
        public abstract void 出价();
    }
    //ConcreteObserver
    public class 艺术品竞拍人 : 竞拍人
    {   //ObserverState
        private int _myprice;
        private int _topprice;
        private string _name;

        //subject
        private 艺术品拍卖 subject;

        public 艺术品竞拍人(string 竞拍人姓名, int 起始价位, int 最高心理价位, 艺术品拍卖 参加的拍卖)
        {
            this._myprice = 起始价位;
            this._topprice = 最高心理价位;
            this._name = 竞拍人姓名;
            this.subject = 参加的拍卖;
            this.subject.注册竞拍人(this);
        }
        public override string ToString()
        {
            return this._name;
        }       
        //Update
        public override void 出价()
        {
            if (this.subject.当前价格 > this._topprice)
            {
                Console.WriteLine("{0}退出,当前最高价格:{1}已超出最高心理价位:{2}", this._name, this.subject.当前价格, this._topprice);
                this.subject.注销竞拍人(this);
            }
            else
            {
                this._myprice += 1;
                this.subject.当前价格 = _myprice;
                if (this._myprice >= this.subject.当前价格)
                {
                    Console.WriteLine("{0}报出新的最高价格:{1}", this._name, this._myprice);
                }
            }
        }
    }

    //Subject
    public abstract class 拍卖
    {
        protected System.Collections.ArrayList observers = new System.Collections.ArrayList();
        //Attach
        public void 注册竞拍人(竞拍人 person)
        {
            this.observers.Add(person);
            Console.WriteLine("------{0}加入拍卖.", person);
        }
        //Detach
        public void 注销竞拍人(竞拍人 person)
        {
            this.observers.Remove(person);
            Console.WriteLine("-----{0}离开拍卖.", person);
        }
        //Notify,广播
        public virtual void 报价()
        {
            for (int i = this.observers.Count - 1; i >= 0; i--)
            {
                ((竞拍人)this.observers[i]).出价();
            }
        }
        public abstract bool 结束 { get; }
    }
    //ConcreteSubject
    public class 艺术品拍卖 : 拍卖
    {
        public int _price;
        public 艺术品拍卖(int 起拍价格)
        {
            this._price = 起拍价格;
        }
        //SubjectState
        public int 当前价格
        {
            get
            {
                return this._price;
            }
            set
            {
                if (this._price > value)
                {
                    return;
                }
                this._price = value;
            }
        }
        //Notify,广播
        public override void 报价()
        {
            base.报价();
            if (base.observers.Count == 1)
            {
                Console.WriteLine("{0}最终胜出,最终报价为:{1}", base.observers[0], this._price);
            }
            else
            {
                if (base.observers.Count == 0)
                {
                    Console.WriteLine("本次拍卖流拍.");
                }
            }
        }
        //SubjectState
        public override bool 结束
        {
            get { return base.observers.Count == 1 || base.observers.Count == 0; }
        }

    }

}
/*
 * 在软件构建过程中，我们需要为某些对象建立一种“通知依赖关系” ——一个对象（目标对象）的状态发生改变，所有的依赖对象（观察者对象）都将得到通知。如果这样的依赖关系过于紧密，将使软件不能很好地抵御变化。
使用面向对象技术，可以将这种依赖关系弱化，并形成一种稳定的依赖关系。从而实现软件体系结构的松耦合。

定义对象间的一种一对多的依赖关系，以便当一个对象的状态发生改变时，所有依赖于它的对象都得到通知并自动更新。

 */
