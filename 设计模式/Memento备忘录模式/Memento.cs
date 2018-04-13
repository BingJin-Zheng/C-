using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Memento备忘录模式
{
    /// <summary>
    /// 备忘录,仅仅封装数据
    /// </summary>
    public class Memento
    {
        public int life;

    }
    /// <summary>
    /// 原生类
    /// </summary>
    public interface IOriginator
    {
        void Load(Memento mem);
        Memento Save();
    }

    public class Game : IOriginator
    {
        private int _Life = 100;
        public void Fight()
        {
            System.Threading.Thread.Sleep(2000);
            this._Life -= new Random().Next(100);
        }
        public void Load(Memento mem)
        {
            this._Life = mem.life;
        }

        public Memento Save()
        {
            return new Memento() { life = this._Life };
        }
        public override string ToString()
        {
            return "游戏";
        }
    }
    /// <summary>
    /// 存取备忘录
    /// </summary>
    public class CareTaker
    {
        private Dictionary<int, Memento> dictTaker = new Dictionary<int, Memento>();

        public void SaveMemento(int ID,Memento mem)
        {
            dictTaker.Add(ID, mem);
        }
        public Memento Load(int ID)
        {
            return dictTaker[ID];
        }
    }

}