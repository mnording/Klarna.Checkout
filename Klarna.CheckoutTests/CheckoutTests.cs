using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Channels;
using System.Threading;
using Klarna.Checkout;
using Klarna.Entities;
using Klarna.Exception;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Klarna.CheckoutTests
{
    [TestClass]
    public class CheckoutTests
    {
        [TestMethod]
        public void MustBeAbleToCreateCheckout()
        {
            MerchantConfig config = new MerchantConfig("K500746", "fia'w2ahSheahahc", Server.Playground);
            Checkout.Checkout check = new Checkout.Checkout(config);
            var order = getBaseOrder();
            order =  check.Create(order);
            Assert.AreEqual("checkout_incomplete",order.Status);
            Assert.IsNotNull(order.Snippet);
        }

        [TestMethod]
        public void MustBeAbleToUpdateOrder()
        {
            MerchantConfig config = new MerchantConfig("K500746", "fia'w2ahSheahahc", Server.Playground);
            Checkout.Checkout check = new Checkout.Checkout(config);
            var order = getBaseOrder();

            order = check.Create(order);
            Assert.AreEqual("checkout_incomplete", order.Status);
            Assert.IsNotNull(order.Snippet);
            var orderlines = new List<OrderLine>
            {
                new OrderLine("test", 2, 1000, 2500)
            };
            orderlines.Add(new OrderLine("tess",2,2300,2500));
            order.SetNewOrderlines(orderlines);
            check.Update(order);
        }
        [TestMethod]
        public void MustBeAbleToReadOrder()
        {
            MerchantConfig config = new MerchantConfig("K500746", "fia'w2ahSheahahc", Server.Playground);
            Checkout.Checkout check = new Checkout.Checkout(config);

            var order = getBaseOrder();
            order = check.Create(order);
            Assert.AreEqual("checkout_incomplete", order.Status);
            Assert.IsNotNull(order.Snippet);

            CheckoutOrder newOrder = check.Read(order.OrderId);
            Assert.AreEqual(order.OrderId, newOrder.OrderId);
        }

        [TestMethod]
        public void MustBeAbleToReadCorrelationOfException()
        {
            var order = getBaseOrder();
            MerchantConfig config = new MerchantConfig("K500746", "fia'w2ahSheahahc", Server.Playground);
            Checkout.Checkout check = new Checkout.Checkout(config);
            order.OrderAmount = 00;
            try
            {
                check.Create(order);
            }
            catch (ApiException ex)
            {
                Assert.IsNotNull(ex.correlation_id);
            }
        }

        private CheckoutOrder getBaseOrder()
        {
            var orderlines = new List<OrderLine>
            {
                new OrderLine("test", 2, 1000, 2500)
            };
            MerchantUrls url = new MerchantUrls
            {
                Terms = new Uri("http://www.test.com"),
                Push = new Uri("http://www.test.com"),
                CheckoutUri = new Uri("http://www.test.com"),
                Confirmation = new Uri("http://www.test.com")
            };
            CheckoutOrder order = new CheckoutOrder(orderlines, url)
            {
                Locale = "sv-se",
                PurchaseCountry = "se",
                PurchaseCurrency = "SEK"
            };
            return order;
        }
    }
}
