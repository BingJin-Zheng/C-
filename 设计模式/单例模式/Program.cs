using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 单例模式
{
    class Program
    {
        static void Main(string[] args)
        {
        }
    }

    #region 简单实现, 对于线程来说不安全,惰性实例化
    public sealed class Singleton1
    {
        static Singleton1 instance = null;

        public void Show()
        {
            Console.WriteLine("instance function");
        }
        private Singleton1()
        {
        }

        public static Singleton1 Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Singleton1();
                }
                return instance;
            }
        }
    }
    #endregion

    #region 线程安全,增加了额外的开销，损失了性能
    public sealed class Singleton2
    {
        static Singleton2 instance = null;
        private static readonly object padlock = new object();

        private Singleton2()
        {
        }

        public static Singleton2 Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new Singleton2();
                    }
                }

                return instance;
            }
        }
    }
    #endregion

    #region 双重锁定,多线程安全,线程不是每次都加锁,允许实例化延迟到第一次访问对象时发生
    public sealed class Singleton3
    {
        static Singleton3 instance = null;
        private static readonly object padlock = new object();

        private Singleton3()
        {
        }

        public static Singleton3 Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (padlock)
                    {
                        if (instance == null)
                        {
                            instance = new Singleton3();
                        }
                    }
                }
                return instance;
            }
        }
    }
    #endregion

    #region 静态初始化
    /// <summary>
    /// 依赖公共语言运行库负责处理变量初始化,公共静态属性为访问实例提供了一个全局访问点,对实例化机制的控制权较少(.NET代为实现),静态初始化是在.NET 中实现 Singleton 的首选方法
    /// </summary>
    public sealed class Singleton4
    {
        private static readonly Singleton4 instance = null;
        static Singleton4()
        {
            instance = new Singleton4();
        }
        private Singleton4()
        {
        }
        public static Singleton4 Instance
        {
            get
            {
                return instance;
            }
        }
    }

    #endregion

    #region 延迟初始化
    public sealed class Singleton5
    {
        private Singleton5()
        {
        }
        public static Singleton5 Instance
        {
            get
            {
                return Nested.instance;
            }
        }

        public static void Hello()
        {
        }

        private class Nested
        {
            internal static readonly Singleton5 instance = null;
            static Nested()
            {
                instance = new Singleton5();
            }
        }
    }
    #endregion

    #region 注意
    /*
     * 1、Singleton模式中的实例构造器可以设置为protected以允许子类派生。
     * 2、Singleton模式一般不要支持ICloneable接口，因为这可能会导致多个对象实例，与Singleton模式的初衷违背。
     * 3、Singleton模式一般不要支持序列化，因为这也有可能导致多个对象实例，同样与Singleton模式的初衷违背。
     * 4、Singletom模式只考虑到了对象创建的管理，没有考虑对象销毁的管理。就支持垃圾回收的平台和对象的开销来讲，我们一般没有必要对其销毁进行特殊的管理。
     * 5、可以很简单的修改一个Singleton，使它有少数几个实例，这样做是允许的而且是有意义的。
     */


    #endregion
}
