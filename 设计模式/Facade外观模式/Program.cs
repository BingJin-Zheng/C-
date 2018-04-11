using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facade外观模式
{
    class Program
    {
        private const int _amount = 12000;
        static void Main(string[] args)
        {
            //具体的操作如你打10086,10086这个号码就是外观
            //facade模式注重简化接口
            //Adapter模式注重转换接口
            //bridge模式注重分离接口(抽象)与实现
            //decotator模式注重稳定接口的前提下为对象扩展功能
            #region 没有使用外观模式时的复杂程度
            Bank bank = new Bank();
            Loan loan = new Loan();
            Credit credit = new Credit();

            Customer customer = new Customer("Ann McKinsey");

            bool eligible = true;

            if (!bank.HasSufficientSavings(customer, _amount))
            {
                eligible = false;
            }
            else if (!loan.HasNoBadLoans(customer))
            {
                eligible = false;
            }
            else if (!credit.HasGoodCredit(customer))
            {
                eligible = false;
            }

            Console.WriteLine("\n" + customer.Name + " has been " + (eligible ? "Approved" : "Rejected"));
            #endregion

            Facade();

            Console.ReadLine();
        }

        public static void Facade()
        {
            Mortgage mortgage = new Mortgage();
            Customer customer = new Customer("Ann McKinsey");
            bool eligable = mortgage.IsEligible(customer, 125000);

            Console.WriteLine("\n" + customer.Name +
                " has been " + (eligable ? "Approved" : "Rejected"));
        }
    }
    /// <summary>
    /// 顾客类,纯数据类
    /// </summary>
    public class Customer
    {
        private string _name;

        public Customer(string name)
        {
            this._name = name;
        }

        public string Name
        {
            get { return _name; }
        }
    }
    /// <summary>
    ///  银行子系统
    /// </summary>
    public class Bank
    {
        public bool HasSufficientSavings(Customer c, int amount)
        {
            Console.WriteLine("Check bank for " + c.Name);
            return true;
        }
    }

    /// <summary>
    /// 信用子系统
    /// </summary>
    public class Credit
    {
        public bool HasGoodCredit(Customer c)
        {
            Console.WriteLine("Check credit for " + c.Name);
            return true;
        }
    }

    /// <summary>
    /// 贷款子系统
    /// </summary>
    public class Loan
    {
        public bool HasNoBadLoans(Customer c)
        {
            Console.WriteLine("Check loans for " + c.Name);
            return true;
        }
    }
    /// <summary>
    /// 外观类
    /// </summary>
    public class Mortgage
    {
        private Bank bank = new Bank();
        private Loan loan = new Loan();
        private Credit credit = new Credit();

        public bool IsEligible(Customer cust, int amount)
        {
            Console.WriteLine("{0} applies for {1:C} loan\n",
              cust.Name, amount);

            bool eligible = true;

            if (!bank.HasSufficientSavings(cust, amount))
            {
                eligible = false;
            }
            else if (!loan.HasNoBadLoans(cust))
            {
                eligible = false;
            }
            else if (!credit.HasGoodCredit(cust))
            {
                eligible = false;
            }

            return eligible;
        }
    }
}


/*
 * 一个客户端与多个子系统进行交互,当子系统增加时,客户端不可避免的要多出很多操作
 * 简化一个客户端与多个子系统的交互,变成一对一交互 
 * 1-多
 * 变成
 * 1-1-多 中间的1的功能是包装多个子系统,不对子系统进行其他操作,只进行包装操作
 * 
 *  从客户程序的角度来看，Facade模式不仅简化了整个组件系统的接口，
 *  同时对于组件内部与外部客户程序来说，从某种程度上也达到了一种“解耦”的效果----
 *  内部子系统的任何变化不会影响到Facade接口的变化。
 *  Facade设计模式更注重从架构的层次去看整个系统，而不是单个类的层次。
 *  Facdae很多时候更是一种架构设计模式。
 * 
 */
