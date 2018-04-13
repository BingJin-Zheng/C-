using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateMethod模板方法
{
    class Program
    {
        static void Main(string[] args)
        {
            Chef gC = new GoodChef();
            Chef bC = new BadChef();

            gC.doJob();
            Console.ReadLine();
            bC.doJob();

            Console.ReadLine();

        }
    }
    /*
     * 模板类有自定义的方法,不需要子类重写
     */

    public abstract class Chef
    {
        /// <summary>
        /// 钩子函数,调用子类的方法,在这个地方进行修改
        /// </summary>
        public void doJob()
        {
            this.WashVegetables();
            this.Cooking();
            this.GoToEat();
        }
        //洗菜
        public abstract void WashVegetables();
        //做饭
        public abstract void Cooking();
        //拿出去吃
        public abstract void GoToEat();


    }

    public class GoodChef : Chef
    {
        public override void Cooking()
        {
            Console.WriteLine("好厨师做饭");
        }

        public override void GoToEat()
        {
            Console.WriteLine("好厨师端给客人吃");
        }

        public override void WashVegetables()
        {
            Console.WriteLine("好厨师洗菜");
        }
    }
    public class BadChef : Chef
    {
        public override void Cooking()
        {
            Console.WriteLine("坏厨师做饭");
        }

        public override void GoToEat()
        {
            Console.WriteLine("坏厨师端给客人吃");
        }

        public override void WashVegetables()
        {
            Console.WriteLine("坏厨师洗菜");
        }
    }
}
/*
 *  定义一个操作中的算法的骨架，而将一些步骤延迟到子类中.
 *  Template Method使得子类可以不改变一个算法的结构即可重定义该算法的某些特定步骤。
 *  1．一次性实现一个算法的不变的部分，并将可变的行为留给子类来实现。

2．各子类中公共的行为应被提取出来并集中到一个公共父类中以避免代码重复。这是Opdyke和Johnson所描述过的“重分解以一般化”的一个很好的例子。首先识别现有代码中的不同之处，并且将不同之处分离为新的操作。最后，用一个调用这些新的操作的模板方法来替换这些不同的代码。

3．控制子类扩展。模板方法只在特定点调用“Hook”操作，这样就只允许在这些点进行扩展。
4．Template Method模式是一种非常基础性的设计模式，在面向对象系统中有着大量的应用。它用最简洁的机制（虚函数的多态性）为很多应用程序框架提供了灵活的扩展点，是代码复用方面的基本实现结构。

5．除了可以灵活应对子步骤的变化外，“不用调用我，让我来调用你（Don't call me ,let me call you)”的反向控制结构是Template Method的典型应用。“Don’t call me.Let me call you”是指一个父类调用一个子类的操作，而不是相反。

6．在具体实现方面，被Template Method调用的虚方法可以具有实现，也可以没有任何实现（抽象方法，纯虚方法），但一般推荐将它们设置为protected方法。
 */
