using ATM_BLL;
using ATM_BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace ATM_PL
{
    /// <summary>
    /// Administrator presentation layer
    /// </summary>
    public class Administrator_PL
    {
        public Administrator_BO admBO;
        private Customer_BO bo;
        private Administrator_BLL bll = new Administrator_BLL();

        /// <summary>
        /// display Administrator Menu
        /// </summary>
        private void displayAdministratorMenu()
        {
            Console.WriteLine("ADMINISTRATOR-MENU");
            Console.WriteLine("Enter 1----Create New Account");
            Console.WriteLine("Enter 2----Delete Existing Account");
            Console.WriteLine("Enter 3----Update Account Information");
            Console.WriteLine("Enter 4----Search for Account");
            Console.WriteLine("Enter 5----View Reports ");
            Console.WriteLine("Enter 6----Exit");
            Console.Write("Enter (1/2/3/4/5/6): ");
        }
        /// <summary>
        /// display reports
        /// </summary>
        private void viewReports()
        {
            string option;
            while (true)
            {
                Console.WriteLine("Please select a mode for viewing reports");
                Console.WriteLine("Enter 1----Accounts By Amount ");
                Console.WriteLine("Enter 2----Accounts By Date ");
                Console.WriteLine("Enter 3----Back");
                Console.Write("Enter (1/2/3): ");
                option = Console.ReadLine();
                if (option == "1" || option == "2" || option == "3")
                {
                    switch (option)
                    {
                        case "1":
                            viewReportsByAmount();
                            break;
                        case "2":
                            viewReportsByDate();
                            break;
                        case "3":
                            Console.Clear();
                            return;
                    }
                    break;
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
        /// display Reports By Amount
        /// </summary>
        private void viewReportsByAmount()
        {
            int am1;
            int am2;
            while (true)
            {
                am1 = inputAmount("Enter the minimum amount: ");
                am2 = inputAmount("Enter the maximum amount: ");
                if (am1 <= am2)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("ERROR: Minimum Amount is greater than Maximun Amount.");
                }
            }
            //call bll
            List<Customer_BO> list = bll.searchAccountsByAmount(am1, am2);
            displayCustomerInformation(list);
        }

        /// <summary>
        /// display Reports By Date
        /// </summary>
        private void viewReportsByDate()
        {
            DateTime date1;
            DateTime date2;
            while (true)
            {
                date1 = inputDate("Enter the starting date(DD/MM/YYYY): ");
                date2 = inputDate("Enter the ending date(DD/MM/YYYY): ");

                if (date1 <= date2)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("ERROR:  Starting Date is greater than Ending Date");
                }
            }
            List<Transaction_BO> list = bll.searchAccountsByDate(date1, date2);
            displayTransactionByDate(list);
        }


        /// <summary>
        /// display administrator Menu
        /// </summary>
        public void administratorMenu()
        {
            string option;
            while (true)
            {
                displayAdministratorMenu();
                option = Console.ReadLine();
                if (option == "1" || option == "2" || option == "3" || option == "4" || option == "5" || option == "6")
                {
                    Console.Clear();
                    switch (option)
                    {
                        case "1":
                            createNewAccount();
                            break;
                        case "2":
                            deleteExistingAccount();
                            break;
                        case "3":
                            updateAccountInformation();
                            break;
                        case "4":
                            searchForAccount();
                            break;
                        case "5":
                            viewReports();
                            break;
                        case "6":
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
        /// create New Account
        /// </summary>
        private void createNewAccount()
        {
            Console.WriteLine("Creating an Account...");
            string login = inputLogin("Login: ", true);
            string pin = inputPin("Pin Code: ", true);
            string name = inputName("Holders Name: ", true);
            string type = inputType("Type (Saving, Current): ", true);
            int balance = inputBalance("Starting Balance: ", true);
            string status = inputStatus("Status(active, disabled): ", true);
            bo = new Customer_BO
            {
                Login = login,
                Pin = pin,
                Name = name,
                Type = type,
                Balance = balance,
                Status = status
            };
            (bool flag, int accNo) = bll.createNewAccount(bo);
            if (flag)
            {
                Console.Clear();
                Console.WriteLine("Account Successfully Created – the account number assigned is: " + accNo);
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Login already Exist. Try Again With Different Login");
            }
        }

        /// <summary>
        /// search For Account
        /// </summary>
        private void searchForAccount()
        {
            Console.WriteLine("Searching for an Account...");
            int accNo = inputAccountNo("Account ID: ", false);
            int UserNo = inputAccountNo("User ID: ", false);
            string name = inputName("Holders Name: ", false);
            string type = inputType("Type (saving, current): ", false);
            int balance = inputBalance("Balance: ", false);
            string status = inputStatus("Status(active, disabled): ", false);
            bo = new Customer_BO
            {
                Acc_Id = accNo,
                User_Id = UserNo,
                Name = name,
                Type = type,
                Balance = balance,
                Login = "",
                Pin = "",
                Status = status,
                LastWDTime = DateTime.Now,
                TodayWDAmount = 0
            };
            List<Customer_BO> list = bll.searchForAccount(bo);
            displayCustomerInformation(list);
        }


        /// <summary>
        /// display Customer Information
        /// </summary>
        /// <param name="list">List<Customer_BO></param>
        private void displayCustomerInformation(List<Customer_BO> list)
        {
            Console.Clear();
            Console.WriteLine(String.Format("{0,-12} {1,-15} {2,-20} {3,-10} {4,-13} {5,-10}", "Account ID", "User ID", "Holder Name", "Type", "Balance", "Status"));
            foreach (Customer_BO c in list)
            {
                Console.WriteLine(String.Format("{0,-12} {1,-15} {2,-20} {3,-10} {4,-13:N0} {5,-10}", c.Acc_Id, c.User_Id, c.Name, c.Type, c.Balance, c.Status));
            }
            Console.WriteLine("Press Any key To Continue...");
            Console.ReadKey();
            Console.Clear();
        }

        /// <summary>
        /// display Transaction By Date
        /// </summary>
        /// <param name="list">List<Transaction_BO></param>
        private void displayTransactionByDate(List<Transaction_BO> list)
        {
            Console.Clear();
            Console.WriteLine(String.Format("{0,-22} {1,-15} {2,-20} {3,-13} {4,-12} {5,-15} {6, -20}", "Transaction Type", "User ID", "Holder Name", "Amount", "Date", "Receiver UserID", "Receicer Name"));
            foreach (Transaction_BO t in list)
            {
                Console.WriteLine(String.Format("{0,-22} {1,-15} {2,-20} {3,-13:N0} {4,-12} {5,-15} {6, -20}",
                    t.transactionType, t.doerUserId, t.doerName, t.amount, DDMMYYYY(t.date),
                    t.receiverUserId == -1 ? "--" : t.receiverUserId.ToString(), t.receiverName == "" ? "--" : t.receiverName));
            }
            Console.WriteLine("Press Any key To Continue...");
            Console.ReadKey();
            Console.Clear();
        }

        /// <summary>
        /// Display date in DD/MM/YYYY format
        /// </summary>
        /// <param name="date">DateTime</param>
        /// <returns>string</returns>
        private string DDMMYYYY(DateTime date)
        {
            return date.Day + "/" + date.Month + "/" + date.Year;
        }
        private void deleteExistingAccount()
        {
            int accNo1 = inputAccountNo("Enter the account number to which you want to delete: ", true);
            Customer_BO cus = bll.isAccountExist(accNo1);
            if (cus == null)
            {
                Console.Clear();
                Console.WriteLine("SORRY: Account does not exist");
                return;
            }
            int accNo2 = inputAccountNo("You wish to delete the account held by " + cus.Name + "; If this information is correct please re-enter the account number: ", true);
            if (accNo1 != accNo2)
            {
                Console.Clear();
                Console.WriteLine("Request Rejected: Account number does not match");
                return;
            }
            bll.deleteAccount(accNo1);
            Console.Clear();
            Console.WriteLine("Account Deleted Successfully ");
        }

        /// <summary>
        /// display Customer Information
        /// </summary>
        private void displayCustomerInformation()
        {
            Console.WriteLine("Account # " + bo.Acc_Id);
            Console.WriteLine("Type: " + bo.Type);
            Console.WriteLine("Holder Name: " + bo.Name);
            Console.WriteLine(String.Format("Balance: {0,-13:N0}", bo.Balance));
            Console.WriteLine("Status: " + bo.Status);
        }



        /// <summary>
        /// update Account Information
        /// </summary>
        private void updateAccountInformation()
        {
            Console.WriteLine("Updating an Account Information...");
            int accNo = inputAccountNo("Enter the Account Number: ", true);
            bo = bll.isAccountExist(accNo);
            if (bo == null)
            {
                Console.Clear();
                Console.WriteLine("SORRY: Account does not exist");
                return;
            }
            displayCustomerInformation();

            Console.WriteLine("Please enter in the fields you wish to update (leave blank otherwise): ");

            string login = inputLogin("Login: ", false);
            if (login != "")
            {
                bo.Login = login;
            }
            String pin = inputPin("Pin Code: ", false);
            if (pin != "")
            {
                bo.Pin = pin;
            }
            string name = inputName("Holders Name: ", false);
            if (name != "")
            {
                bo.Name = name;
            }
            string status = inputStatus("Status: ", false);
            if (status != "")
            {
                bo.Status = status;
            }
            bll.updateAccountInformation(bo);
            Console.Clear();
            Console.WriteLine("Account # " + accNo + ": Information Upddated Succesfully.");
        }


        private void exitFromSystem()
        {
            Console.Clear();
            Console.WriteLine("Thank You for using our service");
            System.Environment.Exit(0);
        }


        /// <summary>
        /// input amount
        /// </summary>
        /// <param name="msg">stirng</param>
        /// <returns>int</returns>
        private int inputAmount(String msg)
        {
            string input;
            int amount;
            while (true)
            {
                Console.Write(msg);
                input = Console.ReadLine();
                try
                {
                    amount = int.Parse(input);
                    if (input.Length == 0 || input.Length > 10 || amount < 0)
                    {
                        Console.WriteLine("=>Amount Should be greater than Zero and can be upto 10-digits");
                        throw new Exception("");
                    }
                    return amount;
                }
                catch
                {
                    Console.WriteLine("Error: Invalid Input");
                }
            }
        }

        /// <summary>
        /// input Date
        /// </summary>
        /// <param name="msg">string</param>
        /// <returns>DateTime</returns>
        private DateTime inputDate(String msg)
        {
            string input;
            while (true)
            {
                Console.Write(msg);
                input = Console.ReadLine();
                try
                {
                    if (input.Length == 0)
                    {
                        throw new Exception("");
                    }
                    return DateTime.ParseExact(input, "dd/MM/yyyy", null);
                }
                catch
                {
                    Console.WriteLine("Error: Invalid Input");
                }
            }
        }
        /// <summary>
        /// input Account No
        /// </summary>
        /// <param name="msg">string</param>
        /// <param name="flag">bool</param>
        /// <returns>int</returns>
        private int inputAccountNo(string msg, bool flag)
        {
            string input;
            while (true)
            {
                Console.Write(msg);
                input = Console.ReadLine();
                try
                {
                    if (flag == true)
                    {
                        if (input == "")
                        {
                            throw new Exception("");
                        }
                    }
                    else
                    {
                        if (input == "")
                        {
                            return -1;
                        }
                    }
                    return int.Parse(input);
                }
                catch
                {
                    Console.WriteLine("Error: Invalid Input");
                }
            }
        }

        /// <summary>
        /// input User No
        /// </summary>
        /// <param name="msg">string</param>
        /// <param name="flag">bool</param>
        /// <returns>int</returns>
        private int inputUserNo(string msg, bool flag)
        {
            string input;
            while (true)
            {
                Console.Write(msg);
                input = Console.ReadLine();
                try
                {
                    if (flag == true)
                    {
                        if (input == "")
                        {
                            throw new Exception("");
                        }
                    }
                    else
                    {
                        if (input == "")
                        {
                            return -1;
                        }
                    }
                    return int.Parse(input);
                }
                catch
                {
                    Console.WriteLine("Error: Invalid Input");
                }
            }
        }



        /// <summary>
        /// input Pin
        /// </summary>
        /// <param name="msg">string</param>
        /// <param name="flag">bool</param>
        /// <returns>string</returns>
        private string inputPin(string msg, bool flag)
        {
            string input;
            while (true)
            {
                Console.Write(msg);
                input = Console.ReadLine();
                try
                {
                    if (flag == true)
                    {
                        if (input.Length != 5)
                        {
                            throw new Exception("");
                        }
                    }
                    else
                    {
                        if (input.Length != 0 && input.Length != 5)
                        {
                            throw new Exception("");
                        }
                    }
                    if (input.Length != 0)
                    {
                        int.Parse(input);
                    }
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
        /// input Login
        /// </summary>
        /// <param name="msg">string</param>
        /// <param name="flag">bool</param>
        /// <returns>string</returns>
        private string inputLogin(string msg, bool flag)
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
                if (flag == true)
                {
                    if (len == 0)
                    {
                        valid = false;
                    }
                }
                else
                {
                    if (len == 0)
                    {
                        return input;
                    }
                }
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

        /// <summary>
        /// input name
        /// </summary>
        /// <param name="msg">string</param>
        /// <param name="flag">bool</param>
        /// <returns>string</returns>
        private string inputName(string msg, bool flag)
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
                if (flag == true)
                {
                    if (len == 0)
                    {
                        valid = false;
                    }
                }
                else
                {
                    if (len == 0)
                    {
                        return input;
                    }

                }
                if (input.Length > 20)
                {
                    Console.WriteLine("=>Login can be of maximum 10-characters");
                    valid = false;
                }
                else
                {
                    for (int i = 0; i < len; i++)
                    {
                        if ((input[i] >= 'a' && input[i] <= 'z') || (input[i] >= 'A' && input[i] <= 'Z') || input[i] == ' ')
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

        /// <summary>
        /// input type
        /// </summary>
        /// <param name="msg">string</param>
        /// <param name="flag">bool</param>
        /// <returns>string</returns>
        private string inputType(string msg, bool flag)
        {
            string input;
            while (true)
            {
                Console.Write(msg);
                input = Console.ReadLine();
                input = input.ToLower();
                if (flag == true)
                {
                    if (input == "saving" || input == "current")
                    {
                        return input;
                    }
                }
                else
                {
                    if (input == "saving" || input == "current" || input == "")
                    {
                        return input;
                    }
                }
                Console.WriteLine("Error: Invalid Input");
            }
        }

        /// <summary>
        /// input balance
        /// </summary>
        /// <param name="msg">string</param>
        /// <param name="flag">bool</param>
        /// <returns>int</returns>
        private int inputBalance(string msg, bool flag)
        {
            string input;
            while (true)
            {
                Console.Write(msg);
                input = Console.ReadLine();
                try
                {
                    if (flag == true)
                    {
                        if (input.Length == 0 || input.Length > 10)
                        {
                            Console.WriteLine("=>Balance can only be upto 10 digits");
                            throw new Exception("");
                        }
                    }
                    else
                    {
                        if (input.Length == 0)
                        {
                            return -1;
                        }
                        if (input.Length > 10)
                        {
                            Console.WriteLine("=>Balance can only be upto 10 digits");
                            throw new Exception("");
                        }
                    }
                    return int.Parse(input);
                }
                catch
                {
                    Console.WriteLine("Error: Invalid Input");
                }
            }
        }

        /// <summary>
        /// input status
        /// </summary>
        /// <param name="msg">string</param>
        /// <param name="flag">bool</param>
        /// <returns>string</returns>
        private string inputStatus(string msg, bool flag)
        {
            string input;
            while (true)
            {
                Console.Write(msg);
                input = Console.ReadLine();
                input = input.ToLower();
                if (flag == true)
                {
                    if (input == "active" || input == "disabled")
                    {
                        return input;
                    }
                }
                else
                {
                    if (input == "active" || input == "disabled" || input == "")
                    {
                        return input;
                    }
                }
                Console.WriteLine("Error: Invalid Input");
            }
        }
    }
}
