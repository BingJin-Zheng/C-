using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bridge桥接模式
{
    class Program
    {
        static void Main(string[] args)
        {
            ///接口类
            职位 p1 = new 董事长();
            职位 p2 = new 办公室主任();
            ///具体功能实现类,修改实现,对整个框架没有影响
            党内职务 d1 = new 支部书记();
            党内职务 d2 = new 纪检委员();
            党内职务 d3 = new 调研员();


            //具体功能实现类可以供接口类使用,接口可以随意与功能组合,并且可以复用实现类
            p1.任命(d1);
            p1.任命(d2);
            p2.任命(d3);

            p1.履行职责();
            p2.履行职责();



            Console.ReadLine();
        }
    }
    /// <summary>
    /// 第一个纬度的抽象
    /// </summary>
    public abstract class 职位
    {
        public abstract string 名称 { get; }
        private List<党内职务> duties = new List<党内职务>();

        public void 任命(党内职务 newDuty)
        {
            this.duties.Add(newDuty);
        }

        public void 履行职责()
        {
            Console.WriteLine("{0}开始履行职责", this.名称);
            foreach (var item in this.duties)
            {
                (item as 党内职务).为人民服务();
            }
            Console.WriteLine("{0}履行职责结束", this.名称);
        }
    }
    /// <summary>
    /// 第一个纬度的对象
    /// </summary>
    public class 董事长 : 职位
    {
        public override string 名称
        {
            get
            {
                 return "董事长";
            }
        }
    }
    /// <summary>
    /// 第一个纬度的对象
    /// </summary>
    public class 办公室主任 : 职位
    {
        public override string 名称
        {
            get
            {
                return "办公室主任";
            }
        }
    }



    /// <summary>
    /// 第二个纬度的抽象
    /// </summary>
    public abstract class 党内职务
    {
        public abstract void 为人民服务();
    }
    /// <summary>
    /// 第二个纬度的对象
    /// </summary>
    public class 支部书记 : 党内职务
    {
        public override void 为人民服务()
        {
            Console.WriteLine("组织党员进行思想总结和自我批评");
        }
    }
    /// <summary>
    /// 第二个纬度的对象
    /// </summary>
    public class 纪检委员 : 党内职务
    {
        public override void 为人民服务()
        {
            Console.WriteLine("听取人民群众的意见");
        }
    }
    /// <summary>
    /// 第二个纬度的对象
    /// </summary>
    public class 调研员 : 党内职务
    {
        public override void 为人民服务()
        {
            Console.WriteLine("调研基层思想工作");
        }
    }

}
/*
 * 2中纬度,各有各的功能作用,现在需要将其融合
 * 使用桥接模式将这2个纬度融合组成一种新的产品
 * 比如:手机品牌和手机软件,这是2个纬度,现在要融合成为一部手机
 * 利用继承的话会形成过度继承,不推荐
 * 利用组合将手机软件装到手机品牌里面
 */
