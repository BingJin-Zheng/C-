using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 建造者模式
{
   
    class Program
    {
        static void Main(string[] args)
        {
            //如何使用这个设计模式

            Director d = new Director();
            House h = d.BuilderHomeHouse();
            Console.WriteLine(h.Base + "\n"+h.Wall+"\n"+h.Roof);

            House h1 = d.BuilderGoodHomeHouse();
            Console.WriteLine(h1.Base + "\n" + h1.Wall + "\n" + h1.Roof);
            
            Console.ReadKey();
        }
    }
    /// <summary>
    /// 导演/管理类,包工头,一般情况下不怎么变,增加/删除需要变化
    /// </summary>
    public class Director
    {
        /// <summary>
        /// 与产品,工程队产生聚合关系
        /// </summary>
        private List<Builder> homeBuilders = new List<Builder>();
        /// <summary>
        /// 与产品,工程队产生聚合关系
        /// </summary>
        private Dictionary<string, Builder> goodHomeBuilders = new Dictionary<string, Builder>();
        /// <summary>
        /// 构造方法,一般情况下,这个建造顺序是不动的
        /// </summary>
        public Director()
        {
            this.homeBuilders.Add(new RoomBaseBuilder());
            this.homeBuilders.Add(new RoomWallBuilder());
            this.homeBuilders.Add(new RoomRoofBuilder());

            this.goodHomeBuilders.Add("Base", new GoodHouseBaseBuilder());
            this.goodHomeBuilders.Add("Wall", new GoodHouseWallBuilder());
            this.goodHomeBuilders.Add("Roof", new GoodHouseRoofBuilder());

        }

        public House BuilderHomeHouse()
        {
            House homeHouse = new House();
            foreach (Builder item in homeBuilders)
            {
                item.BuilderPart(homeHouse);
            }
            return homeHouse;
        }

        public House BuilderGoodHomeHouse()
        {
            House homeHouse = new House();
            Builder baseBuilder = this.goodHomeBuilders["Base"];
            Builder wallBuilder = this.goodHomeBuilders["Wall"];
            Builder roofBuilder = this.goodHomeBuilders["Roof"];
            //先造地基,然后墙体,最后房顶
            baseBuilder.BuilderPart(homeHouse);
            wallBuilder.BuilderPart(homeHouse);
            roofBuilder.BuilderPart(homeHouse);

            return homeHouse;
        }
    }

    /// <summary>
    /// 房子,一个产品
    /// </summary>
    public class House
    {
        /// <summary>
        /// 墙体
        /// </summary>
        public string Wall;
        /// <summary>
        /// 屋顶
        /// </summary>
        public string Roof;
        /// <summary>
        /// 地基
        /// </summary>
        public string Base;
    }
    
    /// <summary>
    /// 虚基类
    /// </summary>
    public abstract class Builder
    {
        public abstract void BuilderPart(House house);
    }


    /// <summary>
    /// 普通房子的建筑队-地基
    /// </summary>
    public class RoomBaseBuilder : Builder
    {
        public override void BuilderPart(House house)
        {
            house.Base = "普通房子的地基";
        }
    }
    /// <summary>
    /// 普通房子的建筑队-墙体
    /// </summary>
    public class RoomWallBuilder : Builder
    {
        public override void BuilderPart(House house)
        {
            house.Wall = "普通房子的墙体";
        }
    }
    /// <summary>
    /// 普通房子的建筑队-屋顶
    /// </summary>
    public class RoomRoofBuilder : Builder
    {
        public override void BuilderPart(House house)
        {
            house.Roof = "普通房子的屋顶";
        }
    }

    /// <summary>
    /// 别墅房子的建筑队-地基
    /// </summary>
    public class GoodHouseBaseBuilder : Builder
    {
        public override void BuilderPart(House house)
        {
            house.Base = "别墅房子的地基";
        }
    }
    /// <summary>
    /// 别墅房子的建筑队-墙体
    /// </summary>
    public class GoodHouseWallBuilder : Builder
    {
        public override void BuilderPart(House house)
        {
            house.Wall = "别墅房子的墙体";
        }
    }
    /// <summary>
    /// 别墅房子的建筑队-屋顶
    /// </summary>
    public class GoodHouseRoofBuilder : Builder
    {
        public override void BuilderPart(House house)
        {
            house.Roof = "别墅房子的屋顶";
        }
    }
}
/*
 * 建造者模式的结构: 一个管理类,一个基类有多个子类,子类经常性变化,一个产品类
 * 但是 管理类使用的方式 这个基类组合多个子类 这样的结构很稳定
 * 多态全交给子类了.当你将这几种类放在很多个文件下,并演化出很多子类,就变得令人很惊叹了
 * 还有更多组合,将某种功能组合成为一个类,比如将管理类的指责给基类,产品类的功能给基类等,但不建议这么做
 * 1:一个导演(管理/主管)类,使用者需要使用的类
 * 2:一个建造者基类,一些具体的实现由子类承担,承担里氏替换原则的作用,这个类是纯虚类或者不是纯虚类都可以
 * 3:具体实现功能的子类,子类多变,需要添加修改删除等等
 * 
 * 例子:建房子,房子是输出品,包工头使用工程队建造房子,工程队下面有多个建造不同房子部分的子工程队
 * */
