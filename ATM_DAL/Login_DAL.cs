using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ATM_DAL
{
    /// <summary>
    /// Login data access layer
    /// </summary>
    public class Login_DAL
    {
        private string filePathCustomer = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"..\..\..\..\ATM_FILES\CustomerData.csv"));
        private string filePathAdministrator = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"..\..\..\..\ATM_FILES\AdministratorData.csv"));
        
        /// <summary>
        /// Check is Customer Exist And Valid
        /// </summary>
        /// <param name="login">string</param>
        /// <returns>string</returns>
        public string isCustomerExistAndValid(string login)
        {
            String[] data;
            StreamReader sr = new StreamReader(filePathCustomer);
            string line = String.Empty;
            while ((line = sr.ReadLine()) != null)
            {
                data = line.Split(',');

                if (decryptLogin(data[2]) == login && data[7] == "active")
                {
                    sr.Close();
                    return line; // all condition satisfied
                }
            }
            sr.Close();
            return null;
        }

        /// <summary>
        /// Check  is Administrator Exist And Valid
        /// </summary>
        /// <param name="login">string</param>
        /// <param name="pin">string</param>
        /// <returns>string</returns>
        public string isAdministratorExistAndValid(string login, string pin)
        {
            String[] data;
            StreamReader sr = new StreamReader(filePathAdministrator);
            string line = String.Empty;
            while ((line = sr.ReadLine()) != null)
            {
                data = line.Split(',');
                if (decryptLogin(data[1]) == login && decryptPin(data[2]) == pin)
                {
                    sr.Close();
                    return line;
                }
            }
            sr.Close();
            return null;
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
