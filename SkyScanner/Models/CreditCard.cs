using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace SkyScanner.Models
{
    public class CreditCard
    {
        public virtual User User { get; set; }
        [Required]
        [RegularExpression("^[0-9]{4}$")]
        public string User_ID { get; set; }
        [ForeignKey("User_ID")]
        [RegularExpression("^[0-9]{9}$", ErrorMessage = "Your ID is not valid")]
        [StringLength(9, MinimumLength = 9)]
        [DisplayName("Card Holder ID")]
        public string Israeli_ID { get; set; }
        [Key]
        [RegularExpression("^[0-9]{16}$", ErrorMessage = "Card number is not valid")]
        [StringLength(16, MinimumLength = 16)]
        [DisplayName("Card Number")]
        public string CardNumber { get; set; }
        [Range(1, 12, ErrorMessage = "Value must be between 1 to 12")]
        public int ExpMonth { get; set; }
        [Range(23, 30, ErrorMessage = "Value must be between 23 to 30")]
        public int ExpYear { get; set; }
        [Range(100, 999)]
        public int CVV { get; set; }
        public CreditCard()
        {
        }
        public bool Save { get; set; } = false;
        public CreditCard(string user_ID, string israeli_id, User user, string cardNumber, int expMonth, int expYear, int cVV)
        {
            User_ID = user_ID;
            User = user;
            CardNumber = cardNumber;
            ExpMonth = expMonth;
            ExpYear = expYear;
            CVV = cVV;
            Israeli_ID = israeli_id;
        }
    }
}
