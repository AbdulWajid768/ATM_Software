using ATM_BO;
using ATM_DAL;
using System;
using System.Collections.Generic;
using System.Text;

namespace ATM_BLL
{
    /// <summary>
    /// Administrator Business Logic Layer
    /// </summary>
    public class Administrator_BLL
    {
        private Administrator_DAL dal = new Administrator_DAL();
        /// <summary>
        /// It creates a new Account
        /// </summary>
        /// <param name="c">Customer_BO</param>
        /// <returns>True on Success, False on Failure</returns>
        public (bool, int) createNewAccount(Customer_BO c)
        {
            bool loginExist = checkIfLoginExist(c.Login);
            if (loginExist)
            {
                return (false, -1);
            }
            int accNo = getMaxAccNo() + 1;
            c.Acc_Id = accNo;
            c.User_Id = accNo * 100;
            c.TodayWDAmount = 0;
            c.LastWDTime = DateTime.Now;
            dal.createNewAccount(c);
            return (true, accNo);
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
        /// Convert string to Transaction_BO
        /// </summary>
        /// <param name="text">text</param>
        /// <returns>Transaction_BO</returns>
        private Transaction_BO stringTOTransactionObject(string text)
        {
            if (text == null)
            {
                return null;
            }
            string[] data = text.Split(',');
            Transaction_BO t = new Transaction_BO
            {
                transactionType = data[0],
                doerUserId = int.Parse(data[1]),
                doerName = data[2],
                amount = int.Parse(data[3]),
                date = DateTime.Parse(data[4]),
                receiverUserId = int.Parse(data[5]),
                receiverName = data[6]

            };
            return t;
        }
        /// <summary>
        /// Update Account Information
        /// </summary>
        /// <param name="c">Customer_BO</param>
        public void updateAccountInformation(Customer_BO c)
        {
            dal.updateAccountInformation(c);
        }

        /// <summary>
        /// Return Maximum Available account number
        /// </summary>
        /// <returns>Max Account Number</returns>
        private int getMaxAccNo()
        {
            return dal.getMaxAccNo();
        }

        /// <summary>
        /// Check whether login exist or not
        /// </summary>
        /// <param name="login">string login</param>
        /// <returns>true on success, false on failure</returns>
        private bool checkIfLoginExist(string login)
        {
            return dal.checkIfLoginExist(login);
        }

        /// <summary>
        /// return Customer_BO if account exist
        /// </summary>
        /// <param name="accNo">int</param>
        /// <returns>Customer_BO on success, null on failure</returns>
        public Customer_BO isAccountExist(int accNo)
        {
            return stringTOCustomerObject(dal.isAccountExist(accNo));
        }

        /// <summary>
        /// Delete Account
        /// </summary>
        /// <param name="accNo">int</param>
        public void deleteAccount(int accNo)
        {
            dal.deleteAccount(accNo);
        }

        /// <summary>
        /// Search for particular Account
        /// </summary>
        /// <param name="c">Customer_BO</param>
        /// <returns>List<Customer_BO></returns>
        public List<Customer_BO> searchForAccount(Customer_BO c)
        {
            return stringListToCustomerObjectList(dal.searchForAccount(c));

        }

        /// <summary>
        /// convert string list to customer object list
        /// </summary>
        /// <param name="list">List<string></param>
        /// <returns>List<Customer_BO></returns>
        private List<Customer_BO> stringListToCustomerObjectList(List<string> list)
        {
            List<Customer_BO> objs = new List<Customer_BO>();
            for (int i = 0; i < list.Count; i++)
            {
                objs.Add(stringTOCustomerObject(list[i]));
            }
            return objs;
        }
        /// <summary>
        /// convert string list to Transaction object list
        /// </summary>
        /// <param name="list">List<string></param>
        /// <returns>List<Transaction_BO></returns>
        private List<Transaction_BO> stringListToTransactionObjectList(List<string> list)
        {
            List<Transaction_BO> objs = new List<Transaction_BO>();
            for (int i = 0; i < list.Count; i++)
            {
                objs.Add(stringTOTransactionObject(list[i]));
            }
            return objs;
        }


        /// <summary>
        /// search account between max and min account balance
        /// </summary>
        /// <param name="am1">int</param>
        /// <param name="am2">int</param>
        /// <returns>List<Customer_BO></returns>
        public List<Customer_BO> searchAccountsByAmount(int am1, int am2)
        {
            return stringListToCustomerObjectList(dal.searchAccountsByAmount(am1, am2));
        }

        /// <summary>
        /// Search transaction done between particular dates
        /// </summary>
        /// <param name="d1">DateTime</param>
        /// <param name="d2">DateTime</param>
        /// <returns>List<Transaction_BO></returns>
        public List<Transaction_BO> searchAccountsByDate(DateTime d1, DateTime d2)
        {
            return stringListToTransactionObjectList(dal.searchAccountsByDate(d1, d2));
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
