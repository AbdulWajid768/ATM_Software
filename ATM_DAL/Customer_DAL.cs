using ATM_BO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ATM_DAL
{
    /// <summary>
    /// Cusomer data access layer
    /// </summary>
    public class Customer_DAL: Base_DAL
    {
        private string filePathCustomer = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"..\..\..\..\ATM_FILES\CustomerData.csv"));
        private string filePathTransaction = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"..\..\..\..\ATM_FILES\TransactionData.csv"));

        /// <summary>
        /// store Tranaction
        /// </summary>
        /// <param name="trasaction">string</param>
        public void storeTranaction(string trasaction)
        {
            StreamWriter sw = new StreamWriter(new FileStream(filePathTransaction, FileMode.Append, FileAccess.Write));
            sw.WriteLine(trasaction);
            sw.Close();
        }
        /// <summary>
        /// update Account
        /// </summary>
        /// <param name="c">Customer_BO</param>
        /// <returns>string</returns>
        public string updateAccount(Customer_BO c)
        {
            StreamReader sr = new StreamReader(filePathCustomer);
            String[] data;
            string line = String.Empty;
            String dataToRet = "";
            List<String> accounts = new List<string>();
            while ((line = sr.ReadLine()) != null)
            {
                data = line.Split(',');
                if (int.Parse(data[0]) == c.Acc_Id)
                {
                    line = $"{c.Acc_Id},{c.User_Id},{encryptLogin(c.Login)},{encryptPin(c.Pin)},{c.Name},{c.Type},{c.Balance},{c.Status},{c.LastWDTime.ToShortDateString()},{c.TodayWDAmount}";
                    dataToRet = line;
                }
                accounts.Add(line);
            }
            sr.Close();
            writeDataToFile(filePathCustomer, accounts);
            return dataToRet;
        }

        /// <summary>
        /// encrypte pin
        /// </summary>
        /// <param name="pin">string</param>
        /// <returns>string</returns>
        public string isAccountExist(int accNo)
        {
            String[] data;
            StreamReader sr = new StreamReader(filePathCustomer);
            string line = String.Empty;
            while ((line = sr.ReadLine()) != null)
            {
                data = line.Split(',');
                if (int.Parse(data[0]) == accNo && data[7] == "active")
                {
                    sr.Close();
                    return line;
                }
            }
            sr.Close();
            return null;
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
