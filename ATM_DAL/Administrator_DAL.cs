using ATM_BO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ATM_DAL
{
    /// <summary>
    ///  Administrator data access layer
    /// </summary>
    public class Administrator_DAL: Base_DAL
    {
        private string filePathCustomer = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"..\..\..\..\ATM_FILES\CustomerData.csv"));
        private string filePathTransaction = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"..\..\..\..\ATM_FILES\TransactionData.csv"));
        /// <summary>
        /// update account information
        /// </summary>
        /// <param name="c">Customer_BO </param>
        public void updateAccountInformation(Customer_BO c)
        {
            deleteAccount(c.Acc_Id);
            createNewAccount(c);
        }


        /// <summary>
        /// search for account
        /// </summary>
        /// <param name="c">Customer_BO</param>
        /// <returns>List<string></string></returns>
        public List<string> searchForAccount(Customer_BO c)
        {
            List<string> list = new List<string>();
            StreamReader sr = new StreamReader(filePathCustomer);
            String[] data;
            string line = String.Empty;
            while ((line = sr.ReadLine()) != null)
            {
                data = line.Split(',');
                if (areAccountIdsMatched(c.Acc_Id, int.Parse(data[0])) && areUserIdsMatched(c.User_Id, int.Parse(data[1])) &&
                    areNamesMatched(c.Name, data[4]) && areTypesMatched(c.Type, data[5]) && areBalancesMatched(c.Balance, int.Parse(data[6]))
                    && areStatusesMatched(c.Status, data[7]))
                {
                    list.Add(line);
                }
            }
            sr.Close();
            return list;
        }

        /// <summary>
        /// Check are Account Ids Matched
        /// </summary>
        /// <param name="accNo1">int</param>
        /// <param name="accNo2">int</param>
        /// <returns></returns>
        private bool areAccountIdsMatched(int accNo1, int accNo2)
        {
            if (accNo1 == -1)
            {
                return true;
            }
            else
            {
                if (accNo1 == accNo2)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        /// <summary>
        /// Check are User Ids Matched
        /// </summary>
        /// <param name="uNo1">int</param>
        /// <param name="uNo2">int</param>
        /// <returns></returns>
        private bool areUserIdsMatched(int uNo1, int uNo2)
        {
            if (uNo1 == -1)
            {
                return true;
            }
            else
            {
                if (uNo1 == uNo2)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Check are Names Matched
        /// </summary>
        /// <param name="name1">string</param>
        /// <param name="name2">string</param>
        /// <returns></returns>
        private bool areNamesMatched(string name1, string name2)
        {
            if (name1 == "")
            {
                return true;
            }
            else
            {
                if (name1 == name2)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Check are Types Matched
        /// </summary>
        /// <param name="type1">string</param>
        /// <param name="type2">string</param>
        /// <returns></returns>
        private bool areTypesMatched(string type1, string type2)
        {
            if (type1 == "")
            {
                return true;
            }
            else
            {
                if (type1 == type2)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Check are Statuses Matched
        /// </summary>
        /// <param name="status1">string</param>
        /// <param name="status2">string</param>
        /// <returns></returns>
        private bool areStatusesMatched(string status1, string status2)
        {
            if (status1 == "")
            {
                return true;
            }
            else
            {
                if (status1 == status2)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Check are Balances Matched
        /// </summary>
        /// <param name="balance1">int</param>
        /// <param name="balance2">int</param>
        /// <returns></returns>
        private bool areBalancesMatched(int balance1, int balance2)
        {
            if (balance1 == -1)
            {
                return true;
            }
            else
            {
                if (balance1 == balance2)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }


        /// <summary>
        /// search Accounts By Amount
        /// </summary>
        /// <param name="am1">int</param>
        /// <param name="am2">int</param>
        /// <returns>List<string></returns>
        public List<string> searchAccountsByAmount(int am1, int am2)
        {
            List<string> list = new List<string>();
            String[] data;
            StreamReader sr = new StreamReader(filePathCustomer);
            string line = string.Empty;
            while ((line = sr.ReadLine()) != null)
            {
                data = line.Split(',');
                if (int.Parse(data[6]) >= am1 && int.Parse(data[6]) <= am2)
                {
                    list.Add(line);
                }
            }
            sr.Close();
            return list;
        }

        /// <summary>
        /// search Accounts By Date
        /// </summary>
        /// <param name="d1">DateTime</param>
        /// <param name="d2">DateTime</param>
        /// <returns>List<string></returns>
        public List<string> searchAccountsByDate(DateTime d1, DateTime d2)
        {
            List<string> list = new List<string>();
            String[] data;
            StreamReader sr = new StreamReader(filePathTransaction);
            string line = String.Empty;
            while ((line = sr.ReadLine()) != null)
            {
                data = line.Split(',');
                if (DateTime.Parse(data[4]).Date >= d1.Date && DateTime.Parse(data[4]).Date <= d2.Date)
                {
                    list.Add(line);
                }
            }
            sr.Close();
            return list;
        }

        /// <summary>
        /// create New Account
        /// </summary>
        /// <param name="c">Customer_BO</param>
        public void createNewAccount(Customer_BO c)
        {
            string text = $"{c.Acc_Id},{c.User_Id},{encryptLogin(c.Login)},{encryptPin(c.Pin)},{c.Name},{c.Type}," +
                $"{c.Balance},{c.Status},{(c.LastWDTime).ToShortDateString()},{c.TodayWDAmount}";
            StreamWriter Writer = new StreamWriter(new FileStream(filePathCustomer, FileMode.Append, FileAccess.Write));
            Writer.WriteLine(text);
            Writer.Close();
        }

        /// <summary>
        /// get Max Acc No from Customers
        /// </summary>
        /// <returns>int</returns>
        public int getMaxAccNo()
        {
            int max = 0;
            String[] data;
            StreamReader sr = new StreamReader(filePathCustomer);
            string line = String.Empty;
            while ((line = sr.ReadLine()) != null)
            {
                data = line.Split(',');
                if (int.Parse(data[0]) > max)
                {
                    max = int.Parse(data[0]);
                }
            }
            sr.Close();
            return max;
        }
        /// <summary>
        ///  check If Login Exist or not
        /// </summary>
        /// <param name="login">string</param>
        /// <returns>bool</returns>
        public bool checkIfLoginExist(string login)
        {
            String[] data;
            StreamReader sr = new StreamReader(filePathCustomer);
            string line = String.Empty;
            while ((line = sr.ReadLine()) != null)
            {
                data = line.Split(',');
                if (data[5] == login)
                {
                    sr.Close();
                    return true;
                }
            }
            sr.Close();
            return false;
        }

        /// <summary>
        /// Check is Account Exist
        /// </summary>
        /// <param name="accNo">int</param>
        /// <returns>string</returns>
        public string isAccountExist(int accNo)
        {
            String[] data;
            StreamReader sr = new StreamReader(filePathCustomer);
            string line = String.Empty;
            while ((line = sr.ReadLine()) != null)
            {
                data = line.Split(',');
                if (int.Parse(data[0]) == accNo)
                {
                    sr.Close();
                    return line;
                }
            }
            sr.Close();
            return null;
        }
        /// <summary>
        /// delete Account
        /// </summary>
        /// <param name="accNo">int</param>
        public void deleteAccount(int accNo)
        {
            
            String[] data;
            List<String> accounts = readDataFromFile(filePathCustomer);      
            StreamWriter sw = new StreamWriter(new FileStream(filePathCustomer, FileMode.Truncate, FileAccess.Write));
            for (int i = 0; i < accounts.Count; i++)
            {
                data = accounts[i].Split(',');
                if (int.Parse(data[0]) == accNo)
                {
                    continue;
                }
                sw.WriteLine(accounts[i]);
            }
            sw.Close();
        }

        /// <summary>
        /// encrypte login
        /// </summary>
        /// <param name="login">string</param>
        /// <returns>string</returns>
        private string encryptLogin(string login)
        {
            char[] encryptedLogin = new char[login.Length];
            for (int i = 0; i < login.Length; i++)
            {
                if (login[i] >= 'a' && login[i] <= 'z')
                {
                    encryptedLogin[i] = (char)(219 - (int)login[i]);
                }
                else if (login[i] >= 'A' && login[i] <= 'Z')
                {
                    encryptedLogin[i] = (char)(155 - (int)login[i]);
                }
                else
                {
                    encryptedLogin[i] = (char)(105 - (int)login[i]);
                }
            }
            return new string(encryptedLogin);
        }
        /// <summary>
        /// encrypte pin
        /// </summary>
        /// <param name="pin">string</param>
        /// <returns>string</returns>
        private string encryptPin(string pin)
        {
            char[] encryptedPin = new char[pin.Length];
            for (int i = 0; i < pin.Length; i++)
            {
                encryptedPin[i] = (char)(105 - (int)pin[i]);
            }
            return new string(encryptedPin);
        }
    }
}
