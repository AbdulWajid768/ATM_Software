using ATM_BLL;
using ATM_BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace ATM_PL
{
    /// <summary>
    /// Customer presentation layer
    /// </summary>
    public class Customer_PL
    {
        public Customer_BO bo;
        Customer_BLL bll = new Customer_BLL();


        /// <summary>
        /// display Customer Menu
        /// </summary>
        private void displayCustomerMenu()
        {
            Console.WriteLine("CUSTUMER-MENU");
            Console.WriteLine("Enter 1----Withdraw Cash");
            Console.WriteLine("Enter 2----Cash Transfer");
            Console.WriteLine("Enter 3----Deposit Cash ");
            Console.WriteLine("Enter 4----Display Balance");
            Console.WriteLine("Enter 5----Exit");
            Console.Write("Enter (1/2/3/4/5): ");
        }


        /// <summary>
        /// customerMenu
        /// </summary>
        public void customerMenu()
        {
            string option;
            while (true)
            {
                displayCustomerMenu();
                option = Console.ReadLine();
                if (option == "1" || option == "2" || option == "3" || option == "4" || option == "5")
                {
                    switch (option)
                    {
                        case "1":
                            Console.Clear();
                            withDrawCash();
                            break;
                        case "2":
                            Console.Clear();
                            cashTransfer();
                            break;
                        case "3":
                            Console.Clear();
                            depositeCash();
                            break;
                        case "4":
                            Console.Clear();
                            displayBalance();
                            break;
                        case "5":
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
        /// exitFromSystem
        /// </summary>
        private void exitFromSystem()
        {
            Console.Clear();
            Console.WriteLine("Thank You for using our service");
            System.Environment.Exit(0);
        }

        /// <summary>
        /// withdraw cash menu
        /// </summary>
        private void withDrawCashMenu()
        {
            Console.WriteLine("Please select a mode of withdrawal");
            Console.WriteLine("Enter 1----Fast Cash");
            Console.WriteLine("Enter 2-----Normal Cash");
            Console.WriteLine("Enter 3-----Back");
            Console.Write("Enter (1/2): ");
        }
        /// <summary>
        /// withdraw cash
        /// </summary>
        private void withDrawCash()
        {
            string option;
            while (true)
            {
                withDrawCashMenu();
                option = Console.ReadLine();
                if (option == "1" || option == "2" || option == "3")
                {
                    switch (option)
                    {
                        case "1":
                            Console.Clear();
                            fastCash();
                            break;
                        case "2":
                            Console.Clear();
                            normalCash();
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
        /// display fast cash menu
        /// </summary>
        private void fastCashMenu()
        {
            Console.WriteLine("Please select the amount");
            Console.WriteLine("Enter 1----500");
            Console.WriteLine("Enter 2----1,000");
            Console.WriteLine("Enter 3----2,000");
            Console.WriteLine("Enter 4----5,000");
            Console.WriteLine("Enter 5----10,000");
            Console.WriteLine("Enter 6----15,000");
            Console.WriteLine("Enter 7----20,000");
            Console.WriteLine("Enter 8----Back");
            Console.Write("Select one of the denominations of money(1/2/3/4/5/6/7/8): ");
        }
        /// <summary>
        /// withdraw fast cash
        /// </summary>
        private void fastCash()
        {
            string option;
            int amount = 0;
            while (true)
            {
                fastCashMenu();
                option = Console.ReadLine();
                if (option == "1" || option == "2" || option == "3" || option == "4" || option == "5" || option == "6" || option == "7" || option == "8")
                {
                    switch (option)
                    {
                        case "1":
                            amount = 500;
                            break;
                        case "2":
                            amount = 1000;
                            break;
                        case "3":
                            amount = 2000;
                            break;
                        case "4":
                            amount = 5000;
                            break;
                        case "5":
                            amount = 10000;
                            break;
                        case "6":
                            amount = 15000;
                            break;
                        case "7":
                            amount = 20000;
                            break;
                        case "8":
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
            withDrawCashAndStoreTransactionInfo(amount, "Fast Cash WithDrawal");
        }
        /// <summary>
        /// withdraw normal cash
        /// </summary>
        private void normalCash()
        {
            string input;
            int amount;
            while (true)
            {
                Console.Write("Please Enter the Withdrawal Amount: ");
                input = Console.ReadLine();
                try
                {
                    amount = int.Parse(input);
                    if (amount < 0 || amount > 20000)
                    {
                        Console.WriteLine("Error: Invalid Input");
                        Console.WriteLine("Amount should be greater than 0 less than eqaul to 20,000");
                    }
                    else
                    {
                        break;
                    }
                }
                catch
                {
                    Console.Clear();
                    Console.WriteLine("Error: Invalid Input");
                }
            }
            withDrawCashAndStoreTransactionInfo(amount, "Normal Cash WithDrawal");
        }

        /// <summary>
        /// withDraw Cash And Store Transaction Info
        /// </summary>
        /// <param name="amount">int</param>
        /// <param name="transactionType">string</param>
        private void withDrawCashAndStoreTransactionInfo(int amount, string transactionType)
        {
            string yn = inputYesNo("Are you sure you want to withdraw Rs." + amount + " (Y/N)? ");
            if (yn == "n")
            {
                Console.Clear();
                Console.WriteLine("Withdraw Aborted");
                return;
            }
            else
            {
                int result;
                (bo, result) = bll.withDrawCash(bo, amount, transactionType);
                switch (result)
                {
                    case 0:
                        break;
                    case -1:
                        Console.Clear();
                        Console.WriteLine("Daily WithDraw Limit(Rs. 20,000) crossed");
                        Console.WriteLine("Withdraw Aborted");
                        return;
                    case -2:
                        Console.Clear();
                        Console.WriteLine("Not enough Money in account to satisfy withdraw");
                        Console.WriteLine("Withdraw Aborted");
                        return;
                    default:
                        break;
                }
            }
            yn = inputYesNo("Cash Successfully Withdrawn! Do you wish to print a receipt (Y/N)?");
            if (yn == "n")
            {
                return;
            }
            else
            {
                displayWithDrawRecipt(amount);
            }
        }


        /// <summary>
        /// display WithDraw Recipt
        /// </summary>
        /// <param name="amount">int</param>
        private void displayWithDrawRecipt(int amount)
        {
            Console.Clear();
            Console.WriteLine("Account # " + bo.Acc_Id);
            Console.WriteLine("Date: " + DDMMYYYY(bo.LastWDTime));
            Console.WriteLine(String.Format("WithDrawn: {0,-13:N0}", amount));
            Console.WriteLine(String.Format("Balance: {0,-13:N0}", bo.Balance));
            Console.WriteLine("Press Any Key to Continue...");
            Console.ReadKey();
            Console.Clear();

        }

        /// <summary>
        /// display Deposited Recipt
        /// </summary>
        /// <param name="amount">int</param>
        private void displayDepositedRecipt(int amount)
        {
            Console.Clear();
            Console.WriteLine("Account # " + bo.Acc_Id);
            Console.WriteLine("Date: " + DDMMYYYY(DateTime.Today));
            Console.WriteLine(String.Format("Deposited: {0,-13:N0}", amount));
            Console.WriteLine(String.Format("Balance: {0,-13:N0}", bo.Balance));
            Console.WriteLine("Press Any Key to Continue...");
            Console.ReadKey();
            Console.Clear();
        }
        /// <summary>
        /// display Cash Transfer Recipt
        /// </summary>
        /// <param name="amount">int</param>
        private void displayCashTransferRecipt(int amount)
        {
            Console.Clear();
            Console.WriteLine("Account # " + bo.Acc_Id);
            Console.WriteLine("Date: " + DDMMYYYY(DateTime.Today));
            Console.WriteLine(String.Format("Cash Transferred: {0,-13:N0}", amount));
            Console.WriteLine(String.Format("Balance: {0,-13:N0}", bo.Balance));
            Console.WriteLine("Press Any Key to Continue...");
            Console.ReadKey();
            Console.Clear();
        }

        /// <summary>
        /// cash transfer
        /// </summary>
        private void cashTransfer()
        {
            int amount = inputAmountMultipleOf500("Enter amount in multiples of 500: ");
            int accNo = inputAccountNo("Enter the account number to which you want to transfer: ");
            Customer_BO cus = bll.isAccountExist(accNo);
            if (cus == null)
            {
                Console.Clear();
                Console.WriteLine("Account no. you entered does not exist OR currently Disabled");
                return;
            }
            int accNo2 = inputAccountNo("You wish to deposit Rs " + amount + " in account held by " + cus.Name + "; If this information is correct please re-enter the account number: ");
            if (accNo != accNo2)
            {
                Console.Clear();
                Console.WriteLine("Account no.s does not match");
                Console.WriteLine("Cash Transfer Aborted");
                return;
            }
            if (bo.Balance < amount)
            {
                Console.Clear();
                Console.WriteLine("Not enough oney in account to satisfy Transfer");
                Console.WriteLine("Cash Transfer Aborted");
                return;
            }
            bo.Balance = bo.Balance - amount;
            cus.Balance = cus.Balance + amount;
            bo = bll.cashTransfer(bo, cus, amount);
            string yn = inputYesNo("Do you wish to print a receipt (Y/N)? ");
            if (yn == "n")
            {
                return;
            }
            else
            {
                displayCashTransferRecipt(amount);
            }
        }

        /// <summary>
        /// deposite cash
        /// </summary>
        private void depositeCash()
        {
            int amount = inputAmount("Enter the cash amount to deposit:");
            bo = bll.depositeCash(bo, amount);
            string yn = inputYesNo("Cash Deposited Successfully. Do you wish to print a receipt (Y/N)? ");
            if (yn == "n")
            {
                return;
            }
            else
            {
                displayDepositedRecipt(amount);
            }
        }

        /// <summary>
        /// input y for yes, n for no
        /// </summary>
        /// <param name="msg">stirng</param>
        /// <returns>int</returns>
        private string inputYesNo(string msg)
        {
            string input;
            while (true)
            {
                Console.Write(msg);
                input = Console.ReadLine();
                input = input.ToLower();
                if (input == "y" || input == "n")
                {
                    return input;
                }
                else
                {
                    Console.WriteLine("Error: Invalid Input");
                }
            }
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
                    if (input.Length == 0 || input.Length > 10 || amount < 0 )
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
        /// input amount multiplr of 500
        /// </summary>
        /// <param name="msg">stirng</param>
        /// <returns>int</returns>
        private int inputAmountMultipleOf500(String msg)
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
                    if (amount % 500 == 0)
                    {
                        return amount;
                    }
                    else
                    {
                        throw new Exception("");
                    }
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
        /// <returns>int</returns>
        private int inputAccountNo(string msg)
        {
            string input;
            while (true)
            {
                Console.Write(msg);
                input = Console.ReadLine();
                try
                {
                    return int.Parse(input);
                }
                catch
                {
                    Console.WriteLine("Error: Invalid Input");
                }
            }
        }

        /// <summary>
        /// display customer current Balance
        /// </summary>
        private void displayBalance()
        {
            Console.WriteLine();
            Console.WriteLine("Account # " + bo.Acc_Id);
            Console.WriteLine("Date: " + DDMMYYYY(DateTime.Today));
            Console.WriteLine(String.Format("Balance: {0,-13:N0}", bo.Balance));
            Console.WriteLine("Press Any Key to Continue...");
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
    }
}

