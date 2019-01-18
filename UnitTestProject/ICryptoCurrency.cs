using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProject
{
    interface ICryptoCurrency
    {
   
        ICryptoCurrency Add(ICryptoCurrency m);
        ICryptoCurrency AddMoney(CryptoCurrency m);
        ICryptoCurrency AddMoneyBag(Vault s);
        bool IsZero { get; }
        ICryptoCurrency Multiply(int factor);
        ICryptoCurrency Negate();
        ICryptoCurrency Subtract(ICryptoCurrency m);
    }
}
