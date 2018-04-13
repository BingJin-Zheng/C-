using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Visitor访问者模式
{
    class Program
    {
        static void Main(string[] args)
        {
            学校 school = new 学校();

            学生 s1 = new 天才学生();
            学生 s2 = new 调皮学生();

            school.学生入学(s1);
            school.学生入学(s2);

            老师 mathTeacher = new 数学老师();

            Console.WriteLine("\n\t数学老师入职,先去家访.\n");
            school.老师入职(mathTeacher);

            老师 englishTeacher = new 英语老师();

            Console.WriteLine("\n\t英语老师入职,先去家访.\n");
            school.老师入职(englishTeacher);

            Console.WriteLine("\n\t调皮学生退学.\n");
            school.学生退学(s2);

            老师 et = new 英语老师();//第3个老师

            Console.WriteLine("\n\t新的英语老师入职,先去家访.\n");
            school.老师入职(et);
        }
    }

    //ObjectStructure
    public class 学校
    {
        private System.Collections.ArrayList elements = new System.Collections.ArrayList();

        public void 学生入学(学生 element)
        {
            this.elements.Add(element);
        }
        public void 学生退学(学生 element)
        {
            this.elements.Remove(element);
        }
        public void 老师入职(老师 vistor)
        {
            //入职先做一遍家访
            foreach (学生 element in this.elements)
            {
                element.接受家访(vistor);
            }
        }
    }

    //Vistor
    public abstract class 老师
    {
        //Visit
        public abstract void 做天才学生家访(天才学生 element);
        public abstract void 做调皮学生家访(调皮学生 element);
    }
    //ConcreteVistor
    public class 数学老师 : 老师
    {
        public override void 做天才学生家访(天才学生 element)
        {
            Console.WriteLine("\n{0}:孩子啊,你是个天才,一定要学好数学,走遍天下都不怕.", this.GetType().Name);
            Console.WriteLine("{0}家访了:{1}", this.GetType().Name, element.GetType().Name);
        }

        public override void 做调皮学生家访(调皮学生 element)
        {
            Console.WriteLine("\n{0}:孩子啊,你虽然调皮,但是数学还是要好好学,否则不会数钱的.", this.GetType().Name);
            Console.WriteLine("{0}家访了:{1}", this.GetType().Name, element.GetType().Name);
        }
    }

    public class 英语老师 : 老师
    {
        public override void 做天才学生家访(天才学生 element)
        {
            Console.WriteLine("\n{0}:孩子啊,你是个天才,学好英语,将来出国到利比亚.", this.GetType().Name);
            Console.WriteLine("{0}家访了:{1}", this.GetType().Name, element.GetType().Name);
        }

        public override void 做调皮学生家访(调皮学生 element)
        {
            Console.WriteLine("\n{0}:孩子啊,你虽然调皮,但也要学好英语,否则老外骂你也听不懂啊.", this.GetType().Name);
            Console.WriteLine("{0}家访了:{1}", this.GetType().Name, element.GetType().Name);
        }
    }
    //Element
    public abstract class 学生
    {
        //Accept
        public abstract void 接受家访(老师 vistor);
    }
    //ConcreteElement
    public class 天才学生 : 学生
    {
        public override void 接受家访(老师 vistor)
        {
            vistor.做天才学生家访(this);
            this.显示天才();
        }
        //具体类中自己的方法
        public void 显示天才()
        {
            Console.WriteLine("天才学生做了个腾空1800度空翻");
        }
    }

    public class 调皮学生 : 学生
    {
        public override void 接受家访(老师 vistor)
        {
            vistor.做调皮学生家访(this);
            this.开玩笑();
        }
        //具体类中自己的方法
        public void 开玩笑()
        {
            Console.WriteLine("调皮学生和老师开玩笑,哈哈哈哈");
        }
    }

}
/*
 * 在软件构建过程中，由于需求的改变，某些类层次结构中常常需要增加新的行为（方法），如果直接在基类中做这样的更改，将会给子类带来很繁重的变更负担，甚至破坏原有设计。
如何在不更改类层次结构的前提下，在运行时根据需要透明地为类层次结构上的各个类动态添加新的操作，从而避免上述问题？
表示一个作用于某对象结构中的各元素的操作。它可以在不改变各元素的类的前提下定义作用于这些元素的新的操作。

    访问者模式有2个参与者
访问者
被访问者
访问者是一组操作的集合，对他所知的被访问者进行操作。
访问者模式有2个重点，
已知被访问者，就是说访问者只能访问有限的对象，如果所访问的对象增加了，那么就要修改访问者了
被访问者必须有个统一的被访问接口。
 */
