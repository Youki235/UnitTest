using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProject
{
    [TestFixture]
    public class CryptoTestsProjectOne
    {
            private CryptoCurrency f12CHF;
            private CryptoCurrency f14CHF;
            private CryptoCurrency f7USD;
            private CryptoCurrency f21USD;

            private Vault fMB1;
            private Vault fMB2;

            /// <summary>
            /// Initializes CryptoCurrency test objects
            /// </summary>
            /// 
            [SetUp]
            protected void SetUp()
            {
                f12CHF = new CryptoCurrency(12, "CHF");
                f14CHF = new CryptoCurrency(14, "CHF");
                f7USD = new CryptoCurrency(7, "USD");
                f21USD = new CryptoCurrency(21, "USD");

                fMB1 = new Vault(f12CHF, f7USD);
                fMB2 = new Vault(f14CHF, f21USD);
            }

            /// <summary>
            /// Assert that Moneybags multiply correctly
            /// </summary>
            /// 
            [Test]
            public void BagMultiply()
            {
                // {[12 CHF][7 USD]} *2 == {[24 CHF][14 USD]}
                CryptoCurrency[] bag = { new CryptoCurrency(24, "CHF"), new CryptoCurrency(14, "USD") };
                Vault expected = new Vault(bag);
                Assert.AreEqual(expected, fMB1.Multiply(2));
                Assert.AreEqual(fMB1, fMB1.Multiply(1));
                Assert.IsTrue(fMB1.Multiply(0).IsZero);
            }

            /// <summary>
            /// Assert that Moneybags negate(positive to negative values) correctly
            /// </summary>
            /// 
            [Test]
            public void BagNegate()
            {
                // {[12 CHF][7 USD]} negate == {[-12 CHF][-7 USD]}
                CryptoCurrency[] bag = { new CryptoCurrency(-12, "CHF"), new CryptoCurrency(-7, "USD") };
                Vault expected = new Vault(bag);
                Assert.AreEqual(expected, fMB1.Negate());
            }

            /// <summary>
            /// Assert that adding currency to Moneybags happens correctly
            /// </summary>
            /// 
            [Test]
            public void BagSimpleAdd()
            {
                // {[12 CHF][7 USD]} + [14 CHF] == {[26 CHF][7 USD]}
                CryptoCurrency[] bag = { new CryptoCurrency(26, "CHF"), new CryptoCurrency(7, "USD") };
                Vault expected = new Vault(bag);
                Assert.AreEqual(expected, fMB1.Add(f14CHF));
            }

            /// <summary>
            /// Assert that subtracting currency to Moneybags happens correctly
            /// </summary>
            /// 
            [Test]
            public void BagSubtract()
            {
                // {[12 CHF][7 USD]} - {[14 CHF][21 USD] == {[-2 CHF][-14 USD]}
                CryptoCurrency[] bag = { new CryptoCurrency(-2, "CHF"), new CryptoCurrency(-14, "USD") };
                Vault expected = new Vault(bag);
                Assert.AreEqual(expected, fMB1.Subtract(fMB2));
            }

            /// <summary>
            /// Assert that adding multiple currencies to Moneybags in one statement happens correctly
            /// </summary>
            /// 
            [Test]
            public void BagSumAdd()
            {
                // {[12 CHF][7 USD]} + {[14 CHF][21 USD]} == {[26 CHF][28 USD]}
                CryptoCurrency[] bag = { new CryptoCurrency(26, "CHF"), new CryptoCurrency(28, "USD") };
                Vault expected = new Vault(bag);
                Assert.AreEqual(expected, fMB1.Add(fMB2));
            }

            /// <summary>
            /// Assert that Moneybags hold zero value after adding zero value
            /// </summary>
            /// 
            [Test]
            public void IsZero()
            {
                Assert.IsTrue(fMB1.Subtract(fMB1).IsZero);

                CryptoCurrency[] bag = { new CryptoCurrency(0, "CHF"), new CryptoCurrency(0, "USD") };
                Assert.IsTrue(new Vault(bag).IsZero);
            }

            /// <summary>
            /// Assert that a new bag is the same as adding value to an existing bag
            /// </summary>
            /// 
            [Test]
            public void MixedSimpleAdd()
            {
                // [12 CHF] + [7 USD] == {[12 CHF][7 USD]}
                CryptoCurrency[] bag = { f12CHF, f7USD };
                Vault expected = new Vault(bag);
                Assert.AreEqual(expected, f12CHF.Add(f7USD));
            }

            /// <summary>
            /// Assert that Vault.Equals() works correctly
            /// </summary>
            /// 
            [Test]
            public void MoneyBagEquals()
            {
                //NOTE: Normally we use Assert.AreEqual to test whether two
                // objects are equal. But here we are testing the Vault.Equals()
                // method itself, so using AreEqual would not serve the purpose.
                Assert.IsFalse(fMB1.Equals(null));

                Assert.IsTrue(fMB1.Equals(fMB1));
                Vault equal = new Vault(new CryptoCurrency(12, "CHF"), new CryptoCurrency(7, "USD"));
                Assert.IsTrue(fMB1.Equals(equal));
                Assert.IsTrue(!fMB1.Equals(f12CHF));
                Assert.IsTrue(!f12CHF.Equals(fMB1));
                Assert.IsTrue(!fMB1.Equals(fMB2));
            }

            /// <summary>
            /// Assert that the hash of a new bag is the same as 
            /// the hash of an existing bag with added value
            /// </summary>
            /// 
            [Test]
            public void MoneyBagHash()
            {
                Vault equal = new Vault(new CryptoCurrency(12, "CHF"), new CryptoCurrency(7, "USD"));
                Assert.AreEqual(fMB1.GetHashCode(), equal.GetHashCode());
            }

            /// <summary>
            /// Assert that CryptoCurrency.Equals() works correctly
            /// </summary>
            /// 
            [Test]
            public void MoneyEquals()
            {
                //NOTE: Normally we use Assert.AreEqual to test whether two
                // objects are equal. But here we are testing the Vault.Equals()
                // method itself, so using AreEqual would not serve the purpose.
                Assert.IsFalse(f12CHF.Equals(null));
                CryptoCurrency equalMoney = new CryptoCurrency(12, "CHF");
                Assert.IsTrue(f12CHF.Equals(f12CHF));
                Assert.IsTrue(f12CHF.Equals(equalMoney));
                Assert.IsFalse(f12CHF.Equals(f14CHF));
            }

            /// <summary>
            /// Assert that the hash of new CryptoCurrency is the same as 
            /// the hash of initialized CryptoCurrency
            /// </summary>
            /// 
            [Test]
            public void MoneyHash()
            {
                Assert.IsFalse(f12CHF.Equals(null));
                CryptoCurrency equal = new CryptoCurrency(12, "CHF");
                Assert.AreEqual(f12CHF.GetHashCode(), equal.GetHashCode());
            }

            /// <summary>
            /// Assert that adding multiple small values is the same as adding one big value
            /// </summary>
            /// 
            [Test]
            public void Normalize()
            {
                CryptoCurrency[] bag = { new CryptoCurrency(26, "CHF"), new CryptoCurrency(28, "CHF"), new CryptoCurrency(6, "CHF") };
                Vault Vault = new Vault(bag);
                CryptoCurrency[] expected = { new CryptoCurrency(60, "CHF") };
                // note: expected is still a Vault
                Vault expectedBag = new Vault(expected);
                Assert.AreEqual(expectedBag, Vault);
            }

            /// <summary>
            /// Assert that removing a value is the same as not having such a value
            /// </summary>
            /// 
            [Test]
            public void Normalize2()
            {
                // {[12 CHF][7 USD]} - [12 CHF] == [7 USD]
                CryptoCurrency expected = new CryptoCurrency(7, "USD");
                Assert.AreEqual(expected, fMB1.Subtract(f12CHF));
            }

            /// <summary>
            /// Assert that removing multiple values works correctly
            /// </summary>
            /// 
            [Test]
            public void Normalize3()
            {
                // {[12 CHF][7 USD]} - {[12 CHF][3 USD]} == [4 USD]
                CryptoCurrency[] s1 = { new CryptoCurrency(12, "CHF"), new CryptoCurrency(3, "USD") };
                Vault ms1 = new Vault(s1);
                CryptoCurrency expected = new CryptoCurrency(4, "USD");
                Assert.AreEqual(expected, fMB1.Subtract(ms1));
            }

            /// <summary>
            /// Assert that if value is subtracted from 0, the result will be negative.
            /// </summary>
            /// 
            [Test]
            public void Normalize4()
            {
                // [12 CHF] - {[12 CHF][3 USD]} == [-3 USD]
                CryptoCurrency[] s1 = { new CryptoCurrency(12, "CHF"), new CryptoCurrency(3, "USD") };
                Vault ms1 = new Vault(s1);
                CryptoCurrency expected = new CryptoCurrency(-3, "USD");
                Assert.AreEqual(expected, f12CHF.Subtract(ms1));
            }

            /// <summary>
            /// Assert that CryptoCurrency.ToString() function works correctly
            /// </summary>
            /// 
            [Test]
            public void Print()
            {
                Assert.AreEqual("[12 CHF]", f12CHF.ToString());
            }

            /// <summary>
            /// Assert that adding more value to CryptoCurrency happens correctly
            /// </summary>
            /// 
            [Test]
            public void SimpleAdd()
            {
                // [12 CHF] + [14 CHF] == [26 CHF]
                CryptoCurrency expected = new CryptoCurrency(26, "CHF");
                Assert.AreEqual(expected, f12CHF.Add(f14CHF));
            }

            /// <summary>
            /// Assert that adding multiple currencies to Moneybags happens correctly
            /// </summary>
            /// 
            [Test]
            public void SimpleBagAdd()
            {
                // [14 CHF] + {[12 CHF][7 USD]} == {[26 CHF][7 USD]}
                CryptoCurrency[] bag = { new CryptoCurrency(26, "CHF"), new CryptoCurrency(7, "USD") };
                Vault expected = new Vault(bag);
                Assert.AreEqual(expected, f14CHF.Add(fMB1));
            }

            /// <summary>
            /// Assert that multiplying currency in CryptoCurrency happens correctly
            /// </summary>
            /// 
            [Test]
            public void SimpleMultiply()
            {
                // [14 CHF] *2 == [28 CHF]
                CryptoCurrency expected = new CryptoCurrency(28, "CHF");
                Assert.AreEqual(expected, f14CHF.Multiply(2));
            }

            /// <summary>
            /// Assert that negating(positive to negative values) currency in CryptoCurrency happens correctly
            /// </summary>
            /// 
            [Test]
            public void SimpleNegate()
            {
                // [14 CHF] negate == [-14 CHF]
                CryptoCurrency expected = new CryptoCurrency(-14, "CHF");
                Assert.AreEqual(expected, f14CHF.Negate());
            }

            /// <summary>
            /// Assert that removing currency from CryptoCurrency happens correctly
            /// </summary>
            /// 
            [Test]
            public void SimpleSubtract()
            {
                // [14 CHF] - [12 CHF] == [2 CHF]
                CryptoCurrency expected = new CryptoCurrency(2, "CHF");
                Assert.AreEqual(expected, f14CHF.Subtract(f12CHF));
            }
        }
    }

