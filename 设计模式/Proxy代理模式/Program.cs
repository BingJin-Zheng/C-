using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proxy代理模式
{
    class Program
    {
        static void Main(string[] args)
        {

            //先自己直接讨债
            债权人 收钱的 = new 直接债权人();
            收钱的.讨债();

            Console.WriteLine("--------直接讨债搞不定---------");

            //请讨债公司
            收钱的 = new 讨债公司();
            收钱的.讨债();

        }
    }
    /// <summary>
    /// 抽象接口
    /// </summary>
    public interface 债权人
    {
        //Request
        void 讨债();
    }
    /// <summary>
    /// 一个比较复杂的类
    /// </summary>
    public class 直接债权人 : 债权人
    {
        public void 讨债()
        {
            Console.WriteLine("直接债权人:欠钱的是大爷,求你还钱吧");
        }
        public void 支付讨债费用()
        {
            Console.WriteLine("直接债权人:花点小钱,能把大钱搞回来,值.");
        }
    }
    /// <summary>
    /// 代理类
    /// </summary>
    public class 讨债公司 : 债权人
    {
        private 直接债权人 客户 = new 直接债权人();
        public void 讨债()
        {
            Console.WriteLine("讨债公司:先把欠钱的揍一顿再说.");
            this.客户.讨债();
            this.客户.支付讨债费用();
            Console.WriteLine("讨债公司:搞定,收工.");
        }
    }


}
/*
 * 分为 主题(抽象基类),一个非常复杂的对象(继承抽象基类),代理也继承这个抽象基类
 * 适配器模式是新接口与老接口 调用不一致,适配类将新接口适应成为原来的调用模式
 * 外观模式是将一个复杂的子系统圈起来,开放一些简单的接口,内部子系统并不管理
 */
