using System;
using System.Collections.Generic;
using System.Text;

namespace ATM_BO
{
    /// <summary>
    /// Transaction business object
    /// </summary>
    public class Transaction_BO
    {
        public string transactionType { get; set; }
        public int doerUserId { get; set; }
        public string doerName { get; set; }
        public int amount { get; set; }
        public DateTime date { get; set; }
        public int receiverUserId { get; set; }
        public string receiverName { get; set; }
    }
}
