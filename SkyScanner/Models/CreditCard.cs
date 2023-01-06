using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SkyScanner.Models
{
    public class CreditCard
    {
        public string User_ID { get; set; }
        [ForeignKey("User_ID")]
        public virtual User User { get; set; }
        [Key]
        public string CardNumber { get; set; }
        public int ExpMonth { get; set; }
        public int ExpYear { get; set; }
        public int CVV { get; set; }
        public CreditCard()
        {

        }
        public CreditCard(string user_ID, User user, string cardNumber, int expMonth, int expYear, int cVV)
        {
            User_ID = user_ID;
            User = user;
            CardNumber = cardNumber;
            ExpMonth = expMonth;
            ExpYear = expYear;
            CVV = cVV;
        }
    }
}
