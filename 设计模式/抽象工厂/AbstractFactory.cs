using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 抽象工厂
{
    /// <summary>
    /// 一个工厂基类,多种产品基类,一个工厂下面有多个工厂子类,多种产品基类,根据多个工厂子类,生产具体的产品
    /// 一个工厂子类 表示 多种产品的组合品牌.例如:IBM电脑是一个品牌,IBM电脑由电池,屏幕,CPU等多种产品组成
    /// 戴尔电脑是一个品牌,戴尔电脑由电池,屏幕,CPU等多种产品组成,如下图关系:(注:...表示可扩展性)
    /// 
    /// 系列\产品    电池  屏幕   CPU ...
    ///  IBM电脑       1    2      3
    ///  戴尔电脑      4    5      6  
    ///  ...
    ///  
    /// 当整体框架搭建完成之后,作用在于当动态切换某一个系列/完成某个一个功能时,只需要使用甚少的代码即可完成任务
    /// 增加减少系列非常简单,但是增加减少产品,则会造成修改困难
    /// </summary>
    class AbstractFactory
    {

    }




    /// <summary>
    /// 抽象工厂,负责规定创建产品(多种)
    /// </summary>
    public abstract class SportsShop
    {
        /// <summary>
        /// 创建产品A
        /// </summary>
        /// <returns></returns>
        public abstract Shoes SellShoes();

        /// <summary>
        /// 创建产品B
        /// </summary>
        /// <returns></returns>
        public abstract Hat SellHat();
    }
    /// <summary>
    /// 抽象工厂子类,Nick系列,生产具体的产品
    /// </summary>
    public class NickShop : SportsShop
    {
        public override Hat SellHat()
        {
            return new NickHat();
        }

        public override Shoes SellShoes()
        {
            return new NickShoes();
        }
    }
    /// <summary>
    /// 抽象工厂子类,LiNing系列,生产具体的产品
    /// </summary>
    public class LiNingShop : SportsShop
    {
        public override Hat SellHat()
        {
            return new LiNingHat();
        }

        public override Shoes SellShoes()
        {
            return new LiNingShoes();
        }
    }

    /// <summary>
    /// 抽象产品A
    /// </summary>
    public abstract class Shoes
    {

    }
    /// <summary>
    /// 抽象产品B
    /// </summary>
    public abstract class Hat
    {

    }

    /// <summary>
    /// Nick系列产品A
    /// </summary>
    public class NickShoes : Shoes
    {
        public override string ToString()
        {
            return "耐克牌子的鞋子";
        }
    }
    /// <summary>
    /// LiNing系列产品A
    /// </summary>
    public class LiNingShoes : Shoes
    {
        public override string ToString()
        {
            return "李宁牌子的鞋子";
        }
    }
    /// <summary>
    /// Nick系列产品B
    /// </summary>
    public class NickHat : Hat
    {
        public override string ToString()
        {
            return "耐克牌子的帽子";
        }
    }
    /// <summary>
    /// LiNing系列产品B
    /// </summary>
    public class LiNingHat : Hat
    {
        public override string ToString()
        {
            return "李宁牌子的帽子";
        }
    }
}
