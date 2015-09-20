using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GoodsDeliverySystem.Models
{
    public class CreditCard
    {
        public string nameOnCard { get; private set; }
        public string cardType { get; private set; }
        public string cardNum { get; private set; }
        public int securityCode { get; private set; }
        public int expirationMonth { get; private set; }
        public int expirationYear { get; private set; }
        public Address billingAddress { get; private set; }

        public CreditCard(string nameOnCard, string cardType, string cardNum, int securityCode, int expirationMonth,
                          int expirationYear, Address billingAddress)
        {
            this.nameOnCard = nameOnCard;
            this.cardType = cardType;
            this.cardNum = cardNum;
            this.securityCode = securityCode;
            this.expirationMonth = expirationMonth;
            this.expirationYear = expirationYear;
            this.billingAddress = billingAddress;
        }
    }
}