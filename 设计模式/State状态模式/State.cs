using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace State状态模式
{
    //状态
    public class State
    {
        public abstract void work();
    }
    
    public class GoodState : State
    {
        public override void work()
        {
            Console.WriteLine("身体状态很好,工作24小时不停电");
        }
    }
    public class BadState : State
    {
        public override void work()
        {
            Console.WriteLine("身体状态很差,工作2小时效率低下");
        }
    }

    //上下文
    public class Teacher
    {
        private Dictionary<int, State> dictState = new Dictionary<int, State>();
        public Teacher()
        {
            dictState.Add(1, new GoodState());
            dictState.Add(2, new BadState());
        }
        //Request()
        public void Teach(int id)
        {
            dictState[id].work();
        }
    }


}
/*
 * 一个对象的行为取决于它的状态, 并且它必须在运行时刻根据状态改变它的行为。 
 * 一个操作中含有庞大的多分支的条件语句，且这些分支依赖于该对象的状态。这个状态通常用一个或多个枚举常量表示。通常, 有多个操作包含这一相同的条件结构。State模式将每一个条件分支放入一个独立的类中。这使得你可以根据对象自身的情况将对象的状态作为一个对象，这一对象可以不依赖于其他对象而独立变化。

 */
