using System;
using System.Collections.Generic;
using Klarna.Checkout;
using Klarna.Checkout.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Klarna.CheckoutTests
{
    [TestClass]
    public class CheckoutOrderTest
    {
        [TestMethod]
        public void MustCalculateOrderTaxAmountCorrect()
        {
            var orderlines = new List<CheckoutOrderLine>
            {
                new CheckoutOrderLine("test", 2, 1000, 1000),
                new CheckoutOrderLine("test", 2, 1000, 1000),
                new CheckoutOrderLine("test", 2, 1000, 1000)
            };
            CheckoutOrder order = new CheckoutOrder(orderlines,null);
            Assert.AreEqual(546,order.OrderTaxAmount);
        }
        [TestMethod]
        public void MustCalculateOrderAmountCorrect()
        {
            var orderlines = new List<CheckoutOrderLine>
            {
                new CheckoutOrderLine("test", 2, 1000, 1000),
                new CheckoutOrderLine("test", 2, 1000, 1000),
                new CheckoutOrderLine("test", 2, 1000, 1000)
            };
            CheckoutOrder order = new CheckoutOrder(orderlines, null);
            Assert.AreEqual(6000, order.OrderAmount);
        }
    }
}
