using ATM_BLL;
using ATM_BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace ATM_PL
{
    /// <summary>
    /// Login presentation layer
    /// </summary>
    public class Login_PL
    {
        private Login_BLL bll = new Login_BLL();
        private Customer_PL cusPL = new Customer_PL();
        private Administrator_PL admPL = new Administrator_PL();
        /// <summary>
        /// Login Menu
        /// </summary>
        public void LoginMenu()
        {
            string option;
            while (true)
            {
                Console.WriteLine("Who are you?");
                Console.WriteLine("Enter 1----If you are Administrator");
                Console.WriteLine("Enter 2----If you are Customer");
                Console.WriteLine("Enter 3----Exit");
                Console.Write("Enter (1/2/3): ");
                option = Console.ReadLine();
                if (option == "1" || option == "2" || option == "3")
                {
                    switch (option)
                    {
                        case "1":
                            administratorLogin();
                            break;
                        case "2":
                            customerLogin();
                            break;
                        case "3":
                            exitFromSystem();
                            break;
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Error: Invalid Input");
                    Console.WriteLine("Please read the following menu carefully and try again.");
                }
            }
        }

        /// <summary>
        /// administrator Login
        /// </summary>
        private void administratorLogin()
        {
            String login = inputLogin("Login: ");
            String pin;
            pin= inputPin("Pin: ");
            Administrator_BO adm = bll.isAdministratorExistAndValid(login, pin);
            if (adm != null)
            {
                Console.Clear();
                Console.WriteLine("=>Your are Successfully LogIned");
                admPL.admBO = adm;
                admPL.administratorMenu();
            }
            else
            {
                Console.Clear();
                Console.WriteLine("ERROR: InCorrect Login or Pin");
            }
        }
        /// <summary>
        /// customer Login
        /// </summary>
        private void customerLogin()
        {
            String login = inputLogin("Login: ");
            String pin;
            Customer_BO cus = bll.isCustomerExistAndValid(login);
            if (cus != null)
            {
                for (int i = 1; i <= 3; i++)
                {
                    pin = inputPin("Pin: ");
                    if (pin == cus.Pin)
                    {
                        Console.Clear();
                        Console.WriteLine("=>Your are Successfully LogIned");
                        cusPL.bo = cus;
                        cusPL.customerMenu();
                    }
                    else
                    {
                        Console.WriteLine("=>Your Pin does not match your Login");
                    }

                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("=>Login Does not Exist or Account is currently Disabled");
                return;
            }
            Console.Clear();
            Console.WriteLine("=>Your entered your Pin wrong 3 or more times.");
            Console.WriteLine("=>Your account is disabled Now.");
            Console.WriteLine("=>Contact Administrator for further Information.");
            bll.disableAccount(cus);
        }
        /// <summary>
        ///  exit From System
        /// </summary>
        private void exitFromSystem()
        {
            Console.Clear();
            Console.WriteLine("Thank You for using our service");
            System.Environment.Exit(0);
        }

        /// <summary>
        /// input Pin
        /// </summary>
        /// <param name="msg">string</param>
        /// <returns>string</returns>
        private string inputPin(string msg)
        { 
            string input;
            while (true)
            {
                Console.Write(msg);
                input = Console.ReadLine();
                try
                {
                    if (input.Length != 5)
                    {
                        throw new Exception("");
                    }
                    int.Parse(input);
                    return input;
                }
                catch
                {
                    Console.WriteLine("Error: Invalid Input");
                    continue;
                }
            }
        }


        /// <summary>
        /// input login
        /// </summary>
        /// <param name="msg">string</param>
        /// <returns>string</returns>
        private string inputLogin(string msg)
        {
            string input;
            bool valid;
            int len;
            while (true)
            {
                valid = true;
                Console.Write(msg);
                input = Console.ReadLine();
                len = input.Length;
                if (input.Length < 5 || input.Length > 10)
                {
                    Console.WriteLine("=>Login can be of minimum 5-characters and maximum 10-characters");
                    valid = false;
                }
                else
                {
                    for (int i = 0; i < len; i++)
                    {
                        if ((input[i] >= '0' && input[i] <= '9') || (input[i] >= 'a' && input[i] <= 'z') || (input[i] >= 'A' && input[i] <= 'Z'))
                        {
                            continue;
                        }
                        else
                        {
                            valid = false;
                            break;
                        }
                    }
                }
                if (valid == true)
                {
                    return input;
                }
                Console.WriteLine("ERROR: Invalid Input");
            }
        }
    }
}
