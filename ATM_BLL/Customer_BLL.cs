using ATM_BO;
using ATM_DAL;
using System;
using System.Collections.Generic;
using System.Text;

namespace ATM_BLL
{
    
    /// <summary>
    /// Customer business logic layer
    /// </summary>
    public class Customer_BLL
    {
      
        private Customer_DAL dal = new Customer_DAL();

        /// <summary>
        /// with draw cash
        /// </summary>
        /// <param name="c">Customer_BO</param>
        /// <param name="amount">int</param>
        /// <param name="transactionType">string</param>
        /// <returns>(Customer_BO, int)</returns>
        public (Customer_BO, int) withDrawCash(Customer_BO c, int amount, string transactionType)
        {
            int result = isWithdDrawValid(c, amount);
            if (result == 0)
            {
                c.Balance = c.Balance - amount;
                DateTime date = new DateTime();
                date = DateTime.Now;
                TimeSpan timeDiff = date.Subtract(c.LastWDTime);
                if (timeDiff.Days > 0)
                {
                    c.TodayWDAmount = amount;
                }
                else
                {
                    c.TodayWDAmount = amount + c.TodayWDAmount;
                }
                dal.storeTranaction($"{transactionType},{c.User_Id},{c.Name},{amount},{c.LastWDTime.ToShortDateString()},{-1},{""}");
                c.LastWDTime = date;
                return (stringTOObject(dal.updateAccount(c)), result);
            }
            return (c, result);
        }

        /// <summary>
        /// deposite cash
        /// </summary>
        /// <param name="c">Customer_BO</param>
        /// <param name="amount">int</param>
        /// <returns>Customer_BO</returns>
        public Customer_BO depositeCash(Customer_BO c, int amount)
        {
            c.Balance = c.Balance + amount;
            dal.storeTranaction($"{"Cash Deposite"},{c.User_Id},{c.Name},{amount},{(DateTime.Today).ToShortDateString()},{-1},{""}");
            return stringTOObject(dal.updateAccount(c));

        }

        /// <summary>
        /// Check is with draw valid
        /// </summary>
        /// <param name="c">Customer_BO</param>
        /// <param name="amount">int</param>
        /// <returns>int</returns>
        private int isWithdDrawValid(Customer_BO c, int amount)
        {
            DateTime date = new DateTime();
            date = DateTime.Now;
            TimeSpan time = date.Subtract(c.LastWDTime);
            if (amount > c.Balance)
            {
                return -2;
            }
            if (time.Days > 0)
            {
                Console.WriteLine(time.Days);
                c.TodayWDAmount = 0;
                if (amount <= 20000)
                {
                    return 0;
                }
                else
                {
                    return -1;
                }
            }
            else
            {
                if (amount + c.TodayWDAmount <= 20000)
                {
                    return 0;
                }
                else
                {
                    return -1;
                }
            }
        }
        /// <summary>
        /// Convert string to Customer_BO
        /// </summary>
        /// <param name="text">text</param>
        /// <returns>Customer_BO</returns>
        private Customer_BO stringTOObject(string text)
        {
            if (text == null)
            {
                return null;
            }
            string[] data = text.Split(',');
            Customer_BO c = new Customer_BO
            {
                Acc_Id = int.Parse(data[0]),
                User_Id = int.Parse(data[1]),
                Login = decryptLogin(data[2]),
                Pin = decryptPin(data[3]),
                Name = data[4],
                Type = data[5],
                Balance = int.Parse(data[6]),
                Status = data[7],
                LastWDTime = DateTime.Parse(data[8]),
                TodayWDAmount = int.Parse(data[9])
            };
            return c;
        }

        /// <summary>
        /// Check is Account exist
        /// </summary>
        /// <param name="accNo">int</param>
        /// <returns>Customer_BO</returns>
        public Customer_BO isAccountExist(int accNo)
        {
            return stringTOObject(dal.isAccountExist(accNo));
        }

        public Customer_BO cashTransfer(Customer_BO c1, Customer_BO c2, int amount)
        {
            dal.storeTranaction($"{"Cash Transfer"},{c1.User_Id},{c1.Name},{amount},{(DateTime.Today).ToShortDateString()},{c2.User_Id},{c2.Name}");
            dal.updateAccount(c2);
            return stringTOObject(dal.updateAccount(c1));
        }

        /// <summary>
        /// decrypte login
        /// </summary>
        /// <param name="login">string</param>
        /// <returns>string</returns>
        private string decryptLogin(string login)
        {
            char[] decryptedLogin = new char[login.Length];
            for (int i = 0; i < login.Length; i++)
            {
                if (login[i] >= 'a' && login[i] <= 'z')
                {
                    decryptedLogin[i] = (char)(219 - (int)login[i]);
                }
                else if (login[i] >= 'A' && login[i] <= 'Z')
                {
                    decryptedLogin[i] = (char)(155 - (int)login[i]);
                }
                else
                {
                    decryptedLogin[i] = (char)(105 - (int)login[i]);
                }
            }
            return new string(decryptedLogin);
        }
       
        /// <summary>
        /// decrypte pin
        /// </summary>
        /// <param name="pin">string</param>
        /// <returns>string</returns>
        private string decryptPin(string pin)
        {
            char[] decryptedPin = new char[pin.Length];
            for (int i = 0; i < pin.Length; i++)
            {
                decryptedPin[i] = (char)(105 - (int)pin[i]);
            }
            return new string(decryptedPin);
        }
    }
}
