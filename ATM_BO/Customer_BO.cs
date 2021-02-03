using System;
using System.Collections.Generic;
using System.Text;

namespace ATM_BO
{
   /// <summary>
    /// Customer business object
    /// </summary>
    public class Customer_BO
    {
        public int Acc_Id { get; set; }
        public int User_Id { get; set; }
        public string Login { get; set; }
        public String Pin { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int Balance { get; set; }
        public string Status { get; set; }
        public DateTime LastWDTime { get; set; } //Last withdraw time
        public int TodayWDAmount { get; set; } //Today total withdraw amount
    }
}
