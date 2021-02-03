using ATM_BO;
using ATM_DAL;
using System;
using System.Collections.Generic;
using System.Text;

namespace ATM_BLL
{
    /// <summary>
    /// Login business logic layer
    /// </summary>
    public class Login_BLL
    { 
        private Login_DAL dal = new Login_DAL();
        private Customer_DAL cusDAL= new Customer_DAL();
        /// <summary>
        /// Check is Customer Exist And Valid
        /// </summary>
        /// <param name="login">string</param>
        /// <returns>Customer_BO</returns>
        public Customer_BO isCustomerExistAndValid(string login)
        {
            return stringTOCustomerObject(dal.isCustomerExistAndValid(login));
        }

        /// <summary>
        /// Check is Administrator Exist And Valid
        /// </summary>
        /// <param name="login">string</param>
        /// <param name="pin">string</param>
        /// <returns>Administrator_BO</returns>
        public Administrator_BO isAdministratorExistAndValid(string login, string pin)
        {
            return stringTOAdministratorObject(dal.isAdministratorExistAndValid(login, pin));
        }

        /// <summary>
        /// disable Account
        /// </summary>
        /// <param name="c">Customer_BO</param>
        public void disableAccount(Customer_BO c)
        {
            c.Status = "disabled";
            cusDAL.updateAccount(c);
        }

        /// <summary>
        /// Convert string to Customer_BO
        /// </summary>
        /// <param name="text">text</param>
        /// <returns>Customer_BO</returns>
        private Customer_BO stringTOCustomerObject(string text)
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
        /// Convert string to Administrator_BO
        /// </summary>
        /// <param name="text">text</param>
        /// <returns>Administrator_BO</returns>
        private Administrator_BO stringTOAdministratorObject(string text)
        {
            if (text == null)
            {
                return null;
            }
            string[] data = text.Split(',');
            Administrator_BO a = new Administrator_BO
            {
                Id = int.Parse(data[0]),
                Login = data[1],
                Pin = data[2]
            };
            return a;
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
