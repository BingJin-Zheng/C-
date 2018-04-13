using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediator中介者模式
{
    class Program
    {
        static void Main(string[] args)
        {

            联合国机构 mediator = new 安理会();//调停者mediator
            国家 usa = new 美国(mediator);//colleague
            国家 iraq = new 伊拉克(mediator);//colleague

            usa.发表声明("禁止研制核武器和大规模杀伤武器,否则就开打!");//不直接与iraq耦合

            iraq.发表声明("严正声明:本国没有研制任何核武器!");//不直接与usa耦合

            iraq.发表声明("别欺国太甚,否则本国会悍然来次恐怖活动!");

            mediator.开除成员国(iraq);//iraq不听话,开除之

            usa.发表声明("再次声明:禁止研制核武器和大规模杀伤武器,否则就开打!");//iraq再也接收不到了

            Console.ReadLine();
        }
    }
    //Mediator
    public abstract class 联合国机构
    {
        protected System.Collections.Generic.List<国家> 成员国 = new List<国家>();
        public void 接收新成员国(国家 colleague)
        {
            if (!this.成员国.Contains(colleague))
            {
                this.成员国.Add(colleague);
            }
        }
        public void 开除成员国(国家 colleague)
        {
            if (this.成员国.Contains(colleague))
            {
                this.成员国.Remove(colleague);
                Console.WriteLine("<{0}>:开除成员国:{1}", this, colleague);
            }
        }
        public abstract void 发表声明(string 内容, 国家 发表者);
    }

    //ConcreteMediator
    public class 安理会 : 联合国机构
    {
        public override void 发表声明(string 内容, 国家 发表者)
        {
            foreach (国家 colleague in base.成员国)
            {
                if (colleague != 发表者)
                {
                    colleague.接收声明(内容, 发表者);
                }
            }
        }
    }
    //Colleague
    public abstract class 国家
    {
        protected 联合国机构 _协调人;
        public 国家(联合国机构 mediator)
        {
            this._协调人 = mediator;
            mediator.接收新成员国(this);
            Console.WriteLine("{0}在{1}加入联合国", this.ToString(), DateTime.Now);
        }
        public virtual void 发表声明(string 内容)
        {
            Console.WriteLine("<{0}>发表声明:\r\n\t{1}", this, 内容);
            this._协调人.发表声明(内容, this);
        }
        public virtual void 接收声明(string 内容, 国家 发表者)
        {
            Console.WriteLine("{0}收到<{1}>发表的声明:\r\n\t{2}\t时间:{3}", this, 发表者, 内容, DateTime.Now);
        }
    }
    public class 美国 : 国家
    {
        public 美国(联合国机构 meidator)
            : base(meidator)
        {

        }
        public override void 接收声明(string 内容, 国家 发表者)
        {
            base.接收声明(内容, 发表者);
            if (内容.Contains("核武器"))
            {
                Console.WriteLine("!!!!警报:FBI开始对{0}进行全程监控", 发表者);
            }
        }
    }
    public class 伊拉克 : 国家
    {
        public 伊拉克(联合国机构 meidator)
            : base(meidator)
        { }
        public override void 接收声明(string 内容, 国家 发表者)
        {
            base.接收声明(内容, 发表者);
            if (内容.Contains("核武器"))
            {
                Console.WriteLine("<{0}>说:额是冤枉的,{1}不要诬陷好人", this, 发表者);
            }
        }
        public override void 发表声明(string 内容)
        {
            if (内容.Contains("恐怖活动"))
            {
                Console.WriteLine("<{0}>:******低调,低调,低调*******", this);
            }
            base.发表声明(内容);
        }
    }
}
/*
 * 对象之间常常会维持一种复杂的引用关系,我们可使用一个“中介对象”来管理对象间的关联关系，避免相互交互的对象之间的紧耦合引用关系，从而更好地抵御变化。
 * Façade模式是解耦系统外到系统内（单向）的对象关联关系；Mediator模式是解耦系统内各个对象之间（双向）的关联关系。

 */
