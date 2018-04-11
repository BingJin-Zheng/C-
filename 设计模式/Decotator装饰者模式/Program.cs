using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decotator装饰者模式
{
    class Program
    {
        static void Main(string[] args)
        {

            Camera c = new Camera500D();
            Decorator d = new SanJiaoJia();
            Decorator d1 = new UVJing();
            Decorator d2 = new ShanGuangDeng();

            d.ObjectToDecorator = c;
            d1.ObjectToDecorator = d;
            d2.ObjectToDecorator = d1;
            d2.Decorate();


            ///链式调用终极版,不灵活,如果需要写的更加灵活需要更多代码做判断
            Decorator d3 = new ShanGuangDeng();
            Decorator d4 = new ShanGuangDeng();
            d2.Decorate(d1).Decorate(d).Decorate(d3).Decorate(d4).Decorate(c).Decorate();

            Console.ReadLine();
        }
    }

    /// <summary>
    /// 装饰接口,可以动态添加新功能的接口
    /// </summary>
    public interface IDecoratable
    {
        /// <summary>
        /// 添加并运行的功能
        /// </summary>
        void Decorate();
    }
    /// <summary>
    /// 相机
    /// </summary>
    public abstract class Camera : IDecoratable
    {
        /// <summary>
        /// 实现接口中定义的新的功能
        /// </summary>
        public void Decorate()
        {
            ///转化为相机自身的功能进行拍照
            this.TakePhoto();
        }

        public abstract void TakePhoto();
    }
    /// <summary>
    /// 实现原本功能的子类,已经工作过一段时间,相对于的是需要添加新的功能
    /// </summary>
    public class Camera500D : Camera
    {

        public override void TakePhoto()
        {
            Console.WriteLine("Camera500D 拍照");
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }

    /// <summary>
    /// 抽象装饰类,将原先实现功能的子类进行装饰,然后进行功能的顺序性调用,实现动态功能的添加
    /// IDecoratable的含义是可以添加新功能的接口,Decorator的含义是实现填加的新功能
    /// </summary>
    public abstract class Decorator : IDecoratable
    {
        /// <summary>
        /// 反向找到最终调用方法的类,反向链表使用
        /// </summary>
        private Decorator parent;
        /// <summary>
        /// 引用需要被装饰的类
        /// </summary>
        public IDecoratable ObjectToDecorator { get; set; }

        /// <summary>
        /// 形成装饰类的链式调用,重载
        /// </summary>
        /// <param name="ObjectToDecorator"></param>
        /// <returns></returns>
        public Decorator Decorate(Decorator ObjectToDecorator)
        {
            this.ObjectToDecorator = ObjectToDecorator;
            ObjectToDecorator.parent = this;
            return ObjectToDecorator;
        }
        /// <summary>
        /// 寻找到第一个开始装饰的类,然后进行调用,反向链表寻找父亲
        /// </summary>
        /// <param name="ObjectToDecorator"></param>
        /// <returns></returns>
        public Decorator Decorate(IDecoratable ObjectToDecorator)
        {
            this.ObjectToDecorator = ObjectToDecorator;
            Decorator parentD = this.parent;
            this.parent = null;//打破循环,为了GC垃圾回收
            while (parentD.parent != null)
            {
                Decorator pd1 = parentD;
                parentD = pd1.parent;
                pd1.parent = null;
            }
            return parentD;
        }

        public virtual void Decorate()
        {
            if (this.ObjectToDecorator != null)
            {
                this.ObjectToDecorator.Decorate(); 
            }
        }
    }
    /// <summary>
    /// 给原先实现功能的类 装载另外一个功能 的装饰类,并且可以装载自身
    /// </summary>
    public class SanJiaoJia : Decorator
    {
        public override void Decorate()
        {

            Console.WriteLine("装了三脚架");
            base.Decorate();
        }
    }
    /// <summary>
    /// 给原先实现功能的类 装载另外一个功能 的装饰类,并且可以装载自身
    /// </summary>
    public class UVJing : Decorator
    {
        public override void Decorate()
        {

            Console.WriteLine("装了UV镜");
            base.Decorate();
        }
    }
    /// <summary>
    /// 给原先实现功能的类 装载另外一个功能 的装饰类,并且可以装载自身
    /// </summary>
    public class ShanGuangDeng : Decorator
    {
        public override void Decorate()
        {

            Console.WriteLine("装了闪光灯");
            base.Decorate();
        }
    }

    #region 小测试
    public abstract class Component
    {
        public abstract void work();
    }
    /// <summary>
    /// 装饰类为甚么要继承基类,是应为要和基类保持统一的行为
    /// </summary>
    public class Decotator : Component
    {
        /// <summary>
        /// 需要被装饰的对象,表现出一种顺序
        /// </summary>
        public Component DecotatorComponent;

        public override void work()
        {
            if (DecotatorComponent!=null)
            {
                DecotatorComponent.work();
            }
            Console.WriteLine("保持和基类统一的行为");
        }
    }

    public class Component1 : Component
    {
        public override void work()
        {
            throw new NotImplementedException();
        }
    }

    public class Decotator1 : Decotator
    {
        public void doOtherWork()
        {

        }
    }
    #endregion


}

/*
 * 当我们使用继承来扩展子类的功能,每次扩展功能,就要子类继承父类进行扩展,很被动 
 * 为了避免子类因为扩展功能增多带来的膨胀问题,并且根据需要动态的实现功能,使功能扩展的影响降到最低
 * 采用装饰设计模式
 * 如果不太理解这个模式,需要打断点一步一步看调用栈
 */
