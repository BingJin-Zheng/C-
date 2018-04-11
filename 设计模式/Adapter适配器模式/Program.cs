using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adapter适配器模式
{
    class Program
    {
        static void Main(string[] args)
        {
            Player p1 = new USAPlayer() { Name = "美国队员" };
            p1.Attack();
            Console.WriteLine(p1);

            Player p2 = new AdapterPlayer() { Name = "中国球员" };
            p2.Attack();
            Console.WriteLine(p2);


            Console.ReadLine();
        }
    }

    /// <summary>
    /// 一个正在项目中使用的基类
    /// 可以成为老的接口,后续添加新接口以及适配新接口的适配器,适配器继承自老接口
    /// </summary>
    public abstract class Player
    {
        public string Name { get; set; }
        /// <summary>
        /// 新接口中必须要实现的方法
        /// </summary>
        public abstract void Attack();
        public abstract void Defense();
    }

    public class USAPlayer : Player
    {
        public override void Attack()
        {
            Console.WriteLine("Attack!");
        }

        public override void Defense()
        {
            Console.WriteLine("Defense!");
        }
        public override string ToString()
        {
            return string.Format("{0}正在进攻与防守", this.Name);
        }
    }

    public class AdapterPlayer : Player
    {

        private ChinaCBAPlayer cba = new ChinaCBAPlayer();
        public override void Attack()
        {
            cba.进攻();
        }

        public override void Defense()
        {
            cba.防守();
        }

        public override string ToString()
        {
            return string.Format("{0}正在进攻与防守",this.Name);
        }
    }

    /// <summary>
    /// 新的接口
    /// </summary>
    public class ChinaCBAPlayer
    {
        public void 进攻()
        {
            Console.WriteLine("进攻!");
        }

        public void 防守()
        {
            Console.WriteLine("防守!");
        }
    }

}

/**
 * 一个老的模块与一个新的模块都存在与项目中,一些操作需要用到老的模块,一些操作需要用到新的模块
 * 就需要一个适配器将接口封装起来,让使用者调用适配器
 * 不推荐使用类适配器,推荐使用对象适配器(采用对象组合的模式)
 */
