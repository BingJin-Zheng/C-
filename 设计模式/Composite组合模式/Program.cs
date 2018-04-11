using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Composite组合模式
{
    class Program
    {
        static void Main(string[] args)
        {
            Component computer = new Composite();

            Component box = new Composite() { Name = "机箱",Price=300};
            Component video = new Leaf() { Name = "显示器", Price = 1500 };

            Component masterboard = new Composite() { Name = "主板", Price = 1000 };
            Component disk = new Leaf() { Name = "SSD固态硬盘", Price = 1000 };

            Component cpu = new Leaf() { Name = "CPU", Price = 1000 };
            Component videocard = new Leaf() { Name = "显卡", Price = 1200 };
            Component memory = new Leaf() { Name = "内存", Price = 500 };

            computer.Add(box);
            computer.Add(video);

            box.Add(masterboard);
            box.Add(disk);

            masterboard.Add(cpu);
            masterboard.Add(videocard);
            masterboard.Add(memory);
            //masterboard.Remove(memory);

            int total = computer.TotalPrice();

            Console.WriteLine(total);


            Console.ReadKey();
        }
    }

    //这种方式是组合模式中的透明模式,可以有不透明模式:叶子节点,不需要使用者知道
    //组合模式工作量较大,最终演化版本可以是链表,双向链表等结构

    /// <summary>
    /// 一个树的根,抽象基类
    /// </summary>
    public abstract class Component
    {
        /// <summary>
        /// 自身单价
        /// </summary>
        public int Price { get; set; }
        public string Name { get; set; }
        public abstract void Add(Component part);
        public abstract void Remove(Component part);
        /// <summary>
        /// 计算当前组件总价
        /// </summary>
        /// <returns></returns>
        public abstract int TotalPrice();

        public Component parent;
    }
    /// <summary>
    /// 叶子节点
    /// </summary>
    public class Leaf : Component
    {
        /// <summary>
        /// 叶子不需要这种方法
        /// </summary>
        /// <param name="part"></param>
        public override void Add(Component part)
        {
            throw new InvalidOperationException("叶子节点不允许该操作");
        }

        public override void Remove(Component part)
        {
            throw new InvalidOperationException("叶子节点不允许该操作");
        }

        public override int TotalPrice()
        {
            return this.Price;
        }
    }
    /// <summary>
    /// 树枝节点,主要使用的类
    /// </summary>
    public class Composite : Component
    {
        private List<Component> _childParts = new List<Component>();
        public override void Add(Component part)
        {
            part.parent = this;
            _childParts.Add(part);
        }

        public override void Remove(Component part)
        {
            _childParts.Remove(part);
        }

        public override int TotalPrice()
        {
            int total = this.Price;
            foreach (var item in _childParts)
            {
                total += item.TotalPrice();///重点,形成伪递归调用
            } 
            return total;
        }
    }


}
/*
 * 基类名字是Component 含有子类的组合模式的子类用composite表示,没有子类的用leaf表示
 * 
 * 组合模式是一个类可以派生出子类,子类再派生出子类
 * 然后有子类的类 将自己的子类 添加进自己的容器,移除出自己的容器(增删改查搜皆可)等等
 * 使用基类写一个通用方法,遍历所有子类,子类调用此通用方法,形成嵌套和伪递归调用
 * 将对象组合成树形结构表示"部分-整体"的层次结构
 */