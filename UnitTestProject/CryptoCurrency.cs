using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProject
{
    class CryptoCurrency :ICryptoCurrency
    {
        private int fAmount;
        private String fCurrency;

        public CryptoCurrency(int amount, String currency)
        {
            fAmount = amount;
            fCurrency = currency;
        }

        public ICryptoCurrency Add(ICryptoCurrency m)
        {
            return m.AddMoney(this);
        }

        public ICryptoCurrency AddMoney(CryptoCurrency m)
        {
            if (m.Currency.Equals(Currency))
                return new CryptoCurrency(Amount + m.Amount, Currency);
            return new Vault(this, m);
        }

        public ICryptoCurrency AddMoneyBag(Vault s)
        {
            return s.AddMoney(this);
        }

        public int Amount
        {
            get { return fAmount; }
        }

        public String Currency
        {
            get { return fCurrency; }
        }

        public override bool Equals(Object anObject)
        {
            if (IsZero)
                if (anObject is ICryptoCurrency)
                    return ((ICryptoCurrency)anObject).IsZero;
            if (anObject is CryptoCurrency)
            {
                CryptoCurrency aMoney = (CryptoCurrency)anObject;
                return aMoney.Currency.Equals(Currency)
                    && Amount == aMoney.Amount;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return fCurrency.GetHashCode() + fAmount;
        }

        public bool IsZero
        {
            get { return Amount == 0; }
        }

        public ICryptoCurrency Multiply(int factor)
        {
            return new CryptoCurrency(Amount * factor, Currency);
        }

        public ICryptoCurrency Negate()
        {
            return new CryptoCurrency(-Amount, Currency);
        }

        public ICryptoCurrency Subtract(ICryptoCurrency m)
        {
            return Add(m.Negate());
        }

        public override String ToString()
        {
            StringBuilder buffer = new StringBuilder();
            buffer.Append("[" + Amount + " " + Currency + "]");
            return buffer.ToString();
        }
    }
}

