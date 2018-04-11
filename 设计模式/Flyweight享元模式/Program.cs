using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//共享的元素
namespace Flyweight享元模式
{
    class Program
    {
        static void Main(string[] args)
        {
            和谐家政服务公司 company = new 和谐家政服务公司();//找到中介公司了

            Console.WriteLine("=========找到中介公司:========");
            保姆 _保姆1 = company.安排保姆("小孩");
            保姆 _保姆2 = company.安排保姆("小孩");

            保姆 _保姆4 = company.安排保姆("老人");
            保姆 _保姆5 = company.安排保姆("老人");

            保姆 _保姆7 = company.安排保姆("宠物");
            if (_保姆7 == null)
            {
                _保姆7 = new 宠物保姆();//中介公司不提供,只能自己找:UnSharedConcreteFlyweight
            }
            Console.WriteLine("=========雇佣保姆结束,开始服务:========");
            _保姆1.服务("2号楼301室");
            _保姆2.服务("5号楼601室");

            _保姆4.服务("9号楼201室");
            _保姆5.服务("6号楼402室");

            _保姆7.服务("8号楼101室");

            _保姆1.加薪(20);
            _保姆4.加薪(30);//服务不错,加工资,只给1,4两个加
            Console.WriteLine("============加薪,继续服务:============");

            _保姆1.服务("2号楼301室");
            _保姆2.服务("5号楼601室");

            _保姆4.服务("9号楼201室");
            _保姆5.服务("6号楼402室");

            _保姆7.服务("8号楼101室");

        }
    }

    public abstract class 保姆
    {
        protected string 工号;
        protected string 姓名;
        protected string 公司;
        protected int 时薪;
        //以上是内部状态
        public abstract void 服务(string 工作地点);//工作地点:外部状态
        //改变内部状态,将影响所有共享的本实例
        public virtual void 加薪(int 加薪数量)
        {
            this.时薪 += 加薪数量;
        }
    }

    public class 小孩保姆 : 保姆
    {
        public 小孩保姆(string 公司, int 时薪)
        {
            base.工号 = "001";
            base.姓名 = "翠花";
            base.公司 = 公司;
            base.时薪 = 时薪;
        }
        public override void 服务(string 工作地点)
        {
            Console.WriteLine("耐心是我的特长!\r\n====小孩子保姆:{0},工资标准:{1},正在:{2}为小孩子服务!", base.姓名, base.时薪, 工作地点);
        }
    }
    public class 老人保姆 : 保姆
    {
        public 老人保姆(string 公司, int 时薪)
        {
            base.工号 = "009";
            base.姓名 = "富贵嫂";
            base.公司 = 公司;
            base.时薪 = 时薪;
        }
        public override void 服务(string 工作地点)
        {
            Console.WriteLine("老吾老,以及人之老!\r\n====老人保姆:{0},工资标准:{1},正在:{2}为老人服务!", base.姓名, base.时薪, 工作地点);
        }
    }

    public class 宠物保姆 : 保姆
    {
        public 宠物保姆()
        {
            base.工号 = "000";
            base.姓名 = "潮人";
            base.公司 = "无";
            base.时薪 = 100;
        }
        public override void 服务(string 工作地点)
        {
            Console.WriteLine("声明:非中介,非诚勿扰.\r\n--------宠物保姆{2}:工资标准:{0}.在{1}为宠物服务.", base.时薪, 工作地点, base.姓名);
        }
    }
    /// <summary>
    /// 享元管理类,可以做成单例,保存享元实例,调用并使用
    /// </summary>
    public class 和谐家政服务公司
    {
        private string companyName = "和谐家政服务公司";
        private System.Collections.Hashtable _所有员工 = null;
        //开张
        public 和谐家政服务公司()
        {
            this.companyName = "和谐家政服务公司";
            //招兵买马
            this._所有员工 = new System.Collections.Hashtable();

            //雇佣员工
            保姆 翠花 = new 小孩保姆(this.companyName, 20);
            保姆 富贵嫂 = new 老人保姆(this.companyName, 30);

            //记录在花名册,按服务对象
            this._所有员工.Add("小孩", 翠花);
            this._所有员工.Add("老人", 富贵嫂);
        }
        public 保姆 安排保姆(string 保姆类型)
        {
            switch (保姆类型)
            {
                case "小孩":
                    return this._所有员工["小孩"] as 保姆;
                case "老人":
                    return this._所有员工["老人"] as 保姆;
                default:
                    Console.WriteLine("本公司只为小孩和老人服务.宠物保姆请自已找.");
                    return null;
            }
        }
    }

}

/*
 * 对现有的元素进行重用
 * 当一个功能需要不停的创建对象,则内存会暴涨,此时采用享元模式
 * 多个类引用一个享元类,但是使用此类时会产生不同的效果,
 * 因此,享元类会维护自身的一个状态,进行状态切换,需要区分内部和外部的区别
 * 享元不同于单例 享元可以被实例化
 * 只有当足够多的享元实例可供共享时,才采用享元模式
 */
