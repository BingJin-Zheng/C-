using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;


namespace 抽象工厂
{
    class Program
    {
        static void Main(string[] args)
        {
            //Factory2();
            Factory3();
            Console.ReadKey();

        }
        /// <summary>
        /// 简单工厂测试
        /// </summary>
        private static void Factory1()
        {

            Console.WriteLine("请输入第一个数字");
            int a = int.Parse(Console.ReadLine());
            Console.WriteLine("请输入第二个数字");
            int b = int.Parse(Console.ReadLine());
            Console.WriteLine("请输入运算符号");
            string op = Console.ReadLine();
            int result;
            Computer com = SimpleFactory.SimpleFactoryComputer(op);

            com.NumberA = a;
            com.NumberB = b;
            result = com.Result;
            Console.WriteLine(result);
        }
        /// <summary>
        /// 工厂方法
        /// </summary>
        private static void Factory2()
        {
            Console.WriteLine("请输入第一个数字");
            int a = int.Parse(Console.ReadLine());
            Console.WriteLine("请输入第二个数字");
            int b = int.Parse(Console.ReadLine());
            Console.WriteLine("请输入运算符号");
            string op = Console.ReadLine();
            int result;
            ComputerFM com = null;
            FactoryFM ffm = null;
            ///这个地方最终需要通过反射机制,动态创建工厂子类
            switch (op)
            {
                case "+":
                    ffm = new AddFactoryFM();
                    break;

                case "-":
                    ffm = new SubtractionFactoryFM();
                    break;
                case "*":
                    ffm = new RideFactoryFM();
                    break;
                case "/":
                    ffm = new DivideFactoryFM();
                    break;
                default:
                    ffm = new AddFactoryFM();
                    break;
            }
            com = ffm.CreateComputer(a, b);
            result = com.Result;
            Console.WriteLine(result);
        }
        /// <summary>
        /// 抽象工厂
        /// </summary>
        private static void Factory3()
        {

            Shoes s = null;
            Hat h = null;
            SportsShop shop = null;
            //这个地方可以使用反射机制进行动态创建,利用反射找到SportsShop的子类,使用
            //System.Reflection.Assembly.LoadFile(".Dll");找到这个typeName
            //System.Reflection.Assembly实例方法CreateInstance(string typeName);创建出来这个子类
            //当你把new这种操作符省去之后,将会带来极大的便利性
            //shop = new LiNingShop();
            //shop = new NickShop();
            System.Reflection.Assembly assembly = System.Reflection.Assembly.LoadFile(@"G:\GitHub\CSharpDesignMode\设计模式\抽象工厂\bin\Debug\抽象工厂.exe");
            foreach (var item in assembly.GetTypes())
            {
                ///IsEquivalentTo和那个类型相等  IsSubclassOf是那个类的子类
                if (item.IsEquivalentTo(typeof(LiNingShop)))
                {
                    shop = assembly.CreateInstance(item.FullName) as SportsShop;
                    Console.WriteLine("lining牌子的反射实现{0}", shop);
                }
            }
            
            s = shop.SellShoes();
            h = shop.SellHat();

            Console.WriteLine("鞋子:{0},帽子:{1}",s,h);
        }
    }
}

/*
 * 建造者模式解决的是一个对象自身某部分功能
 * 简单工厂仅仅是 对面向过程的封装,进行方法调用并返回产品 (这个地方在抽象工厂中使用了反射技术,去掉new关键字)
 * 工厂方法解决的是 单一产品多种类型
 * 抽象工厂是工厂方法的升级 解决的是 多种产品,多个系列组合的问题
 */