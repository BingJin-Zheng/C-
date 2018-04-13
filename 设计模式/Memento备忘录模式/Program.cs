using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memento备忘录模式
{
    class Program
    {
        static void Main(string[] args)
        {
            游戏 game = new 游戏();
            存取档工具 sltool = new 存取档工具();
            游戏进度 progress = game.存档();

            sltool.存档("000", progress);
            game.查看状态();//查询初始状态

            Console.WriteLine("\n第一次战斗\n");
            game.战斗();//第一次战斗
            game.查看状态();
            progress = game.存档();
            sltool.存档("001", progress);

            Console.WriteLine("\n第二次战斗\n");
            game.战斗();//第二次战斗
            game.查看状态();
            progress = game.存档();
            sltool.存档("002", progress);
            Console.WriteLine("\n读档:000\n");
            progress = sltool.读档("000");
            game.读档(progress);
            game.查看状态();

            Console.WriteLine("\n读档:001\n");
            progress = sltool.读档("001");
            game.读档(progress);
            game.查看状态();

            Console.WriteLine("\n读档:002\n");
            progress = sltool.读档("002");
            game.读档(progress);
            game.查看状态();

        }
    }

    //Memento
    public class 游戏进度
    {
        private int _attack;
        private int _life;
        private int _defense;
        private DateTime _createtime;

        public 游戏进度(int 攻击, int 生命, int 防御)
        {
            this._attack = 攻击;
            this._life = 生命;
            this._defense = 防御;
            this._createtime = DateTime.Now;
        }
        public int 攻击力
        {
            get { return this._attack; }
        }
        public int 生命力
        {
            get { return this._life; }
        }
        public int 防御力
        {
            get { return this._defense; }
        }
        public DateTime 存档日期
        {
            get { return this._createtime; }
        }
        public override string ToString()
        {
            return string.Format("{0}:生命:{1},攻击:{2},防御:{3}", this._createtime, this._life, this._attack, this._defense);
        }
    }


    public class 游戏
    {
        private int _attack;
        private int _life;
        private int _defense;
        public 游戏()
        {
            this._attack = 100;
            this._life = 100;
            this._defense = 100;
            Console.WriteLine("游戏开始于:{0}.", DateTime.Now);
        }
        public int 攻击力
        {
            get { return this._attack; }
        }
        public int 生命力
        {
            get { return this._life; }
        }
        public int 防御力
        {
            get { return this._defense; }
        }
        public void 查看状态()
        {
            Console.WriteLine("当前生命力:{0},攻击力:{1},防御力:{2},游戏是否结束:{3}", this._life, this._attack, this._defense, this.游戏结束);
        }
        public void 战斗()
        {
            Console.WriteLine("\t开始战斗.");
            System.Threading.Thread.Sleep(3000);
            int lifeless = new Random().Next(200);
            this._life = this._life - lifeless;
            int attackless = new Random().Next(100);
            this._attack = this._attack - attackless;
            int defenseless = new Random().Next(100);
            this._defense = this._defense - defenseless;
            Console.WriteLine("本回合战斗结束,损失:生命:{0},攻击:{1},防御:{2}", lifeless, attackless, defenseless);
        }
        public bool 游戏结束
        {
            get { return this._life <= 0; }
        }

        public 游戏进度 存档()
        {
            return new 游戏进度(this._attack, this._life, this._defense);
        }
        public void 读档(游戏进度 saved)
        {
            this._life = saved.生命力;
            this._attack = saved.攻击力;
            this._defense = saved.防御力;
            Console.WriteLine("读档结束:{0}", saved);
        }
    }

    //CareTaker
    public class 存取档工具
    {
        private System.Collections.Hashtable saves
            = new System.Collections.Hashtable();

        public void 存档(string 进度编号, 游戏进度 progress)
        {
            if (!this.saves.ContainsKey(进度编号))
            {
                this.saves.Add(进度编号, progress);
            }
            else
            {
                this.saves[进度编号] = progress;
            }
            Console.WriteLine("进度已保存,进度编号:{0},进度信息:{1}", 进度编号, progress);
        }
        public 游戏进度 读档(string 进度编号)
        {
            return this.saves[进度编号] as 游戏进度;
        }
    }

}
/*
 * 
 */
