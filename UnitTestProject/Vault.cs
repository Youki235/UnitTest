using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProject
{
    class Vault : ICryptoCurrency
    {
        private ArrayList fMonies = new ArrayList(5);

        private Vault()
        {
        }
        public Vault(CryptoCurrency[] bag)
        {
            for (int i = 0; i < bag.Length; i++)
            {
                if (!bag[i].IsZero)
                    AppendMoney(bag[i]);
            }
        }
        public Vault(CryptoCurrency m1, CryptoCurrency m2)
        {
            AppendMoney(m1);
            AppendMoney(m2);
        }
        public Vault(CryptoCurrency m, Vault bag)
        {
            AppendMoney(m);
            AppendBag(bag);
        }
        public Vault(Vault m1, Vault m2)
        {
            AppendBag(m1);
            AppendBag(m2);
        }
        public ICryptoCurrency Add(ICryptoCurrency m)
        {
            return m.AddMoneyBag(this);
        }
        public ICryptoCurrency AddMoney(CryptoCurrency m)
        {
            return (new Vault(m, this)).Simplify();
        }
        public ICryptoCurrency AddMoneyBag(Vault s)
        {
            return (new Vault(s, this)).Simplify();
        }
        private void AppendBag(Vault aBag)
        {
            foreach (CryptoCurrency m in aBag.fMonies)
                AppendMoney(m);
        }
        private void AppendMoney(CryptoCurrency aMoney)
        {
            ICryptoCurrency old = FindMoney(aMoney.Currency);
            if (old == null)
            {
                fMonies.Add(aMoney);
                return;
            }
            fMonies.Remove(old);
            ICryptoCurrency sum = old.Add(aMoney);
            if (sum.IsZero)
                return;
            fMonies.Add(sum);
        }
        private bool Contains(CryptoCurrency aMoney)
        {
            CryptoCurrency m = FindMoney(aMoney.Currency);
            return m.Amount == aMoney.Amount;
        }
        public override bool Equals(Object anObject)
        {
            if (IsZero)
                if (anObject is ICryptoCurrency)
                    return ((ICryptoCurrency)anObject).IsZero;

            if (anObject is Vault)
            {
                Vault aMoneyBag = (Vault)anObject;
                if (aMoneyBag.fMonies.Count != fMonies.Count)
                    return false;

                foreach (CryptoCurrency m in fMonies)
                {
                    if (!aMoneyBag.Contains(m))
                        return false;
                }
                return true;
            }
            return false;
        }
        private CryptoCurrency FindMoney(String currency)
        {
            foreach (CryptoCurrency m in fMonies)
            {
                if (m.Currency.Equals(currency))
                    return m;
            }
            return null;
        }
        public override int GetHashCode()
        {
            int hash = 0;
            foreach (CryptoCurrency m in fMonies)
            {
                hash ^= m.GetHashCode();
            }
            return hash;
        }
        public bool IsZero
        {
            get { return fMonies.Count == 0; }
        }
        public ICryptoCurrency Multiply(int factor)
        {
            Vault result = new Vault();
            if (factor != 0)
            {
                foreach (CryptoCurrency m in fMonies)
                {
                    result.AppendMoney((CryptoCurrency)m.Multiply(factor));
                }
            }
            return result;
        }
        public ICryptoCurrency Negate()
        {
            Vault result = new Vault();
            foreach (CryptoCurrency m in fMonies)
            {
                result.AppendMoney((CryptoCurrency)m.Negate());
            }
            return result;
        }
        private ICryptoCurrency Simplify()
        {
            if (fMonies.Count == 1)
                return (ICryptoCurrency)fMonies[0];
            return this;
        }
        public ICryptoCurrency Subtract(ICryptoCurrency m)
        {
            return Add(m.Negate());
        }
        public override String ToString()
        {
            StringBuilder buffer = new StringBuilder();
            buffer.Append("{");
            foreach (CryptoCurrency m in fMonies)
                buffer.Append(m);
            buffer.Append("}");
            return buffer.ToString();
        }
    }
}

