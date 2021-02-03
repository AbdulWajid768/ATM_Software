using ATM_PL;
using System;
using System.IO;

namespace ATM_SOFTWARE_VERSION1._0
{
    /// <summary>
    /// Driver Class
    /// </summary>
    class Driver
    {
        static void Main(string[] args)
        {
            Login_PL login = new Login_PL();
            login.LoginMenu();
        }
    }
}
